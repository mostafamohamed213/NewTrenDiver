using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_User")]
    public class User :EntityBase<int> , IReadEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UserType { get; set; }

        [NotMapped]
        public override bool IsDeleted { get => base.IsDeleted; set => base.IsDeleted = value; }
    }
}
