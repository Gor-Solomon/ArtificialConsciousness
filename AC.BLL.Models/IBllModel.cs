using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models
{
    public interface IBllModel
    {

        int Id { get; set; }

        string Name { get; set; }

        string DisplayName { get; }

        bool IsSelectedItemForView { get; set; }
    }
}
