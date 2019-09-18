using AC.BLL.Interfaces.Graph;
using AC.BLL.Models.Connections;
using AC.BLL.Models.Graphs;
using AC.BLL.Models.Nodes;
using AC.Desktop.Controls.Commands;
using AC.Desktop.Controls.Dialgo.ViewModels;
using AC.Desktop.Controls.Helpers;
using AC.Desktop.Controls.SideBar;
using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Main.Views;
using AC.Desktop.Main.Views.Graphs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Desktop.Main.ViewModels.Graphs
{
    public class GraphsViewModel : ViewModel<GraphsView>, ICloseable
    {//a
        #region Fildes
        private IGraphService _graphService;
        GraphModel _graphModel;
        #endregion
        public GraphsViewModel(IGraphService graphService)
        {
            _graphService = graphService;
            SaveCommand = new BaseCommand(SaveExecute, o => !SideBar?.SelectedItemIsNull ?? false);
        }

        #region Commands
        public BaseCommand SaveCommand { get; }
        #endregion



        #region Properties

        public SideBarViewModel<GraphModel> SideBar { get; set; }
        public GraphModel SelectedGraphModel
        {
            get { return _graphModel; }
            set
            {
                if (_graphModel != value)
                {
                    _graphModel = value;
                    Notify();
                }
            }
        }
        #endregion

        #region Ovveridebles
        protected override async void LoadControl(object param = null)
        {
            if (_graphService is null)
            {
                _graphService = App.Container.Resolve<IGraphService>();
            }

            var blResult = await _graphService.GetAllAsync();

            if (blResult.Succeed)
            {
                ConfigureSideBar(blResult.Value.ToObservable());
            }
            else
            {
                await ShowMessage(blResult.ErrorMessage);
            }

            SaveCommand.NotifyCanExecuteChanged();
        }
        #endregion

        #region Privet Methods
        void ConfigureSideBar(ObservableCollection<GraphModel> list)
        {
            var sideBarConfiguration = new SideBarConfiguration<GraphModel>
            {
                SelectedItemAction = SelectExecute,
                NewAction = NewExecute,
                DeleteAction = DeleteExecute,
            };

            SideBar = new SideBarViewModel<GraphModel>(list, configuration: sideBarConfiguration);
            SideBar.SelectedItem = null;

            Notify(nameof(SideBar));
        }

        private async void DeleteExecute(object obj)
        {
            if (SelectedGraphModel != null)
            {
                if (SelectedGraphModel.Id == 0)
                {
                    SideBar.Items.Remove(SelectedGraphModel);
                    SideBar.SelectedItem = SideBar.Items.FirstOrDefault();
                }
                else
                {
                    var result = await _graphService.DeleteAsync(SelectedGraphModel.Id);

                    if (!result.Succeed)
                    {
                        await ShowMessage(result.ErrorMessage);
                    }
                    else
                    {
                        LoadControl(null);
                        SideBar.SelectedItem = SideBar.Items.LastOrDefault();
                    }
                }

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private void NewExecute(object obj)
        {
            SelectedGraphModel = new GraphModel();
            SideBar.AddItem(SelectedGraphModel);
            SideBar.SelectedItem = SelectedGraphModel;
            SaveCommand.NotifyCanExecuteChanged();
        }

        private async void SelectExecute(GraphModel obj)
        {
            if (obj != null && obj.Id != 0)
            {
                var blResult = await _graphService.GetByIdAsync(obj.Id);

                if (blResult.Succeed)
                {
                    SelectedGraphModel = blResult.Value;
                    SaveCommand.NotifyCanExecuteChanged();
                }
                else
                {
                    await ShowMessage(blResult.ErrorMessage);
                }

            }
        }

        async void SaveExecute(object param)
        {
            if (SelectedGraphModel != null)
            {
                var blResult = await _graphService.SaveAsync(SelectedGraphModel);

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

        public void InvokeClose()
        {
            SelectedGraphModel = null;
            SideBar = null;

            _graphService.Dispose();
            _graphService = null;
        }
        #endregion

    }
}
