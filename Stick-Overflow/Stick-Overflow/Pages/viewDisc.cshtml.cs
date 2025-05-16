using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace Stick_Overflow.Pages
{
    public class viewDiscModel : PageModel
    {
        [BindProperty]
        public string reply { get; set; }

        [BindProperty]
        public string id { get; set; }

        public void OnPost()
        {
            string mid = "";
            string usrId = HttpContext.Request.Cookies["logged-in-id"] ?? HttpContext.Session.GetString("user-id");
            const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                const string maxQuery = "SELECT MAX(m_Id) FROM messaggio";
                using (SqlCommand cmd = new SqlCommand(maxQuery, conn))
                {
                    object ris = cmd.ExecuteScalar();
                    if (ris == DBNull.Value || ris == null)
                    {
                        mid = "1";
                    }
                    else
                    {
                        mid = ris.ToString() + 1;
                    }
                }


                const string query = "INSERT INTO messaggio(m_Id, m_titolo, m_testo, m_data, u_Id, d_Id) VALUES(@mid, ' ', @testo, CURRENT_TIMESTAMP, @uid, @did)"; // @data da sistemare, se non funziona in sql allora possiamo passare come param una var datetime di c#
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mid", mid);
                    cmd.Parameters.AddWithValue("@testo", reply);
                    cmd.Parameters.AddWithValue("@uid", usrId);
                    cmd.Parameters.AddWithValue("@did", id);

                    cmd.ExecuteNonQuery();
                    Response.Redirect("/viewDisc?disc=" + id);
                }
            }

        }
    }
}
