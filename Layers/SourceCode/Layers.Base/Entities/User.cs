using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    public class User<TUId> where TUId : struct
    {
        public string UserName { get; set; }
        public int UserType { get; set; }
        public TUId UserId  { get; set; }
        public CultureInfo Culture { get; set; }
    }
}
