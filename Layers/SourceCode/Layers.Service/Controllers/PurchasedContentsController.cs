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
    [RoutePrefix("api/PurchasedContents")]
    public class PurchasedContentsController : BaseController<Read.PurchasedContents, Write.PurchasedContents, int>
    {
        private IPurchasedContentsManager _PurchasedContentsManager { get; set; }
        public PurchasedContentsController(IPurchasedContentsManager PurchasedContentssManager)
        {
            _PurchasedContentsManager = PurchasedContentssManager;
        }
        protected override IManager<Read.PurchasedContents, Write.PurchasedContents, int> Manager
        {
            get
            {
                return _PurchasedContentsManager;
            }
        }

        [AllowAnonymous]
        [Route("GetPurchasedContents/{userid}")]
        public DescriptiveResponse<IQueryable> GetPurchasedContents(int userid)
        {
            var PurchasedContents = _PurchasedContentsManager.GetPurchasedContents(userid);

            return DescriptiveResponse<IQueryable>.Success(PurchasedContents);
        }

    }
}
