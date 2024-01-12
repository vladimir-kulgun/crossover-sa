using System.Web;

namespace Journals.WebPortal.Services
{
    public interface IFileUploadService
    {
        byte[] CreateContent(HttpPostedFileBase journalFile);
    }
}