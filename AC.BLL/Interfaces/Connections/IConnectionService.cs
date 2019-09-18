using AC.BLL.Common;
using AC.BLL.Models.Common;
using AC.BLL.Models.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Interfaces.Connections
{
    public interface IConnectionService : IBaseService
    {
        Task<BlResult<ConnectionModel>> GetByIdAsync(int connectionId);
        Task<BlResult<IEnumerable<ConnectionModel>>> GetAllAsync();
        Task<BlResult<IEnumerable<SimpleModel>>> GetAllByNameAndIdAsync();
        Task<BlResult<ConnectionModel>> SaveAsync(ConnectionModel connectionModel);
        Task<BlResult> DeleteAsync(int id);
    }
}
