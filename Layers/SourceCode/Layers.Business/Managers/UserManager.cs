using Read = Layers.Base.Entities.Read;
using Write = Layers.Base.Entities.Write;
using Layers.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers.Data.Contracts.Contracts;
using Layers.Business.Contracts.Base;
using Layers.Base.Entities;
using Layers.Base.Entities.DTO;
using Layers.Business.Mappers;

namespace Layers.Business.Managers
{
    public class UserManager : Manager<Read.User, Write.User, int, int>, IUserManager
    {

        #region Ctor
        public UserManager(IReadRepository<Read.User, int> readRepository, IWriteRepository<Write.User, int> writeRepository) : base(readRepository, writeRepository)
        {

        }

        public DescriptiveResponse<UserDTO> FilterUsers(Filtration filter)
        {
            // Find all items that match filteration
            Read.User user = _readRepository.Find(filter).Collection.FirstOrDefault();

            var userDTO = UserMapper.Instance.ToDTOObject(user);
            // Return success response
            return DescriptiveResponse<UserDTO>.Success(userDTO);
        }

        #endregion


    }
}
