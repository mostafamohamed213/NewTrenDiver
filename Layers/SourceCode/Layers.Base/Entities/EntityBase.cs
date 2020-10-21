using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    /// <summary>
    /// Base type for all domain entities
    /// </summary>
    /// <typeparam name="T">Type of Id property</typeparam>
    public abstract class EntityBase<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
