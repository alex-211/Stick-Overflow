using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Stick_Overflow.Pages
{
    public class viewForumModel : PageModel
    {
        [BindProperty]
        public string param { get; set; }
        [BindProperty]
        public string id { get; set; }

        public void OnPost()
        {
            Response.Redirect("/viewForum?forum=" + id + "&src=" + param);
        }
    }
}
