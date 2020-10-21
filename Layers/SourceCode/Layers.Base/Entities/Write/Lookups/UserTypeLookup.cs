using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    [Table("LK_UserType")]
    public class UserTypeLookup : LookupBase<int>
    {
        public virtual List<UserTypeLookupLoclize> UserTypeLookupLoclize { get; set; }
    }

    [Table("LK_UserType_Local")]
    public class UserTypeLookupLoclize : LookupLoclizeBase<int>
    {
        [ColumnAttribute("UserTypeId")]
        public int UserTypeLookupId { get; set; }
    }


}
