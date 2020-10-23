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
    [RoutePrefix("api/Content")]
    public class ContentController : BaseController<Read.Content, Write.Content, int>
    {
        protected IReadRepository<Read.Content, int> _readRepository;
        protected IWriteRepository<Write.Content, int> _writeRepository;

        private IContentManager _ContentManager { get; set; }
        public ContentController(IContentManager ContentManager,
            IReadRepository<Read.Content, int> readRepository, IWriteRepository<Write.Content, int> writeRepository)
        {
            _ContentManager = ContentManager;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        protected override IManager<Read.Content, Write.Content, int> Manager
        {
            get
            {
                return _ContentManager;
            }
        }

        //Add Content 
        [Route("Add")]
        public override DescriptiveResponse<int> Post(Content content)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(content);
        }

        //Edit Content
        [Route("Edit")]
        public DescriptiveResponse<int> Put(Content content)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            return Manager.Save(content);
        }
        // show content for user so [AllowAnonymous]  - end user flow
        [AllowAnonymous]
        [Route("GetContentForUser/{id}")]
        public override DescriptiveResponse<Read.Content> Get(int id)
        {
            return Manager.GetItem(id);
        }
        //Get all Contents for this Channel  for user(Content provider)
        [Route("GetContentperChannel/{channelid}")]
        public DescriptiveResponse<IEnumerable<Read.Content>> GetChannelperUser(int channelid)
        {
            var contents = _readRepository.GetAll().Where(s => s.ChannelId == channelid).ToList();
            if (contents.Count == 0)
            {
                return DescriptiveResponse<IEnumerable<Read.Content>>.Error(ErrorStatus.NOT_FOUND);
            }
            return DescriptiveResponse<IEnumerable<Read.Content>>.Success(contents);
        }
        //   [AllowAnonymous] just for test but must be autherize

    }
}