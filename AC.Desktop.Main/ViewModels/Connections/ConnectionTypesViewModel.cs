using AC.BLL.Interfaces.Attribuets;
using AC.BLL.Interfaces.Connections;
using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Connections;
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
    class ConnectionTypesViewModel : ViewModel<ConnectionTypesView>, ICloseable
    {
        #region Fildes
        private IConnectionTypeService _connectionTypeService;
        private IAttribuetDescriptionService _attribuetDescriptionService;
        private ConnectionTypeModel _selectedConnectionType;
        private ObservableCollection<AttributeDescriptionModel> _attribuets;
        #endregion

        #region Constructors
        public ConnectionTypesViewModel(IConnectionTypeService connectionTypeService, IAttribuetDescriptionService attribuetDescriptionService)
        {
            _connectionTypeService = connectionTypeService;
            _attribuetDescriptionService = attribuetDescriptionService;
            SaveCommand = new BaseCommand(SaveExecute, o => !SideBar?.SelectedItemIsNull ?? false);
        }
        #endregion

        #region Commands
        public BaseCommand SaveCommand { get; }
        #endregion

        #region Properties


        public ConnectionTypeModel SelectedConnectionType
        {
            get { return _selectedConnectionType; }
            set
            {
                if (_selectedConnectionType != value)
                {
                    _selectedConnectionType = value;
                    Notify();
                }
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
                }
            }
        }

        public SideBarViewModel<ConnectionTypeModel> SideBar { get; set; }
        #endregion

        #region Overridables
        protected override async void LoadControl(object param = null)
        {
            _attribuetDescriptionService = _attribuetDescriptionService ?? App.Container.Resolve<IAttribuetDescriptionService>();
            _connectionTypeService = _connectionTypeService ?? App.Container.Resolve<IConnectionTypeService>();

            var attirbuetsResult = await _attribuetDescriptionService.GetAllAsync();

            if (attirbuetsResult.Succeed)
            {
                Attribuets = attirbuetsResult.Value.ToObservable();
            }
            else
            {
                await ShowMessage(attirbuetsResult.ErrorMessage);
            }

            var blResult = await _connectionTypeService.GetAllAsync();

            if (blResult.Succeed)
            {
                ConfigureSideBar(blResult.Value.ToObservable());
                SideBar.SelectedItem = SideBar.Items.FirstOrDefault();
            }
            else
            {
                await ShowMessage(blResult.ErrorMessage);
            }

            SaveCommand.NotifyCanExecuteChanged();
        }
        #endregion

        #region IClosable
        public void InvokeClose()
        {
            _attribuetDescriptionService = null;
            _connectionTypeService = null;
            SelectedConnectionType = null;
            SideBar = null;
        }
        #endregion

        #region Private Methods

        void ConfigureSideBar(ObservableCollection<ConnectionTypeModel> connectionTypeModels)
        {
            var sideBarConfiguration = new SideBarConfiguration<ConnectionTypeModel>
            {
                SelectedItemAction = SelectExecute,
                NewAction = NewExecute,
                DeleteAction = DeleteExecute,
            };

            SideBar = new SideBarViewModel<ConnectionTypeModel>(connectionTypeModels, configuration: sideBarConfiguration);
            SideBar.SelectedItem = null;

            Notify(nameof(SideBar));
        }

        private void NewExecute(object obj)
        {
            SelectedConnectionType = new ConnectionTypeModel();
            SelectedConnectionType.ConnectionTypeAttributes = new ObservableCollection<AttributeDescriptionModel>();
            SideBar.AddItem(SelectedConnectionType);
            SideBar.SelectedItem = SelectedConnectionType;
            SaveCommand.NotifyCanExecuteChanged();
        }

        private void SelectExecute(ConnectionTypeModel obj)
        {
            SelectedConnectionType = obj;
        }

        private async void DeleteExecute(object obj)
        {
            if (SelectedConnectionType != null)
            {
                if (SelectedConnectionType.Id == 0)
                {
                    SideBar.Items.Remove(SelectedConnectionType);
                    SideBar.SelectedItem = SideBar.Items.FirstOrDefault();
                }
                else
                {
                    var result = await _connectionTypeService.DeleteAsync(SelectedConnectionType.Id);

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

        private async void SaveExecute(object obj)
        {
            if (SelectedConnectionType != null)
            {
                var blResult = await _connectionTypeService.SaveAsync(SelectedConnectionType);

                if (blResult.Succeed)
                {
                    LoadControl(null);
                    SideBar.SelectedItem = SideBar.Items.FirstOrDefault(x => x.Id == blResult.Value.Id);
                }
                else
                {
                    await ShowMessage(blResult.ErrorMessage);
                }
            }
        }
        #endregion
    }
}
