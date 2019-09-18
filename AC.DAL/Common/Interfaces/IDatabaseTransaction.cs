using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Common.Interfaces
{
    public interface IDatabaseTransaction : IDisposable
    {
        DbContextTransaction Transaction { get; set; }

        void EnsureTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        Task<int> SaveChangesAsync(bool commit = true);
        void KillDBContext();
    }
}
