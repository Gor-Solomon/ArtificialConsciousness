using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Interfaces.Nodes
{
    public interface INodeRepository : IBaseRepository<Node> , IDatabaseTransaction
    {
        Task<IEnumerable<Node>> GetNodesByGraphIdAsync(int graphId);
        Task<Dictionary<int, string>> GetNodesIdAndNameAsync();
        Task<Node> GetByIdWithReferencAsync(int id);
        Task AddAttribuets(Node fetchedEntity, IEnumerable<int> attributesToAdd);
    }
}
