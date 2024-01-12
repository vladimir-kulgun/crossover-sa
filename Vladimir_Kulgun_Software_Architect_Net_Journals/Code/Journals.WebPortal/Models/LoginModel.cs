using System.ComponentModel.DataAnnotations;

namespace Journals.WebPortal.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { set; get; }
    }
}