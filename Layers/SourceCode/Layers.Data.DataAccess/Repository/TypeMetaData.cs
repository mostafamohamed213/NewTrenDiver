using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.DataAccess.Repository
{
    internal sealed class TypeMetaData
    {
        public PropertyInfo IdProperty { get; set; }
        public PropertyInfo IsDeletedProperty { get; set; }
        public object IdDefaultValue { get; set; }
    }
}
