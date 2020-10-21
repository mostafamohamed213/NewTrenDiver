using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.Contracts.Contracts
{
   public interface IReadUnitOfWork : IDisposable
    {
        object Context { get;}
    }
}
