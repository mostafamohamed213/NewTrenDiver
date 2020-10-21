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
    public class ReadUnitOfWork : IReadUnitOfWork
    {
        #region Members

        private bool _isDisposed = false;
        private DbContext _context;

        #endregion

        #region Properties

        public object Context
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
