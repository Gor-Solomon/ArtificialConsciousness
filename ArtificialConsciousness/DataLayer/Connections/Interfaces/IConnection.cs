using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Environment.DataLayer.Nodes.Interfaces;

namespace AC.Environment.DataLayer.Connections.Interfaces
{
    interface IConnection
    {
        List<INode> AsideNodes { get; set; }
        List<INode> BsideNodes { get; set; }
    }
}
