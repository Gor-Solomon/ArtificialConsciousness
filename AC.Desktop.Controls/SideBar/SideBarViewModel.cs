using AC.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Desktop.Controls.SideBar
{
    public class SideBarViewModel<T> : BaseSideBarViewModel<T, SideBarView> where T : class, IBllModel
    {
        public SideBarViewModel(IEnumerable<T> collection, SideBarConfiguration<T> configuration = null) : base(collection, configuration)
        {
        }
    }
}
