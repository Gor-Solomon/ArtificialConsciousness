using AC.Desktop.Controls.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Desktop.Controls.Dialgo.ViewModels
{
    public interface ICloseable : IViewModel
    {
        void InvokeClose();
    }
}
