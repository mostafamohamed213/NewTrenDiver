
using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyncService;
using Layers.Data.Contracts.Contracts;
using Layers.Data.DataAccess.Context;
using Layers.Utilities.Extensions;

namespace Layers.Data.DataAccess.Repository
{
    public class WriteUnitOfWork : IWriteUnitOfWork
    {

        #region Members
        private bool _isDisposed = false;

        private DbContext _context;
        private DbContextTransaction _transactionContext;

        #endregion

        #region Properties
        public object Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new WriteContext();
                }

                return _context;
            }
        }
        #endregion

        #region IWriteUnitOfWork
        public void Commit()
        {
            if (_transactionContext != null)
            {
                _transactionContext.Commit();
            }
        }


        public void RollBack()
        {
            if (_transactionContext != null)
            {
                _transactionContext.Rollback();
            }
        }


        public bool SaveChanges()
        {
            //Get changed Entities 
            List<Tuple<object, Operation>> changedEntities = ((DbContext)Context).ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added ||
                                                                                                                         entry.State == EntityState.Modified ||
                                                                                                                         entry.State == EntityState.Deleted)
                                                                                 .Select(entry => new Tuple<object, Operation>(entry.Entity, entry.State.ToOperation())).ToList();
            //Comit changes
            bool success = ((DbContext)Context).SaveChanges() > 0;

            if (success)
            {
                foreach (var entity in changedEntities)
                {
                    Type targetType = entity.Item1.GetType();

                    // if entity type contains numbers so entity is proxy genrated from EF
                    if (targetType.Name.ToCharArray().Any(ch => char.IsNumber(ch)))
                    {
                        targetType = entity.Item1.GetType().BaseType;
                    }

                    // parallel
                    typeof(SyncService.SyncService).GetMethod("OperationDone").MakeGenericMethod(targetType)
                                                   .Invoke(null, new object[] { entity.Item2, entity.Item1 });
                }
            }

            return success;
        }

        public void StartTransaction()
        {

            _transactionContext = _context.Database.BeginTransaction();
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _context?.Dispose();
            }
        }
        #endregion
    }
}
