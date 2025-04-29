using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Stick_Overflow.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        public User usr { get; set; }
        public string messaggio;

        [BindProperty]
        public string password_conf { get; set; } // non ne sono sicuro da rivedere
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                messaggio = "cacati";
                return;
            }

            try
            {
                const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connData))
                {
                    string query = "SELECT MAX id FROM utente";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object ris = cmd.ExecuteNonQuery;
                        if (ris == null)
                        {
                            usr.Id = 1;
                        }
                        else
                        {
                            usr.Id = Convert.ToInt32(ris) + 1;
                        }
                    }

                    string insertQuery = "INSERT INTO utente VALUES ()";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        object ris = cmd.ExecuteNonQuery;
                        messaggio = "Signup completato, ora è possibile loggarsi";
                        // redirect to login page or directly atore user's id in sessione storage
                    }
                }
            }
            catch (SqlException ex)
            {
                messaggio = "cacca" + ex;
            }
        }
    }
}
