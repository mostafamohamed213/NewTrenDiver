using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    [DataContract]
    public class LookupObject<TId,TUId> where TId : IEquatable<TId>
                                        where TUId : struct
    {
        [DataMember]
        public TId Id { get; set; }
        [DataMember]
        public string UniqueKey { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        public Nullable<TUId> UserId { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public Dictionary<string,object> ExtraFields { get; set; }
        [DataMember]
        public LookupObjectType LookupObjectType { get; set; }

    }
}
