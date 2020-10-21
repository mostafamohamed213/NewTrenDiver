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
    [RoutePrefix("api/ContentGoal")]
    public class ContentGoalController : BaseController<Read.ContentGoal, Write.ContentGoal, int>
    {
        private IContentGoalManager _ContentGoalsManager { get; set; }
        public ContentGoalController(IContentGoalManager ContentGoalsManager)
        {
            _ContentGoalsManager = ContentGoalsManager;
        }
        protected override IManager<Read.ContentGoal, Write.ContentGoal, int> Manager
        {
            get
            {
                return _ContentGoalsManager;
            }
        }
        [Route("Add")]
        public override DescriptiveResponse<int> Post(ContentGoal contentgoal)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(contentgoal);
        }

        [Route("Edit")]
        public DescriptiveResponse<int> Put(ContentGoal contentgoal)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(contentgoal);
        }
    }
}

