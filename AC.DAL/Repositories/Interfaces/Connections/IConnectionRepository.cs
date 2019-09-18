using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Interfaces.Connections
{
    public interface IConnectionRepository : IBaseRepository<Connection>, IDatabaseTransaction
    {
        Task<IEnumerable<KeyValuePair<int, string>>> GetConnectionsByNameAndIdAsync();

        Task<Connection> GetByIdWithReferencAsync(int graphId);
    }
}
