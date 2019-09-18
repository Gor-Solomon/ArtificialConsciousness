using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.BLL.Common;
using AC.BLL.Models.Nodes;

namespace AC.BLL.Interfaces.Node
{
    public interface INodeService : IBaseService
    {
        Task<BlResult<Dictionary<int, string>>> GetNodesIdAndNameAsync();
        Task<BlResult<IEnumerable<NodeModel>>> GetByGraphsAsync(int graphId);
        Task<BlResult<NodeModel>> GetByIdAsync(int id);
        Task<BlResult<NodeModel>> SaveAsync(NodeModel nodeModel);
        Task<BlResult> DeleteAsync(int id);
    }
}
