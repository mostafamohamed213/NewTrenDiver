using Read = Layers.Base.Entities.Read;
using Write = Layers.Base.Entities.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Layers.Data.DataAccess.Repository;
using Layers.Data.Contracts.Contracts;
using Layers.Business.Managers;
using Layers.Business.Contracts.Base;
using Microsoft.Practices.Unity.Configuration;
using Layers.Base.Entities.DTO;
using Layers.Base.Entities;
using Layers.Utilities.Users;
using Layers.Base.Entities.Write;
using Layers.Data.DataAccess.Context;
using System.Data.Entity;

namespace Layers.Service.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseController<Read.User, Write.User, int>
    {
        private IUserManager _userManager { get; set; }
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        protected override IManager<Read.User, Write.User, int> Manager
        {
            get
            {
                return _userManager;
            }
        }
        [Route("GetUser")]
        [HttpPost]
        public DescriptiveResponse<UserDTO> GetUser(Filtration filter)
        {
            return _userManager.FilterUsers(filter);
        }


    }
}
