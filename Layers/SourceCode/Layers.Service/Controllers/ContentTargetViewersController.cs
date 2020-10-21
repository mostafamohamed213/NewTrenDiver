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
    [RoutePrefix("api/ContentTargetViewers")]
    public class ContentTargetViewersController : BaseController<Read.ContentTargetViewer, Write.ContentTargetViewer, int>
    {
        private IContentTargetViewersManager _ContentTargetViewersManager { get; set; }
        public ContentTargetViewersController(IContentTargetViewersManager ContentTargetViewersManager)
        {
            _ContentTargetViewersManager = ContentTargetViewersManager;
        }
        protected override IManager<Read.ContentTargetViewer, Write.ContentTargetViewer, int> Manager
        {
            get
            {
                return _ContentTargetViewersManager;
            }
        }

        [Route("Add")]
        public override DescriptiveResponse<int> Post(ContentTargetViewer contentTargetViewer)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(contentTargetViewer);
        }

        [Route("Edit")]
        public DescriptiveResponse<int> Put(ContentTargetViewer contentTargetViewer)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(contentTargetViewer);
        }
    }
}
