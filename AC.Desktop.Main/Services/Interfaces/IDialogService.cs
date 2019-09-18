using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Desktop.Main.Services.Interfaces
{
    public interface IDialogService
    {
        void ShowGraphsDialog();
        void ShowNodesDialog();
        void ShowConnectionsDialog();
        void ShowConnectionTypesDialog();
        void ShowAttribuetsDialog();
    }
}
