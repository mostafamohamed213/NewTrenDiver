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
using Layers.Data.DataAccess.Context;
using Layers.Base.Entities.Write;

namespace Layers.Business.Managers
{
    public class ChannelManager : Manager<Read.Channel, Write.Channel, int, int>, IChannelManager
    {

        #region Ctor
        ReadContext db = new ReadContext();
        public ChannelManager(IReadRepository<Read.Channel, int> readRepository, IWriteRepository<Write.Channel, int> writeRepository) : base(readRepository, writeRepository)
        {

        }


        #endregion
        // GeT top 20 Content for each Category and if user click specific type return top 20 for this type
        //types (livestream,Recorded,Webinar) or all
        public IQueryable GetTopContent(string ContentType)
        {
            contenttype type;
            if (ContentType == "Livestream")
            {
                type = contenttype.livestram;
            }

            else if (ContentType == "Recorded")
            {

                type = contenttype.recorded;
            }

            else if (ContentType == "Webinar")
            {
                type = contenttype.webinar;
            }
            else if (ContentType == "All")
            {
                return GetAlltopContent();
            }
            else
            {
                return GetAlltopContent();
            }
           var Query = db.CategoryLookup.Select(c => new
            {
                CategoryName = c.UniqueKey,

                Contents = (from a in c.Channels
                            join b in db.Content
                            on a.Id equals b.ChannelId
                            where b.Type==type
                            select new
                            {
                                Channel = a.Name,
                                Content = b.Title
                            ,
                                ContentType = b.Type,
                                Price = b.Price


                            }).Take(2)
            });
            return Query;

        }

        //get all top 20 content for each Types
        public IQueryable GetAlltopContent()
        {
            var Query = db.CategoryLookup.Select(c => new
            {
                CategoryName = c.UniqueKey,

                Contents = (from a in c.Channels
                            join b in db.Content
                            on a.Id equals b.ChannelId

                            select new
                            {
                                Channel = a.Name,
                                Content = b.Title
                            ,
                                ContentType = b.Type,
                                Price = b.Price


                            }).Take(2)
            });
            return Query;
        }

        //localization Query
        //CategoryName = c.CategoryLookupLocalize.Join(db.LanguageLookup.Where(w=>w.Abbreviation=="ar"),
        //       s=>s.LanguageId,
        //       ll=>ll.Id,
        //       (s,ll)=> new { s.Name })

        //public IEnumerable<TopContentDTO> GetAlltopevent2()
        //{
        //    IEnumerable<TopContentDTO> data = db.CategoryLookup.Select(c => new TopContentDTO
        //    {
        //        CategoryName = c.UniqueKey,

        //        contents = (List<Read.Content>)(from a in c.Channels
        //                                        join b in db.Content
        //                                        on a.Id equals b.ChannelId

        //                                        select new TopContentDTO
        //                                        {
        //                                            ChannelName = a.Name,
        //                                            ContentName = b.Title
        //                                        ,
        //                                            Contenttype = b.Type,
        //                                            Price = b.Price


        //                                        }).Take(2)
        //    });

        //    return data;
        //}

        // Get Contents per Category (front end will send page number and how many contnt will be in this page)
        public IQueryable GetContentsPerCategory(int categoryid, int pagenumber, int sizeofcontents)
        {
            var Query = db.Content.Join(db.Channel.Where(pp => pp.CategoryId == categoryid), e => e.ChannelId, p => p.Id,
    (e, p) => new
    {
        p.Name,
        e.Title,
        e.Type,
        e.Price,
        e.CreationDate


    }).OrderByDescending(s => s.CreationDate).Skip((pagenumber - 1) * sizeofcontents).Take(sizeofcontents);
            return Query;
        }
    }
}