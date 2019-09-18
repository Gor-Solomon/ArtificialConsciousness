using AC.BLL.Interfaces.Connections;
using AC.BLL.Interfaces.Graph;
using AC.BLL.Interfaces.Node;
using AC.BLL.Models.Common;
using AC.BLL.Models.Connections;
using AC.BLL.Models.Graphs;
using AC.BLL.Models.Nodes;
using AC.Desktop.Controls.Commands;
using AC.Desktop.Controls.Dialgo.ViewModels;
using AC.Desktop.Controls.Helpers;
using AC.Desktop.Controls.SideBar;
using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Main.Views.Connections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Desktop.Main.ViewModels.Connections
{
    class ConnectionsViewModel : ViewModel<ConnectionsView>, ICloseable
    {
        #region Fildes
        IConnectionTypeService _connectionTypeService;
        IConnectionService _connectionService;
        INodeService _nodeService;
        IGraphService _graphService;

        private ObservableCollection<GraphModel> _graphModels;
        private ObservableCollection<SimpleModel> _nodeModels;
        private ObservableCollection<ConnectionTypeModel> _connectionTypes;
        private ConnectionModel _selectedConnection;
        #endregion

        #region Constructors
        public ConnectionsViewModel(IConnectionService connectionService, INodeService nodeService, IGraphService graphService, IConnectionTypeService connectionTypeService)
        {
            _connectionTypeService = connectionTypeService;
            _connectionService = connectionService;
            _nodeService = nodeService;
            _graphService = graphService;

            SaveCommand = new BaseCommand(SaveExecute, o => SelectedConnection != null);
        }

        #endregion

        #region Properties
        public SideBarViewModel<SimpleModel> SideBar { get; set; }

        public ObservableCollection<GraphModel> GraphModels
        {
            get { return _graphModels; }
            set
            {
                if (_graphModels != value)
                {
                    _graphModels = value;
                    Notify();
                }
            }
        }

        public ObservableCollection<ConnectionTypeModel> ConnectionTypes
        {
            get { return _connectionTypes; }
            set
            {
                if (_connectionTypes != value)
                {
                    _connectionTypes = value;
                    Notify();
                }
            }
        }


        public ObservableCollection<SimpleModel> NodeModels
        {
            get { return _nodeModels; }
            set
            {
                if (_nodeModels != value)
                {
                    _nodeModels = value;
                    Notify();
                }
            }
        }
        public ConnectionModel SelectedConnection
        {
            get { return _selectedConnection; }
            set
            {

                if (_selectedConnection != value)
                {
                    _selectedConnection = value;
                    Notify();
                    Notify(nameof(_selectedConnection.Graph));
                }
            }
        }

        #endregion

        #region Commands
        public BaseCommand SaveCommand { get; }
        #endregion

        #region Ovveridebels
        protected override async void LoadControl(object param = null)
        {
            _nodeService = _nodeService ?? App.Container.Resolve<INodeService>();
            _graphService = _graphService ?? App.Container.Resolve<IGraphService>();
            _connectionService = _connectionService ?? App.Container.Resolve<IConnectionService>();
            _connectionTypeService = _connectionTypeService ?? App.Container.Resolve<IConnectionTypeService>();

            if (_graphModels is null)
            {
                var graphResult = await _graphService.GetAllAsync();

                if (graphResult.Succeed)
                {
                    GraphModels = graphResult.Value.ToObservable();
                }
                else
                {
                    await ShowMessage(graphResult.ErrorMessage);
                }
            }

            if (_nodeModels is null)
            {
                var nodeResult = await _nodeService.GetNodesIdAndNameAsync();

                if (nodeResult.Succeed)
                {
                    NodeModels = nodeResult.Value.
                        Select(x => new SimpleModel(x.Key, x.Value)).ToObservable();
                }
                else
                {
                    await ShowMessage(nodeResult.ErrorMessage);
                }
            }

            if (ConnectionTypes is null)
            {
                var connectionTypeResult = await _connectionTypeService.GetAllAsync();

                if (connectionTypeResult.Succeed)
                {
                    ConnectionTypes = connectionTypeResult.Value.ToObservable();
                }
                else
                {
                    await ShowMessage(connectionTypeResult.ErrorMessage);
                }
            }

            var connectionsResult = await _connectionService.GetAllByNameAndIdAsync();

            if (connectionsResult.Succeed)
            {
                ConfigureSideBar(connectionsResult.Value.ToObservable());
            }
            else
            {
                await ShowMessage(connectionsResult.ErrorMessage);
            }

            SaveCommand.NotifyCanExecuteChanged();
        }

        public void InvokeClose()
        {
            GraphModels = null;
            NodeModels = null;
            SelectedConnection = null;
            ConnectionTypes = null;

            _graphService.Dispose();
            _graphService = null;
            _nodeService.Dispose();
            _nodeService = null;
            _connectionService.Dispose();
            _connectionService = null;
            _connectionTypeService.Dispose();
            _connectionTypeService = null;
        }
        #endregion

        #region Privet Methods
        void ConfigureSideBar(ObservableCollection<SimpleModel> list)
        {
            var sideBarConfiguration = new SideBarConfiguration<SimpleModel>
            {
                SelectedItemAction = SelectExecute,
                NewAction = NewExecute,
                DeleteAction = DeleteExecute,
            };

            SideBar = new SideBarViewModel<SimpleModel>(list, configuration: sideBarConfiguration);
            SideBar.SelectedItem = null;

            Notify(nameof(SideBar));
        }

        private void NewExecute(object obj)
        {
            var temp = new SimpleModel();
            SideBar.AddItem(temp);
            SideBar.SelectedItem = temp;
            SaveCommand.NotifyCanExecuteChanged();
        }

        private async void DeleteExecute(object obj)
        {
            if (SelectedConnection != null)
            {
                if (SelectedConnection.Id == 0)
                {
                    SideBar.RemoveEmptyItems();
                    SideBar.SelectedItem = SideBar.Items.FirstOrDefault();
                    Notify(nameof(SideBar));
                }
                else
                {
                    var result = await _connectionService.DeleteAsync(SelectedConnection.Id);

                    if (!result.Succeed)
                    {
                        await ShowMessage(result.ErrorMessage);
                    }
                    else
                    {
                        LoadControl(null);

                    }
                }

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private async void SelectExecute(SimpleModel obj)
        {
            if (obj != null && obj.Id > 0)
            {
                var result = await _connectionService.GetByIdAsync(obj.Id);

                if (result.Succeed)
                {
                    SelectedConnection = new ConnectionModel();
                    Notify(nameof(SelectedConnection));
                    SelectedConnection = result.Value;
                    Notify(nameof(SelectedConnection));
                    Notify(nameof(SelectedConnection.Graph)); 
                }
                else
                {
                    await ShowMessage(result.ErrorMessage);
                }
            }
            else
            {
                SelectedConnection = new ConnectionModel();
            }

            SaveCommand.NotifyCanExecuteChanged();
        }

        private async void SaveExecute(object obj)
        {
            var result = await _connectionService.SaveAsync(SelectedConnection);

            if (result.Succeed)
            {
                LoadControl(null);
            }
            else
            {
                await ShowMessage(result.ErrorMessage);
            }
        }
        #endregion
    }
}
