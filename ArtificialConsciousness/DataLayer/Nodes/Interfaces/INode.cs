using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Environment.DataLayer.Nodes.Interfaces
{
    public interface INode
    {
       int Id { get; set; }
       string Name { get; set; }
       object Data { get; set; }
    }
}
