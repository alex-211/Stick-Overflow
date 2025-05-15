using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stick_Overflow.Pages
{
    public class database_visualiserModel : PageModel
    {
        public IActionResult OnGet()
        {
            string id = Request.Cookies["logged-in-id"] ?? HttpContext.Session.GetString("user-id");
            commonMethods cm = new commonMethods();
            if (!cm.isAdmin(id) || id == null)
            {
                return Unauthorized(); // Restituisce 401
            }
            return Page();
        }
    }
}
