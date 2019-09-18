using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Desktop.Main
{
    public class ShellViewModel : ViewModel<ShellView>
    {
        public IViewModel ShellContent { get; set; }
        public ShellViewModel(Lazy<MainViewModel> mainViewModel)
        {
            ShellContent = mainViewModel.Value;
        }
        public void Show()
        {
            View.Show();
        }
    }
}
