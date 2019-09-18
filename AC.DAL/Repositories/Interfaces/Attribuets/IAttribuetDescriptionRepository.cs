using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Interfaces.Attribuets
{
    public interface IAttribuetDescriptionRepository : IBaseRepository<AttributeDescription>, IDatabaseTransaction
    {
        Task<bool> ExistsEntityWithSuchAName(string name);
    }
}
