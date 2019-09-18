using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Environment.DataLayer.Nodes.Interfaces;

namespace AC.Environment.DataLayer.DataGraph
{
    public interface ISemanticGraph
    {
        void AddNode(INode node, INode parrentNode);
    }
}
