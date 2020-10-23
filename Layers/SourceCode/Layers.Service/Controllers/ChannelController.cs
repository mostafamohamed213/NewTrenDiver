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
using Layers.Base.Entities.Read;
using Layers.Service.Filters;

namespace Layers.Service.Controllers
{
    [RoutePrefix("api/Channel")]
    public class ChannelController : BaseController<Read.Channel, Write.Channel, int>
    {
        protected IReadRepository<Read.Channel, int> _readRepository;
        protected IWriteRepository<Write.Channel, int> _writeRepository;
       
        private IChannelManager _ChannelManager { get; set; }
        public ChannelController(IChannelManager ChannelManager,
            IReadRepository<Read.Channel, int> readRepository, IWriteRepository<Write.Channel, int> writeRepository)
        {
            _ChannelManager = ChannelManager;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        protected override IManager<Read.Channel, Write.Channel, int> Manager
        {
            get
            {
                return _ChannelManager;
            }
        }
        // Add Channel
        [Route("Add")]
      
        public override DescriptiveResponse<int> Post(Write.Channel channel)
        {
            if(!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(""+ModelState);
            }
            if(CheckName(channel.Name))
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.ALREADY_EXIST);
            }
            if (CheckChannelCategoryPerInstructor( channel.channelType,channel.CategoryId))
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.ALREADY_EXIST);
            }
            return Manager.Save(channel);
        }
        // Edit Channel
        [Route("Edit")]
        public  DescriptiveResponse<int> Put(Write.Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.INPUT_INVAILD);
            }
            if (CheckNameforedit(channel.Name,channel.Id))
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.ALREADY_EXIST);
            }
            if (CheckChannelCategoryPerInstructorforedit(channel.channelType, channel.CategoryId,channel.Id))
            {
                return DescriptiveResponse<int>.Error(ErrorStatus.ALREADY_EXIST);
            }
            return Manager.Save(channel);
        }

        //Get All Channel For this Content Provider
        [Route("GetChannelperUser/{userid}")]
        public DescriptiveResponse<IEnumerable<Read.Channel>> GetChannelperUser(int userid)
        {
            var channelsperuser= _readRepository.GetAll().Where(s=>s.UserId==userid).ToList();
            if(channelsperuser.Count==0)
            {
                return DescriptiveResponse<IEnumerable<Read.Channel>>.Error(ErrorStatus.NOT_FOUND);
            }
            return DescriptiveResponse<IEnumerable<Read.Channel>>.Success(channelsperuser);
        }


        // GeT top 20 Content for each Category and if user click specific type return top 20 for this type
        //types (livestream,Recorded,Webinar) or all
        [AllowAnonymous]
        [Route("GetTopContent/{ContentType}")]

        public DescriptiveResponse<IQueryable> GetTopContent(string ContentType)
        {
          var content= _ChannelManager.GetTopContent(ContentType);
            return DescriptiveResponse<IQueryable>.Success(content);
        }

        //[AllowAnonymous]
        //[Route("GetTopContent2")]

        //public DescriptiveResponse<IEnumerable<TopContentDTO>> GetTopContent2()
        //{
        //    var content = _ChannelManager.GetAlltopevent2();
        //    return DescriptiveResponse<IEnumerable<TopContentDTO>>.Success(content);
        //}

        //get all top 20 content for each Types

        [AllowAnonymous]
        [Route("GetContentsPerCategory/{CategoryID}")]

        public DescriptiveResponse<IQueryable> GetContentsPerCategory(int CategoryID,int page, int size)
        {
            var content = _ChannelManager.GetContentsPerCategory(CategoryID, page, size);
            return DescriptiveResponse<IQueryable>.Success(content);
        }

        //Check if the channel Name is exist or not -- if not exist you can create channel with this name 
        public bool CheckName(string Name)
        {
            if ((_readRepository.GetSingleOrDefault(s => s.Name == Name)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Check if the channel Name is exist or not -- if not exist you can edit channel with this name 
        public bool CheckNameforedit(string Name,int id)
        {
            if ((_readRepository.GetSingleOrDefault(s => s.Name == Name && s.Id!=id )) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //add channel and make a validation per type of channel(one instructor channel per content provider in the one category)
        // every instructor (content provider) has only  one Category per Type
        public bool CheckChannelCategoryPerInstructor(ChannelType channelType,int categoryid)
        {
            if (channelType == ChannelType.Instructor)
            {
                if ((_readRepository.GetSingleOrDefault(s => s.CategoryId == categoryid && s.channelType == ChannelType.Instructor)) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //edit channel and make a validation per type of channel(one instructor channel per content provider in the one category)
        // every instructor (content provider) has only  one Category per Type
        public bool CheckChannelCategoryPerInstructorforedit(ChannelType channelType, int categoryid,int  id)
        {
            if (channelType == ChannelType.Instructor)
            {
                if ((_readRepository.GetSingleOrDefault(s => s.CategoryId == categoryid && s.channelType== ChannelType.Instructor && s.Id!=id)) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //[Route("Add")]
        //public  IHttpActionResult Posts(Channel entity)
        //{

        //    //if (_readRepository.GetSingleOrDefault(s=>s.Name==entity.Name)!=null)
        //    //  {

        //    //  }
        //     _writeRepository.Attach(entity);
        //after this unit of work .save()
        //    return Ok();
        //}
        //[Route("Addch")]
        //public IHttpActionResult Postch(Channel channel)
        //{
        //    WriteContext writeContext = new WriteContext();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    writeContext.Channel.Add(channel);

        //    writeContext.SaveChanges();
        //    return Ok();
        //}

    }
}