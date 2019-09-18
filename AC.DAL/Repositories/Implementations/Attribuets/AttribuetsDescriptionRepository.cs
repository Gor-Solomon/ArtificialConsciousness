using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using AC.DAL.Repositories.Interfaces.Attribuets;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Implementations.Attribuets
{
    public class AttribuetsDescriptionRepository : BaseRepository<AttributeDescription, ACDatabaseEntities>, IAttribuetDescriptionRepository
    {
        public AttribuetsDescriptionRepository(ACDatabaseEntities databaseEntities) : base(databaseEntities)
        {

        }

        public async Task<bool> ExistsEntityWithSuchAName(string name)
        {
            return await _dbContext.AttributeDescriptions.AnyAsync(x => x.Name == name);
        }
    }
}
