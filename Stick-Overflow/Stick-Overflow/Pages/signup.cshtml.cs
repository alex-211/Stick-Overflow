using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;

namespace Stick_Overflow.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        public User usr { get; set; }
        public string messaggio;

        [BindProperty]
        public string password_conf { get; set; } // non ne sono sicuro da rivedere // update: sembra funzionare

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                messaggio = "cacati";
                return;
            }

            try
            {
                if (usr.password != password_conf)
                {
                    messaggio = "Le due password non combaciano";
                    return;
                }
                const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connData))
                {
                    conn.Open();
                    string query = "SELECT MAX(u_Id) FROM utente";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object ris = cmd.ExecuteScalar();
                        if (ris == DBNull.Value || ris == null)
                        {
                            usr.Id = 1;
                        }
                        else
                        {
                            usr.Id = Convert.ToInt32(ris) + 1;
                        }
                    }

                    string insertQuery = "INSERT INTO utente (u_Id, u_nickname, u_password, u_email, u_abilitato) VALUES ('" + usr.Id + "', '" + usr.name + "', '" + usr.password + "', '" + usr.email + "', 1)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        object ris = cmd.ExecuteNonQuery();
                        CookieOptions cookie = new CookieOptions();
                        cookie.Expires = DateTime.Now.AddDays(60);
                        Response.Cookies.Append("logged-in-id", Convert.ToString(usr.Id), cookie);

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
            catch (SqlException ex)
            {
                messaggio = "cacca nel puzzo: " + ex;
            }
        }
    }
}
