using AC.BLL.Common;
using AC.BLL.Models.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Interfaces.Connections
{
    public interface IConnectionTypeService : IBaseService
    {
        Task<BlResult<IEnumerable<ConnectionTypeModel>>> GetAllAsync();
        Task<BlResult<ConnectionTypeModel>> GetByIdAsync(int id);
        Task<BlResult<ConnectionTypeModel>> SaveAsync(ConnectionTypeModel connectionTypeModel);
        Task<BlResult> DeleteAsync(int id);
    }
}
