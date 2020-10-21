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
using Layers.Data.DataAccess.Context;
using Layers.Base.Entities.Write;
using Layers.Base.Enums;
using System.Data.Entity;

namespace Layers.Service.Controllers
{
    [RoutePrefix("api/ContentRequirements")]
    public class ContentRequirementsController : BaseController<Read.ContentRequirement, Write.ContentRequirement, int>
    {
        private IContentRequirementsManager _ContentRequirementsManager { get; set; }
        public ContentRequirementsController(IContentRequirementsManager ContentRequirementsManager)
        {
            _ContentRequirementsManager = ContentRequirementsManager;
        }
        protected override IManager<Read.ContentRequirement, Write.ContentRequirement, int> Manager
        {
            get
            {
                return _ContentRequirementsManager;
            }
        }

        [Route("Add")]
        public override DescriptiveResponse<int> Post(ContentRequirement contentRequirement)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(contentRequirement);
        }

        [Route("Edit")]
        public DescriptiveResponse<int> Put(ContentRequirement contentRequirement)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(contentRequirement);
        }
    }
}
