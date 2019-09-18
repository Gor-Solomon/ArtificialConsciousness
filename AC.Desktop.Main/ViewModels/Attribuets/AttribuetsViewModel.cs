using AC.BLL.Interfaces.Attribuets;
using AC.BLL.Models.Attribuets;
using AC.Desktop.Controls.Commands;
using AC.Desktop.Controls.Dialgo.ViewModels;
using AC.Desktop.Controls.Helpers;
using AC.Desktop.Controls.SideBar;
using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Main.Views.Attribuets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Desktop.Main.ViewModels.Attribuets
{
    class AttribuetsViewModel : ViewModel<AttribuetsView>, ICloseable
    {
        #region Fildes
        private IAttribuetDescriptionService _attribuetDescriptionService;
        private AttributeDescriptionModel _attributeDescriptionModel;
        #endregion

        #region Constructors
        public AttribuetsViewModel(IAttribuetDescriptionService attribuetDescriptionService)
        {
            _attribuetDescriptionService = attribuetDescriptionService;
            SaveCommand = new BaseCommand(SaveExecute, o => !SideBar?.SelectedItemIsNull ?? false);
        }
        #endregion

        #region Commands
        public BaseCommand SaveCommand { get; }
        #endregion

        #region Properties


        public AttributeDescriptionModel AttributeDescriptionModel
        {
            get { return _attributeDescriptionModel; }
            set
            {
                if (_attributeDescriptionModel != value)
                {
                    _attributeDescriptionModel = value;
                    Notify();
                }
            }
        }

        public SideBarViewModel<AttributeDescriptionModel> SideBar { get; set; }
        #endregion

        #region Overridables
        protected override async void LoadControl(object param = null)
        {
            _attribuetDescriptionService = _attribuetDescriptionService ?? App.Container.Resolve<IAttribuetDescriptionService>();

            var blResult = await _attribuetDescriptionService.GetAllAsync();

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
            AttributeDescriptionModel = null;
            SideBar = null;
        }
        #endregion

        #region Private Methods

        void ConfigureSideBar(ObservableCollection<AttributeDescriptionModel> attributeDescriptionModel)
        {
            var sideBarConfiguration = new SideBarConfiguration<AttributeDescriptionModel>
            {
                SelectedItemAction = SelectExecute,
                NewAction = NewExecute,
                DeleteAction = DeleteExecute,
            };

            SideBar = new SideBarViewModel<AttributeDescriptionModel>(attributeDescriptionModel, configuration: sideBarConfiguration);
            SideBar.SelectedItem = null;

            Notify(nameof(SideBar));
        }

        private void NewExecute(object obj)
        {
            AttributeDescriptionModel = new AttributeDescriptionModel();
            SideBar.AddItem(AttributeDescriptionModel);
            SideBar.SelectedItem = AttributeDescriptionModel;
            SaveCommand.NotifyCanExecuteChanged();
        }

        private void SelectExecute(AttributeDescriptionModel obj)
        {
            AttributeDescriptionModel = obj;
        }

        private async void DeleteExecute(object obj)
        {
            if (AttributeDescriptionModel != null)
            {
                if (AttributeDescriptionModel.Id == 0)
                {
                    SideBar?.Items?.Remove(AttributeDescriptionModel);
                    if (SideBar != null)
                    {
                        SideBar.SelectedItem = SideBar.Items.FirstOrDefault();
                    }
                }
                else
                {
                    var result = await _attribuetDescriptionService.DeleteAsync(AttributeDescriptionModel.Id);

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
            if (AttributeDescriptionModel != null)
            {
                var blResult = await _attribuetDescriptionService.SaveAsync(AttributeDescriptionModel);

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
