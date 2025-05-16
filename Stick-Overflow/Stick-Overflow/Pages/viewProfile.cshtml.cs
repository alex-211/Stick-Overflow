using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stick_Overflow.Pages
{
    public class viewProfileModel : PageModel
    {
        [BindProperty]
        public string nick { get; set; }

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string oldPswd { get; set; }
        public void OnGet()
        {
        }
    }
}
