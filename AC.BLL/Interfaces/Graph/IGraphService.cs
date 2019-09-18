using AC.BLL.Common;
using AC.BLL.Models.Graphs;
using AC.BLL.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Interfaces.Graph
{
    public interface IGraphService : IBaseService
    {
        Task<BlResult<GraphModel>> GetByIdAsync(int graphId);
        Task<BlResult<IEnumerable<GraphModel>>> GetAllAsync();
        Task<BlResult<GraphModel>> SaveAsync(GraphModel graphModel);
        Task<BlResult> DeleteAsync(int id); 
    }
}
