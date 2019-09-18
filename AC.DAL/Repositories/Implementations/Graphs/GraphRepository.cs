using AC.DAL.EF;
using AC.DAL.Repositories.Interfaces.Graphs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Implementations.Graphs
{
    public class GraphRepository : BaseRepository<Graph, ACDatabaseEntities>, IGraphRepository
    {
        public GraphRepository(ACDatabaseEntities databaseEntities) : base(databaseEntities) { }

        public async Task<Graph> GetByIdWithReferencAsync(int graphId)
        {
            return await _dbContext.Graphs.Include(y => y.Nodes).Include(z => z.Connections).
                Include(t => t.Connections.Select(f => f.A2BConnectionType)).
                         Include(o => o.EnteryNode).FirstOrDefaultAsync(x => x.Id == graphId);
        }
    }
}