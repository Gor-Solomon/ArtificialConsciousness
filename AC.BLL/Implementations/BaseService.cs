using AC.BLL.Interfaces;
using AC.BLL.Resources;
using AC.DAL.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Implementations
{
    public abstract class BaseService : IBaseService, IDisposable
    {
        IDatabaseTransaction DatabaseTransaction { get; set; }

        public IBaseService ChildBL { get; }

        public bool IsChild { get; set; }

        public bool IsSavingChanges { get; set; }

        public bool IsCommitingChanges { get; set; }

        private BaseService()
        {
            IsSavingChanges = true;
            IsCommitingChanges = true;
        }

        public BaseService(IBaseService childBL) : this()
        {
            ChildBL = childBL;
        }

        public BaseService(IDatabaseTransaction databaseTransaction) : this()
        {
            DatabaseTransaction = databaseTransaction;
        }

        public BaseService(IDatabaseTransaction databaseTransaction, IBaseService childBL = null) : this(childBL)
        {
            DatabaseTransaction = databaseTransaction;
            if (ChildBL != null)
            {
                ChildBL.IsChild = true;
                ChildBL.IsCommitingChanges = false; // Default value when BL is child

                var bl = ChildBL as BaseService;

                if (bl != null)
                {
                    bl.DatabaseTransaction = DatabaseTransaction;
                }

                BaseService temp = this;
                while (temp.ChildBL != null)
                {
                    temp = temp.ChildBL as BaseService;
                    if (temp != null)
                    {
                        temp.IsChild = true;
                        temp.IsCommitingChanges = false;
                        temp.DatabaseTransaction = DatabaseTransaction;
                    }
                }
            }
        }

        //public BaseService(IDatabaseTransaction databaseTransaction, params IBaseService[] baseServices)
        //{
        //    DatabaseTransaction = databaseTransaction;
        //    foreach (var service in baseServices)
        //    {
        //        service.IsChild = true;
        //        service.IsCommitingChanges = false;
        //        var bl = service as BaseService;
        //        if (bl != null)
        //            bl.DatabaseTransaction = databaseTransaction;
        //    }
        //}

        public void EnsureTransaction()
        {
            if (DatabaseTransaction == null)
                throw CreateException(nameof(DatabaseTransaction));

            DatabaseTransaction.EnsureTransaction();
        }

        public void RollbackTransaction()
        {
            if (DatabaseTransaction != null)
                DatabaseTransaction.RollBackTransaction();
        }

        public async Task<int> SaveChangesAsync(bool commit = true)
        {
            if (!IsSavingChanges) return -1;

            int result = -1;

            commit = commit && IsCommitingChanges;

            if (IsChild && commit)
                throw CreateException(ConstDictionary.ChildSaveNotAllowed);

            result = await DatabaseTransaction.SaveChangesAsync(commit);

            return result;
        }

        public virtual void Dispose()
        {
            DatabaseTransaction?.Dispose();
            DatabaseTransaction?.KillDBContext();
        }

        protected Exception CreateException(params string[] args)
        {
            var builder = new StringBuilder();
            foreach (var arg in args)
                builder.Append($"{arg}{Environment.NewLine}");

            return new Exception(builder.ToString());
        }
    }
}
