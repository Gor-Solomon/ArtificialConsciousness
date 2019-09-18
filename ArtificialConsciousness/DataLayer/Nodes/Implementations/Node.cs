using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Environment.DataLayer.Nodes.Interfaces;

namespace AC.Environment.DataLayer.Nodes.Implementations
{
    public class Node : INode
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public object Data { get; set; }
    }
}
