using AC.DAL.EF;
using AC.DAL.Repositories.Interfaces;
using AC.DAL.Repositories.Interfaces.Nodes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Implementations.Nodes
{
    public class NodeRepository : BaseRepository<Node, ACDatabaseEntities>, INodeRepository
    {
        public NodeRepository(ACDatabaseEntities databaseEntities) : base(databaseEntities) { }

        public async Task AddAttribuets(Node fetchedEntity, IEnumerable<int> attributesToAdd)
        {
            if (attributesToAdd != null)
            {
                if (fetchedEntity.NodeAttributes is null)
                {
                    fetchedEntity.NodeAttributes = new List<AttributeDescription>();
                }

                foreach (var item in attributesToAdd)
                {
                    var attr = await _dbContext.AttributeDescriptions.FindAsync(item);
                    fetchedEntity.NodeAttributes.Add(attr);
                }
            }
        }

        public async Task<Node> GetByIdWithReferencAsync(int id)
        {
            return await _dbContext.Nodes.Include(a => a.Graph).
                Include(b => b.ConnectionsFrom).
                Include(bA => bA.ConnectionsFrom.Select(bB => bB.A2BConnectionType)).
                Include(c => c.ConnectionsTo).
                Include(cA => cA.ConnectionsFrom.Select(cB => cB.B2AConnectionType)).
                Include(d => d.NodeAttributes).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Node>> GetNodesByGraphIdAsync(int graphId)
        {
            return await _dbContext.Nodes.Where(x => x.GraphId == graphId).Include(y => y.Graph).ToListAsync();
        }

        public async Task<Dictionary<int, string>> GetNodesIdAndNameAsync()
        {
            return await _dbContext.Nodes.ToDictionaryAsync(d => d.Id, y => y.Name);
        }
    }
}
