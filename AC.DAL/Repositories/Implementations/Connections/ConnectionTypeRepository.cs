using AC.DAL.EF;
using AC.DAL.Repositories.Interfaces.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace AC.DAL.Repositories.Implementations.Connections
{
    public class ConnectionTypeRepository : BaseRepository<ConnectionType, ACDatabaseEntities>, IConnectionTypeRepository
    {
        public ConnectionTypeRepository(ACDatabaseEntities databaseEntities) : base(databaseEntities)
        {

        }

        public async Task AddAttribuets(ConnectionType connectionTypeEntity, IEnumerable<int> ids)
        {
            if (ids != null)
            {
                if (connectionTypeEntity.ConnectionTypeAttributes is null)
                {
                    connectionTypeEntity.ConnectionTypeAttributes = new List<AttributeDescription>();
                }

                foreach (var item in ids)
                {
                    var attr = await _dbContext.AttributeDescriptions.FindAsync(item);
                    connectionTypeEntity.ConnectionTypeAttributes.Add(attr);
                }
            }
        }

        public async Task<IEnumerable<ConnectionType>> GetAllWithReferencsAsync()
        {
            return await _dbContext.ConnectionTypes.Include(a => a.ConnectionTypeAttributes).ToListAsync();
        }

        public async Task<ConnectionType> GetByIdWithReferencAsync(int connectionTypeId)
        {
            return await _dbContext.ConnectionTypes.Include(a => a.ConnectionTypeAttributes).
                         FirstOrDefaultAsync(x => x.Id == connectionTypeId);
        }
    }
}
