using Read = Layers.Base.Entities.Read;
using Layers.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers.Base.Entities.DTO;

namespace Layers.Business.Mappers
{
    internal class UserMapper : MapperBase<Read.User, UserDTO>
    {
        #region Members
        private static UserMapper _instance;
        #endregion

        #region Properties

        public static UserMapper Instance
        {
            get
            {

                if (_instance == null)
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserMapper();
                        }
                    }
                return _instance;
            }
        }

        #endregion

        #region Ctor

        private UserMapper()
        {
            CreateMap(typeof(Read.User), typeof(UserDTO));
            CreateMap(typeof(UserDTO), typeof(Read.User));
        }

        #endregion
    }
}
