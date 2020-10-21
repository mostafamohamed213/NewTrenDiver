using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    public class LookupBase<T> : EntityBase<T>
    {
        public virtual string  UniqueKey { get; set; }

    }
}
