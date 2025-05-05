using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stick_Overflow.Pages
{
    public class logoutModel : PageModel
    {
        public string messaggio;
        protected void Page_Load(object sender, EventArgs e)
        {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("logged-in-id", null, cookie);

            messaggio = "function was called but it don't work";

            Response.Redirect("index");
        }
        public void OnGet()
        {
        }
    }
}
