using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PicShopper.Web.Services
{
    public interface IUserData
    {
        bool DoLogin(string uname, string pass);
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

        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=WackoPickoDB;" +
                                                      "Integrated Security=True;Persist Security Info=False;" +
                                                      "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                      "Encrypt=False;TrustServerCertificate=True");
        public bool DoLogin(string uname, string pass)
        {
            SqlCommand cmd = con.CreateCommand();
            DataTable table = new DataTable();
            string qrString = "SELECT * FROM Users WHERE name='" + uname + "' AND password='" + pass + "';";

            using (con)
            {
                using (cmd = new SqlCommand(qrString, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
