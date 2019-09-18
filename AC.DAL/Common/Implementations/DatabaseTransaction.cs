using AC.DAL.Common.Interfaces;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Common.Implementations
{
    public abstract class DatabaseTransaction<T> : IDatabaseTransaction where T : DbContext
    {
        protected readonly ACDatabaseEntities _dbContext;
        public DbContextTransaction Transaction { get; set; }

        public DatabaseTransaction(ACDatabaseEntities dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
            _dbContext.Configuration.LazyLoadingEnabled = false;
            _dbContext.Configuration.ProxyCreationEnabled = false;
        }
        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Dispose();
            }
        }

        public void Dispose()
        {
            try
            {
                _dbContext.Database.UseTransaction(null);
                Transaction?.Dispose();
                Transaction = null;
            }
            catch (Exception)
            {

            }
           
        }

        public void EnsureTransaction()
        {
            if (Transaction is null)
            {
                Transaction = _dbContext.Database.BeginTransaction();
            }
        }

        public void RollBackTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
            }
            RollBackChanges();
        }

        public async Task<int> SaveChangesAsync(bool commit = true)
        {
            int result = -1;

            try
            {
                if (Transaction == null)
                    throw CreateException("Transaction is null");

                result = await _dbContext.SaveChangesAsync();

                if (commit)
                    CommitTransaction();
            }
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    var entry = ex.Entries.FirstOrDefault().Entity;

            //    RollBackTransaction();

            //    string entryInfo = string.Empty;

            //    if (entry is IEntity iEntity)
            //        entryInfo = BuildInfo(entry.GetType(), iEntity.Id);

            //    throw CreateException(nameof(DbUpdateConcurrencyException), entryInfo);
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    DbEntityValidationResult validationResult = ex.EntityValidationErrors.FirstOrDefault();
            //    DbValidationError validationError = validationResult.ValidationErrors.FirstOrDefault();

            //    var entry = validationResult.Entry.Entity;
            //    var entryInfo = string.Empty;

            //    RollBackTransaction();

            //    if (entry is IEntity iEntity)
            //        entryInfo = BuildInfo(entry.GetType(), iEntity.Id);

            //    throw CreateException(nameof(DbEntityValidationException), entryInfo, validationError.PropertyName, validationError.ErrorMessage);
            //}
            //catch (DbUpdateException ex)
            //{
            //    var entry = ex.Entries.First().Entity;

            //    RollBackTransaction();

            //    string entryInfo = string.Empty;

            //    if (entry is IEntity iEntity)
            //        entryInfo = BuildInfo(entry.GetType(), iEntity.Id);

            //    throw CreateException(nameof(DbUpdateException), entryInfo, GetMessageFromInnerException(ex));
            //}
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private void RollBackChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();

            if (_dbContext.ChangeTracker.HasChanges())
            {
                var changes = _dbContext.ChangeTracker.Entries().Where(item => item.State != EntityState.Unchanged);

                if (changes != null)
                {
                    foreach (var item in changes)
                    {
                        var entity = item.Entity;

                        if (entity == null)
                            continue;

                        switch (item.State)
                        {
                            case EntityState.Added:
                                var set = _dbContext.Set(item.GetType());
                                set.Remove(item);
                                break;
                            case EntityState.Deleted:
                                item.State = EntityState.Unchanged;
                                break;
                            case EntityState.Modified:
                                item.CurrentValues.SetValues(item.OriginalValues);
                                item.State = EntityState.Unchanged;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        string GetMessageFromInnerException(Exception ex)
        {
            while (ex != null)
            {
                if (ex.InnerException == null)
                    return ex.Message;
                ex = ex.InnerException;
            }
            return string.Empty;
        }

        Exception CreateException(params string[] args)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var arg in args)
                builder.Append($"{arg}{Environment.NewLine}");

            return new Exception(builder.ToString());
        }

        string BuildInfo(Type type, int id) => string.Format("Type: {0}, Id: {1}", type.Name, id);

        public void KillDBContext()
        {
            _dbContext?.Dispose();
        }
    }
}
