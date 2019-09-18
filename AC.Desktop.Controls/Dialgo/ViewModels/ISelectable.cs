using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Desktop.Controls.Dialgo.ViewModels
{
    public interface ISelectable
    {
        bool IsSelectEnabled { get; set; }
        int SelectedItemId { get; set; }
    }
}
