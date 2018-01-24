using PicShopper.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.Services
{
    public interface IRecent
    {
        IEnumerable<RecentFile> GetRecentFiles();
    }
    public class Recent : IRecent
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=PicShopperDB;" +
                                                     "Integrated Security=True;Persist Security Info=False;" +
                                                     "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                     "Encrypt=False;TrustServerCertificate=True");

        public IEnumerable<RecentFile> GetRecentFiles()
        {
            List<RecentFile> recs = new List<RecentFile>();
            SqlCommand cmd        = con.CreateCommand();
            DataTable table       = new DataTable();
            cmd.CommandType       = CommandType.StoredProcedure;
            cmd.CommandText       = "sp_getRecentPictures";

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
                    RecentFile rec = new RecentFile();
                    rec.Title      = row["title"].ToString();
                    rec.Name       = row["file_name"].ToString();
                    rec.Price      = (int)row["price"];
                    recs.Add(rec);
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

            return recs;
        }
    }
}
