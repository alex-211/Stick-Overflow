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

        [BindProperty]
        public bool rememberUser { get; set; }

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

                    string queryCheck = "SELECT u_id FROM utente WHERE u_nickname = @nickname OR u_email = @email";
                    using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@nickname", usr.name);
                        cmdCheck.Parameters.AddWithValue("@email", usr.email);
                        object ris = cmdCheck.ExecuteScalar();
                        if (ris != null)
                        {
                            messaggio = "username / email già in uso";
                        }
                    }
                    string insertQuery = "INSERT INTO utente (u_Id, u_nickname, u_password, u_email, u_abilitato) VALUES (@id, @name, @pswd, @email, 1)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", usr.Id);
                        cmd.Parameters.AddWithValue("@name", usr.name);
                        cmd.Parameters.AddWithValue("@pswd", usr.password);
                        cmd.Parameters.AddWithValue("@email", usr.email);
                        object ris = cmd.ExecuteNonQuery();
                        if (rememberUser == true)
                        {
                            CookieOptions cookie = new CookieOptions();
                            cookie.Expires = DateTime.Now.AddDays(60);
                            Response.Cookies.Append("logged-in-id", Convert.ToString(usr.Id), cookie);
                        }
                        else
                        {
                            HttpContext.Session.SetString("user-id", Convert.ToString(ris));
                        }

                        Response.Redirect("redirectBuffer?target=index");
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
