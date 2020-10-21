using Read = Layers.Base.Entities.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write = Layers.Base.Entities.Write;
using Layers.Base.Entities.DTO;
using Layers.Base.Entities;

namespace Layers.Business.Contracts.Base
{
    public interface IUserManager : IManager<Read.User, Write.User,int>
    {
        DescriptiveResponse<UserDTO> FilterUsers(Filtration filter);

    }
}
