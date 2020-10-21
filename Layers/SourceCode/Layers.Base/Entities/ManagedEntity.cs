using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    /// <summary>
    /// Parent Type for all entities that manged by user,
    /// contains all basic propreties that should be exist for any managed entity
    /// </summary>
    /// <typeparam name="T">Type of Id property</typeparam>
    /// <typeparam name="F">Type of user Id</typeparam>
    public class ManagedEntity<T,F> : EntityBase<T> where F: struct
    {
        public virtual DateTime CreationDate { get; set; }
        public virtual F? CreatedBy { get; set; }
        public virtual Nullable<DateTime> LastModifiedDate { get; set; }
        public virtual F? LastModifiedBy { get; set; }

    }
}
