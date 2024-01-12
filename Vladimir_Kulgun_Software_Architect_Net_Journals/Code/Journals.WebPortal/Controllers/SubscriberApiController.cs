using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Journals.Dom.Services;
using Journals.WebPortal.Filters;
using Journals.WebPortal.Models;

namespace Journals.WebPortal.Controllers
{
    [ValidateModel]
    [ArgumentNullExceptionFilter]
    [System.Web.Http.RoutePrefix("api/subscribers")]
    public class SubscriberApiController : ApiController
    {
        private readonly ISubscriberServices _subscriberServices;

        public SubscriberApiController(ISubscriberServices subscriberServices)
        {
            _subscriberServices = subscriberServices;
        }

        [System.Web.Http.Route("{subscriberId}/journals")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetJournals([FromUri] int subscriberId)
        {
            var models = _subscriberServices.FindJounalsBySubscriber(subscriberId);

            return Ok(Mapper.Map<IEnumerable<JournalModel>>( models));
        }

        [System.Web.Http.Route("{subscriberId}/journals/{journalId}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetJournal([FromUri] int subscriberId, [FromUri] int journalId)
        {
            var content = _subscriberServices.FindJounalBySubscriber(subscriberId, journalId);
            return content != null ? (IHttpActionResult) Ok(content) : NotFound();
        }
        
        [System.Web.Http.Route("login")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            var subscriber = _subscriberServices.FindSubscriber(model.UserName);

            return subscriber != null ? (IHttpActionResult) Ok(subscriber.Id) : NotFound();
        }
    }
}