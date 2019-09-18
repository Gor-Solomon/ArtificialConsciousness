using AC.DAL.EF;
using AC.DAL.Repositories.Interfaces.Connections;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Implementations.Connections
{
    public class ConnectionRepository : BaseRepository<Connection, ACDatabaseEntities>, IConnectionRepository
    {
        public ConnectionRepository(ACDatabaseEntities databaseEntities) : base(databaseEntities)
        {

        }

        public async Task<Connection> GetByIdWithReferencAsync(int connectionId)
        {
            return await _dbContext.Connections.Include(a => a.A2BConnectionType).
                         Include(b => b.B2AConnectionType).
                         Include(d => d.Graph).Include(e => e.NodeA).Include(f => f.NodeB).
                         Include(g => g.Overwrite).Include(h => h.Overwriters).
                         FirstOrDefaultAsync(x => x.Id == connectionId);
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetConnectionsByNameAndIdAsync()
        {
            return (await _dbContext.Connections.Select(a => new { a.Id, NodeA = a.NodeA.Name, NodeB = a.NodeB.Name }).ToListAsync()
                 ).Select(x => new KeyValuePair<int, string>(x.Id, x.NodeA + " - " + x.NodeB));
        }
    }
}
