using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PicShopper.Web.Models;
using System.Data;
using System.Data.SqlClient;

namespace PicShopper.Web.Services
{
    public interface IUserData
    {
        Login DoLogin(string uname, string pass);
        bool ChangePassword(string uid, string newpassword);
    }
    public class UsersData : IUserData
    {
        public UsersData(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        private IConfiguration _configuration;

        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=PicShopperDB;" +
                                                      "Integrated Security=True;Persist Security Info=False;" +
                                                      "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                      "Encrypt=False;TrustServerCertificate=True");
        public Login DoLogin(string uname, string pass)
        {
            SqlCommand cmd      = con.CreateCommand();
            DataTable  table    = new DataTable();
            Login      user     = new Login();
            string     qrString = "SELECT * FROM tbl_users WHERE uname='" + uname + "' AND password='" + pass + "';";

            using (con)
            {
                using (cmd = new SqlCommand(qrString, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user.UserId   = (int)reader["u_id"];
                        user.UserName = (string)reader["lastname"];
                        user.UType    = (UserType)reader["group"];
                    }

                    return user;
                }
            }
        }

        public bool ChangePassword(string uid, string newpassword)
        {
            SqlCommand cmd   = con.CreateCommand();
            cmd.CommandType  = CommandType.StoredProcedure;
            cmd.CommandText  = "sp_changeUserPassword";
            cmd.Parameters.Add("@uId", SqlDbType.VarChar).Value = uid;
            cmd.Parameters.Add("@newPassword", SqlDbType.VarChar).Value = newpassword;
            try
            {
                con.Open();
                int success = cmd.ExecuteNonQuery();

                if(success == 0)
                {
                    return true;
                }
                return false;
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
