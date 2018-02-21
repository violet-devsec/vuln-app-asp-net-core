using PicShopper.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.Services
{

    public interface IPicViewData
    {
        PicView GetPic(int id);
    }
    public class PicViewData : IPicViewData
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=PicShopperDB;" +
                                                 "Integrated Security=True;Persist Security Info=False;" +
                                                 "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                 "Encrypt=False;TrustServerCertificate=True");
        public PicView GetPic(int picId)
        {
            PicView    pic   = new PicView();
            SqlCommand cmd   = con.CreateCommand();
            cmd.CommandType  = CommandType.StoredProcedure;
            cmd.CommandText  = "sp_getPicture";
            cmd.Parameters.Add("@picId", SqlDbType.VarChar).Value = picId;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pic.Title  = (string)reader["title"];
                    pic.Name   = (string)reader["file_name"];
                    pic.Price  = (int)reader["price"];
                    return pic;
                }
                return null;
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
            
        }
    }
}
