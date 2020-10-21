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
    [RoutePrefix("api/Lesson")]
    public class LessonController : BaseController<Read.Lessons, Write.Leason, int>
    {
        private ILessonManager _lessonManager { get; set; }
    public LessonController(ILessonManager lessonManager)
    {
            _lessonManager = lessonManager;
    }
    protected override IManager<Read.Lessons, Write.Leason, int> Manager
    {
        get
        {
                return _lessonManager;
        }
    }

    }
}
