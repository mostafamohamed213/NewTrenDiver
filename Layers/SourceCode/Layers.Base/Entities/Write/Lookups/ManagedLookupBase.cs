using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    /// <summary>
    /// Parent Type for all lookups that manged by user,
    /// contains all basic propreties that should be exist for any managed lookups
    /// </summary>
    /// <typeparam name="T">Type of Id property</typeparam>
    /// <typeparam name="F">Type of user Id</typeparam>
    public class ManagedLookupBase<T,F> : LookupBase<T>
    {
        public virtual DateTime CreationDate { get; set; }
        public virtual F CreatedBy { get; set; }
        public virtual Nullable<DateTime> LastModifiedDate { get; set; }
        public virtual F LastModifiedBy { get; set; }
    }
}
