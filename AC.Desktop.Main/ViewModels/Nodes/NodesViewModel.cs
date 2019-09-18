using AC.BLL.Implementations.Graph;
using AC.BLL.Implementations.Node;
using AC.BLL.Interfaces.Attribuets;
using AC.BLL.Interfaces.Graph;
using AC.BLL.Interfaces.Node;
using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Common;
using AC.BLL.Models.Connections;
using AC.BLL.Models.Graphs;
using AC.BLL.Models.Nodes;
using AC.Desktop.Controls.Commands;
using AC.Desktop.Controls.Dialgo.ViewModels;
using AC.Desktop.Controls.Helpers;
using AC.Desktop.Controls.SideBar;
using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Main.Views.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Desktop.Main.ViewModels.Nodes
{

    class NodesViewModel : ViewModel<NodesView>, ICloseable
    {
        #region Fildes
        INodeService _nodeService;
        IGraphService _graphService;
        IAttribuetDescriptionService _attribuetDescriptionService;
        NodeModel _selectedNodeModel;
        SimpleModel _nodesList;
        ObservableCollection<AttributeDescriptionModel> _attribuets;
        #endregion

        public NodesViewModel(INodeService nodeService, IGraphService graphService, IAttribuetDescriptionService attribuetDescriptionService)
        {
            _nodeService = nodeService;
            _graphService = graphService;
            _attribuetDescriptionService = attribuetDescriptionService;

            SaveCommand = new BaseCommand(SaveExecute, o => SelectedNodeModel != null);
        }

        #region Properties

        public NodeModel SelectedNodeModel
        {
            get
            { return _selectedNodeModel; }
            set
            {
                if (_selectedNodeModel != value)
                {
                    _selectedNodeModel = value;
                    Notify();
                };
            }
        }

        public SimpleModel NodesList
        {
            get
            { return _nodesList; }
            set
            {
                if (_nodesList != value)
                {
                    _nodesList = value;
                    Notify();
                };
            }
        }

        public ObservableCollection<AttributeDescriptionModel> Attribuets
        {
            get { return _attribuets; }
            set
            {
                if (_attribuets != value)
                {
                    _attribuets = value;
                    Notify();
                };
            }
        }



        public GraphModel SelectedGraph
        {
            get
            { return SelectedNodeModel?.Graph; }
            set
            {
                if (SelectedNodeModel?.Graph != value)
                {
                    if (SelectedNodeModel != null)
                    {
                        SelectedNodeModel.Graph = value;
                        SelectedNodeModel.GraphId = value?.Id ?? 0;
                    }
                    Notify();
                };
            }
        }

        public SideBarViewModel<SimpleModel> SideBar { get; set; }
        public ObservableCollection<GraphModel> GraphModels { get; set; }
        #endregion

        protected override async void LoadControl(object param = null)
        {
            _graphService = _graphService ?? App.Container.Resolve<IGraphService>();
            _nodeService = _nodeService ?? App.Container.Resolve<INodeService>();
            _attribuetDescriptionService = _attribuetDescriptionService ?? App.Container.Resolve<IAttribuetDescriptionService>();

            var graphResult = await _graphService.GetAllAsync();

            if (graphResult.Succeed)
            {
                GraphModels = graphResult.Value.Select
                    (x => new GraphModel() { Id = x.Id, Name = x.Name }).ToObservable();

                Notify(nameof(GraphModels));
            }
            else
            {
                await ShowMessage(graphResult.ErrorMessage);
            }

            var nodesResult = await _nodeService.GetNodesIdAndNameAsync();

            if (nodesResult.Succeed)
            {
                ConfigureSideBar(nodesResult.Value.Select(x => new SimpleModel()
                { Id = x.Key, Name = x.Value }).ToObservable());
            }
            else
            {
                await ShowMessage(nodesResult.ErrorMessage);
            }

            var attrResult = await _attribuetDescriptionService.GetAllAsync();

            if (attrResult.Succeed)
            {
                Attribuets = attrResult.Value.ToObservable();
            }
            else
            {
                await ShowMessage(attrResult.ErrorMessage);
            }
        }

        public void InvokeClose()
        {
            SelectedNodeModel = null;

            _graphService.Dispose();
            _graphService = null;

            _nodeService.Dispose();
            _nodeService = null;

            _attribuetDescriptionService.Dispose();
            _attribuetDescriptionService = null;
        }

        #region Commands
        public BaseCommand SaveCommand { get; }
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

        private async void DeleteExecute(object obj)
        {
            if (SelectedNodeModel != null)
            {
                if (SelectedNodeModel.Id == 0)
                {
                    SideBar.Items.Remove(SideBar.SelectedItem);
                    SideBar.SelectedItem = SideBar.Items.FirstOrDefault();
                }
                else
                {
                    var result = await _nodeService.DeleteAsync(SelectedNodeModel.Id);

                    if (!result.Succeed)
                    {
                        await ShowMessage(result.ErrorMessage);
                    }
                    else
                    {
                        SideBar.SelectedItem = null;
                        SelectedNodeModel = null;
                        LoadControl(null);

                    }
                }

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private void NewExecute(object obj)
        {
            var temp = new SimpleModel();
            SideBar.AddItem(temp);
            SideBar.SelectedItem = temp;
            SelectedNodeModel = new NodeModel();
            SelectedGraph = GraphModels.FirstOrDefault();
            SaveCommand.NotifyCanExecuteChanged();
        }

        private async void SelectExecute(SimpleModel obj)
        {
            if (obj != null && obj.Id != 0)
            {
                var result = await _nodeService.GetByIdAsync(obj.Id);

                if (result.Succeed)
                {
                    SelectedNodeModel = result.Value;
                    SelectedGraph = GraphModels.FirstOrDefault(x => x.Id == SelectedNodeModel.GraphId);
                }
                else
                {
                    await ShowMessage(result.ErrorMessage);
                }
            }

            SaveCommand.NotifyCanExecuteChanged();
        }

        private async void SaveExecute(object obj)
        {
            var result = await _nodeService.SaveAsync(SelectedNodeModel);

            if (result.Succeed)
            {
                SelectedNodeModel = null;
                LoadControl(null);
                SideBar.SelectedItem = SideBar.Items.FirstOrDefault(x => x.Id == result.Value.Id);
            }
            else
            {
                await ShowMessage(result.ErrorMessage);
            }
        }
        #endregion
    }
}
