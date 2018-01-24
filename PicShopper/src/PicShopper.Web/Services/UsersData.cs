using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PicShopper.Web.Services
{
    public interface IUserData
    {
        int DoLogin(string uname, string pass);
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
        public int DoLogin(string uname, string pass)
        {
            SqlCommand cmd      = con.CreateCommand();
            DataTable  table    = new DataTable();
            string     qrString = "SELECT * FROM tbl_users WHERE uname='" + uname + "' AND password='" + pass + "';";

            using (con)
            {
                using (cmd = new SqlCommand(qrString, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userid = (int)reader["u_id"];
                        return userid;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
