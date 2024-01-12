using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Journals.Dom.Models;
using Journals.Dom.Services;
using Journals.WebPortal.Exceptions;
using Journals.WebPortal.ViewModel;
using PagedList;

namespace Journals.WebPortal.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly IJournalService _journalService;
        private int _subscriberId = 2;

        public SubscriberController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        public ActionResult Index(int? page, string searchString)
        {
            var subsribedJournals = _journalService.FindJounalsBySubscriber(_subscriberId);
            var journals = _journalService.ListJournals(searchString);

            var viewModels = Mapper.Map<IEnumerable<JournalViewModel>>(journals);
            foreach (var vm in viewModels)
            {
                vm.IsSubscribed = subsribedJournals.FirstOrDefault(x => x.Id == vm.Id) != null;
            }

            var pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View("Index", viewModels.ToPagedList(pageNumber, 10));
        }

        public ActionResult Subscribe(int? id, int? page, string searchString)
        {
            if (id.HasValue)
                _journalService.SubscribeJournal(id.Value, _subscriberId);

            return RedirectToAction("Index", new { page, searchString});
        }

        public ActionResult Unsubscribe(int? id, int? page, string searchString)
        {
            if (id.HasValue)
                _journalService.UnsubscribeJournal(id.Value, _subscriberId);

            return RedirectToAction("Index", new { page, searchString });
        }
    }
}