using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Interfaces.Connections
{
    public interface IConnectionTypeRepository : IBaseRepository<ConnectionType>, IDatabaseTransaction
    {
        Task<IEnumerable<ConnectionType>> GetAllWithReferencsAsync();
        Task<ConnectionType> GetByIdWithReferencAsync(int connectionTypeId);
        Task AddAttribuets(ConnectionType connectionTypeEntity, IEnumerable<int> ids);
    }
}
