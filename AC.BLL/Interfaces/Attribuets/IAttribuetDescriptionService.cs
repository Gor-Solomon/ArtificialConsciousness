using AC.BLL.Common;
using AC.BLL.Models.Attribuets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Interfaces.Attribuets
{
    public interface IAttribuetDescriptionService : IBaseService
    {
        Task<BlResult<AttributeDescriptionModel>> GetByIdAsync(int id);
        Task<BlResult<IEnumerable<AttributeDescriptionModel>>> GetAllAsync();
        Task<BlResult<AttributeDescriptionModel>> SaveAsync(AttributeDescriptionModel attributeDescriptionModel);
        Task<BlResult> DeleteAsync(int id);
    }
}
