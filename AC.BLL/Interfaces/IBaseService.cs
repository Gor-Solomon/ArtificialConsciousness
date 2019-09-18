using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Interfaces
{
    public interface IBaseService : IDisposable
    {
        bool IsChild { get; set; }
        bool IsSavingChanges { get; set; }
        bool IsCommitingChanges { get; set; }
        void RollbackTransaction();
        void EnsureTransaction();
        Task<int> SaveChangesAsync(bool commit = true);
    }
}
