using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Interfaces.Graphs
{
    public interface IGraphRepository : IBaseRepository<Graph>, IDatabaseTransaction
    {
        Task<Graph> GetByIdWithReferencAsync(int graphId);
    }
}
