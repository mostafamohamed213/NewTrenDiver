using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Contracts
{
    public interface IEntity<T>
    {
         T Id { get; set; }
         bool IsDeleted { get; set; }
    }
}
