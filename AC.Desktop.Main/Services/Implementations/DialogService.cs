using AC.Desktop.Controls.Dialgo;
using AC.Desktop.Main.Resources.Local;
using AC.Desktop.Main.Services.Interfaces;
using AC.Desktop.Main.ViewModels.Attribuets;
using AC.Desktop.Main.ViewModels.Connections;
using AC.Desktop.Main.ViewModels.Graphs;
using AC.Desktop.Main.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AC.Desktop.Main.Services.Implementations
{
    class DialogService : BaseDialogService, IDialogService
    {
        Lazy<GraphsViewModel> _grapshViewModel;
        Lazy<NodesViewModel> _nodesViewModel;
        Lazy<ConnectionsViewModel> _connectionsViewModel;
        Lazy<ConnectionTypesViewModel> _connectionTypesViewModel;
        Lazy<AttribuetsViewModel> _attribuetsViewModel;
        public DialogService(Lazy<GraphsViewModel> grapshViewModel,
                             Lazy<NodesViewModel> nodesViewModel,
                             Lazy<ConnectionsViewModel> connectionsViewModel,
                             Lazy<ConnectionTypesViewModel> connectionTypesViewModel,
                             Lazy<AttribuetsViewModel> attribuetsViewModel)
        {
            _grapshViewModel = grapshViewModel;
            _nodesViewModel = nodesViewModel;
            _connectionsViewModel = connectionsViewModel;
            _connectionTypesViewModel = connectionTypesViewModel;
            _attribuetsViewModel = attribuetsViewModel;
        }

       

        public void ShowGraphsDialog()
        {
            ShowDialog(_grapshViewModel.Value, Strings.Graphs, true);
        }

        public void ShowNodesDialog()
        {
            ShowDialog(_nodesViewModel.Value, Strings.Nodes, true);
        }

        public void ShowConnectionsDialog()
        {
            ShowDialog(_connectionsViewModel.Value, Strings.Connections, true);
        }

        public void ShowConnectionTypesDialog()
        {
            ShowDialog(_connectionTypesViewModel.Value, Strings.ConnectionTypes, true);
        }

        public void ShowAttribuetsDialog()
        {
            ShowDialog(_attribuetsViewModel.Value, Strings.Attributes, true);
        }
    }
}
