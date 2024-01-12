using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Journals.Dom.Models;
using Journals.Dom.Services;
using Journals.WebPortal.Exceptions;
using Journals.WebPortal.Services;
using Journals.WebPortal.ViewModel;
using PagedList;

namespace Journals.WebPortal.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IJournalService _journalService;

        public PublisherController(IFileUploadService fileUploadService, IJournalService journalService)
        {
            _fileUploadService = fileUploadService;
            _journalService = journalService;
        }

        public ActionResult Index(int? page)
        {
            var journals = _journalService.FindJounalsByPublisher(1);

            var viewModels = Mapper.Map<IEnumerable<JournalViewModel>>(journals);

            var pageNumber = (page ?? 1);
            return View("Index", viewModels.ToPagedList(pageNumber, 10));
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
                _journalService.DeleteJournal(id.Value, 1);

            return RedirectToAction("Index");
        }

        public ActionResult FileUpload()
        {
            return View("FileUpload");
        }

        [HttpPost]
        public ActionResult FileUpload(FileUploadViewModel viewModel)
        {
            try
            {
                var content = _fileUploadService.CreateContent(viewModel.Files);
                _journalService.CreateJounal(viewModel.Name, content, 1);
            }
            catch (FileUploadException ex)
            {
                ViewBag.FileStatus = ex.Message;
                return View("FileUpload");

            }

            return RedirectToAction("Index");
        }
    }
}