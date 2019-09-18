using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Attribuets
{
    public class AttributeDescriptionModel : BlModelBase
    {
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    Notify();
                }
            }
        }

    }
}
