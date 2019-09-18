using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Nodes
{
    public class NodeAttributeModel : BlModelBase
    {
        public override string DisplayName => AttributeDescription?.Name;
        public int NodeID { get; set; }
        public int AttributeID { get; set; }

        public AttributeDescriptionModel AttributeDescription { get; set; }
        public NodeModel Node { get; set; }
    }
}
