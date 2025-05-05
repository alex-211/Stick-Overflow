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
                        query = "SELECT u_Id FROM utente WHERE u_nickname = '" + loginParam + "' AND u_password = '" + pswd + "'";
                    }
                    else
                    {
                        // contiene email
                        query = "SELECT u_Id FROM utente WHERE u_email = '" + loginParam + "' AND u_password = '" + pswd + "'";
                    }
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object ris = cmd.ExecuteScalar();
                        if (ris == DBNull.Value || ris == null)
                        {
                            messaggio = "cacca non posso loggarmi";
                            return;
                        }
                        else
                        {
                            CookieOptions cookie = new CookieOptions();
                            cookie.Expires = DateTime.Now.AddDays(60);
                            Response.Cookies.Append("logged-in-id", Convert.ToString(ris), cookie);

                            if (Request.IsHttps)
                            {
                                Response.Redirect("https://localhost:5033/index");
                            }
                            else
                            {
                                Response.Redirect("http://localhost:5033/index");
                            }
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

