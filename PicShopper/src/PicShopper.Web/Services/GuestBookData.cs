using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PicShopper.Web.Models;

namespace PicShopper.Web.Services
{
    public interface IGuestBookData
    {
        IEnumerable<GuestBook> GetComments();
        bool AddComment(GuestBook gb);
    }
    public class GuestBookData : IGuestBookData
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=WackoPickoDB;" +
                                                      "Integrated Security=True;Persist Security Info=False;" +
                                                      "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                      "Encrypt=False;TrustServerCertificate=True");

        public IEnumerable<GuestBook> GetComments()
        {
            List<GuestBook> coms = new List<GuestBook>();
            SqlCommand cmd = con.CreateCommand();
            DataTable table = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_getComments";

            try
            {
                con.Open();
                SqlDataAdapter da = null;
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                }

                foreach (DataRow row in table.Rows)
                {
                    GuestBook com = new GuestBook();
                    com.Name = row["usr_name"].ToString();
                    com.Comment = row["comment"].ToString();
                    coms.Add(com);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                con.Close();
            }

            return coms;
        }

        public bool AddComment(GuestBook newItem)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insertComments";
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = newItem.Name;
            cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = newItem.Comment;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                con.Close();
            }

            return true;
        }

    }
}
