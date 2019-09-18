using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Main.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.BLL.Interfaces.Node;
using AC.Desktop.Controls.Commands;
using AC.Desktop.Main.Services.Interfaces;

namespace AC.Desktop.Main.ViewModels
{
    public class MainViewModel : ViewModel<MainView>
    {
        IDialogService _dialogService;

        public BaseCommand GraphsDialogCommand { get; }
        public BaseCommand NodesDialogCommand { get; }
        public BaseCommand ConnectionsDialogCommand { get; }
        public BaseCommand ConnectionTypesDialogCommand { get; }
        public BaseCommand AttribuetsDialogCommand { get; }

        public  MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            GraphsDialogCommand = new BaseCommand(GraphsDialogExecute);
            NodesDialogCommand = new BaseCommand(NodesDialogExecute);
            ConnectionsDialogCommand = new BaseCommand(ConnectionsDialogExecute);
            ConnectionTypesDialogCommand = new BaseCommand(ConnectionTypesDialogExecute);
            AttribuetsDialogCommand = new BaseCommand(AttribuetDialogExecute);
        }

        void GraphsDialogExecute(object param)
        {
            _dialogService.ShowGraphsDialog();
        }

        void NodesDialogExecute(object param)
        {
            _dialogService.ShowNodesDialog();
        }

        void ConnectionsDialogExecute(object param)
        {
            _dialogService.ShowConnectionsDialog();
        }

        private void ConnectionTypesDialogExecute(object obj)
        {
            _dialogService.ShowConnectionTypesDialog();
        }

        private void AttribuetDialogExecute(object obj)
        {
            _dialogService.ShowAttribuetsDialog();
        }
    }
}
