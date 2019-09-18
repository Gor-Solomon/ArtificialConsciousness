using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Common
{
    public class SimpleModel : BlModelBase
    {
        public SimpleModel()
        {

        }

        public SimpleModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
