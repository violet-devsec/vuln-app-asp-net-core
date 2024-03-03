using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace PicShopper.Web.Services
{
    public interface IUploads
    {
        Task<bool> Upload(IFormFile fileContent, string title, int price, string userId);
    }

    public class Uploads : IUploads
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=PicShopperDB;" +
                                                     "Integrated Security=True;Persist Security Info=False;" +
                                                     "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                     "Encrypt=False;TrustServerCertificate=True");

        private IHostingEnvironment hostingEnvironment = null;

        public Uploads(IHostingEnvironment environment)
        {
            this.hostingEnvironment = environment;
        }

        private bool AddFileDetailsToDB(string title, int price, string userId, string fileName)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insertPictureDetails";
            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            cmd.Parameters.Add("@width", SqlDbType.Int).Value = 144;
            cmd.Parameters.Add("@height", SqlDbType.Int).Value = 144;
            cmd.Parameters.Add("@file", SqlDbType.VarChar).Value = fileName;
            cmd.Parameters.Add("@price", SqlDbType.Int).Value = price;
            cmd.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(userId);
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

        public async Task<bool> Upload(IFormFile content, string title, int price, string user)
        {
            string path = Path.Combine(this.hostingEnvironment.WebRootPath, "images\\uploads", content.FileName);
            
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await content.CopyToAsync(stream);
            }

            AddFileDetailsToDB(title, price, user, content.FileName);

            return true;
        }
    }
}
