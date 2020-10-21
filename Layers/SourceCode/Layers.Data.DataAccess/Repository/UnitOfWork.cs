using Layers.Data.Contracts.Contracts;
using Layers.Data.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members

        private DbContext _context;
        private DbContextTransaction _transactionContext;

        #endregion

        #region Ctor

        #endregion

        #region IUnitOfWork

        public DbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new ReadContext();
                }
                return _context;
            }
        }

        public void Commit()
        {
            if (_transactionContext != null)
                _transactionContext.Commit();
        }

        public void RollBack()
        {
            if (_transactionContext != null)
                _transactionContext.Rollback();
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void StartTransaction()
        {
            _transactionContext = Context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        #endregion

        #region 
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }

            if (_transactionContext != null)
            {
                _transactionContext.Dispose();
            }
        }

        #endregion

    }
}
