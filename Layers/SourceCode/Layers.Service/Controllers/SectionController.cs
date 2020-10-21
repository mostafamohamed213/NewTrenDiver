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
    [RoutePrefix("api/Section")]
    public class SectionController : BaseController<Read.Section, Write.Section, int>
    {
    

        private ISectionManager _SectionManager { get; set; }
        public SectionController(ISectionManager SectionManager)
        {
            _SectionManager = SectionManager;
        }

        protected override IManager<Read.Section, Write.Section, int> Manager
        {
            get
            {
                return _SectionManager;
            }
        }

        [Route("Add")]
        public override DescriptiveResponse<int> Post(Section Section)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(Section);
        }

        [Route("Edit")]
        public DescriptiveResponse<int> Put(Section Section)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(Section);
        }
    }
}