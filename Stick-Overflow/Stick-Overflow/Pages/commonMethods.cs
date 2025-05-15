using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

namespace Stick_Overflow.Pages
{
    public class commonMethods
    {
        public string[] getUserDetails(string id)
        {
            const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                const string query = "SELECT u_nickname, u_email, u_abilitato FROM utente WHERE u_id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nickname = reader[0].ToString();
                            string email = reader[1].ToString();
                            string abilitato = reader[2].ToString();

                            return new string[] { nickname, email, abilitato };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public SqlDataReader getUserMessages(string userId)
        {
            const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                // aggiungere anche group by e order by?
                const string query = "SELECT m.m_Id, m.m_titolo, m.m_testo, m.m_data, f.f_titolo, u.u_nickname, d.d_titolo FROM messaggio AS m, forum AS f, utente AS u WHERE m.d_Id = d.d_Id AND d.f_Id = f.f_Id AND m.u_Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader;
                    }
                }
            }
        }

        public void whoIsLogged()
        {
            // soon
        }

        public bool isAdmin(string id)
        {
            const string connData = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + "|DataDirectory|\\forum.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                const string query = "SELECT u_Id FROM amministratore WHERE u_Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    object ris = cmd.ExecuteScalar();
                    if (ris != null)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
    }
}
