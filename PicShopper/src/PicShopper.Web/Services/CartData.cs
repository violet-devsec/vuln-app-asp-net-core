using PicShopper.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.Services
{
    public interface ICartData
    {
        IEnumerable<Cart> GetCart(int uid);
        bool AddToCart();
    }
    public class CartData : ICartData
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=PicShopperDB;" +
                                                      "Integrated Security=True;Persist Security Info=False;" +
                                                      "Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;" +
                                                      "Encrypt=False;TrustServerCertificate=True");

        public IEnumerable<Cart> GetCart(int userId)
        {
            List<Cart> carts = new List<Cart>();
            SqlCommand cmd   = con.CreateCommand();
            DataTable  table = new DataTable();
            cmd.CommandType  = CommandType.StoredProcedure;
            cmd.CommandText  = "sp_getUserCart";
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

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
                    Cart cart     = new Cart();
                    cart.ItemName = row["item_name"].ToString();
                    cart.Price    = (int)row["item_price"];
                    cart.Count    = (int)row["item_count"];
                    carts.Add(cart);
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
            
            return carts;
        }
        
        // Adds items to cart
        public bool AddToCart()
        {
            return true;
        }
    }
}
