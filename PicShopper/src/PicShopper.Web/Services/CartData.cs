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
        bool AddToCart(Cart cartData, string id);
        IEnumerable<BuyItem> GetBuyItems(int id);
        bool RemoveItem(int id);
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
        
        public bool AddToCart(Cart cart, string id)
        {
            int userId       = Convert.ToInt32(id);
            SqlCommand cmd   = con.CreateCommand();
            cmd.CommandType  = CommandType.StoredProcedure;
            cmd.CommandText  = "sp_addItemToCart";
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value   = cart.ItemName;
            cmd.Parameters.Add("@price", SqlDbType.Int).Value      = cart.Price;
            cmd.Parameters.Add("@count", SqlDbType.Int).Value      = cart.Count;
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value     = userId;
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

        public IEnumerable<BuyItem> GetBuyItems(int userId)
        {
            List<BuyItem> items = new List<BuyItem>();
            SqlCommand    cmd   = con.CreateCommand();
            DataTable     table = new DataTable();
            cmd.CommandType     = CommandType.StoredProcedure;
            cmd.CommandText     = "sp_getUserCart";
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
                    BuyItem item = new BuyItem();
                    item.Name = row["item_name"].ToString();
                    item.Price = (int)row["item_price"];
                    item.Count = (int)row["item_count"];
                    items.Add(item);
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

            return items;
        }

        public bool RemoveItem(int userId)
        {
            SqlCommand cmd    = con.CreateCommand();
            cmd.CommandType   = CommandType.StoredProcedure;
            cmd.CommandText   = "sp_removeUserItemsFromCart";
            cmd.Parameters.Add("@userId", SqlDbType.VarChar).Value = userId;
            try
            {
                con.Open();
                int success = cmd.ExecuteNonQuery();

                if (success == 0)
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
