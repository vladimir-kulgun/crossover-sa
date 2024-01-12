using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Journals.WebPortal.ViewModel
{
    public class FileUploadViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Journal name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public HttpPostedFileBase Files { get; set; }
    }
}