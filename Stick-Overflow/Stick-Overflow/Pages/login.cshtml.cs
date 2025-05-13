using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Stick_Overflow.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public string pswd { get; set; }

        [BindProperty]
        public string loginParam { get; set; }

        [BindProperty]
        public bool rememberUser { get; set; }

        public string messaggio;
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                messaggio = "cacca";
                return;
            }

            try
            {
                const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connData))
                {
                    conn.Open();
                    string query = " ";
                    if (!loginParam.Contains("@"))
                    {
                        // query che contiene nickname
                        query = "SELECT u_Id FROM utente WHERE u_nickname = @loginParam AND u_password = @password AND u_abilitato = 1";
                    }
                    else
                    {
                        // contiene email
                        query = "SELECT u_Id FROM utente WHERE u_email = @loginParam AND u_password = @password AND u_abilitato = 1";
                    }
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@loginParam", loginParam);
                        cmd.Parameters.AddWithValue("@password", pswd);

                        object ris = cmd.ExecuteScalar();
                        if (ris == null)
                        {
                            messaggio = "cacca non posso loggarmi";
                            return;
                        }
                        else
                        {
                            if (rememberUser == true)
                            {
                                CookieOptions cookie = new CookieOptions();
                                cookie.Expires = DateTime.Now.AddDays(30);
                                Response.Cookies.Append("logged-in-id", Convert.ToString(ris), cookie);
                            }
                            else
                            {
                                HttpContext.Session.SetString("user-id", Convert.ToString(ris));
                            }

                            Response.Redirect("redirectBuffer?target=index");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                messaggio = "cacca : " + ex;
            }
        }
    }
}

