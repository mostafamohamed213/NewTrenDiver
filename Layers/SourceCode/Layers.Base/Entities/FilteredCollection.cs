using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    public class FilteredCollection<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Collection { get; set; }
        public int TotalCount { get; set; }

    }
}
