using Assignment_CRUID.Models;
using System.Data;
using System.Data.SqlClient;

namespace Assignment_CRUID.Utility
{
    public class DbLayer
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnStr;
        public DbLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnStr = _configuration.GetConnectionString("DbConnection");
        }

        public IEnumerable<Product> ProductsList()
        {
            var products = new List<Product>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Usp_GetAllProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Product product = new Product();
                    product.ProductId = Convert.ToInt32(rd["ProductId"]);
                    product.ProductName = rd["Name"].ToString();
                    product.Description = rd["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(rd["UnitPrice"]);
                    product.CategoryId = Convert.ToInt32(rd["CategoryId"]);

                    products.Add(product);
                }
                con.Close();
            }
            return products;
        }

        public IEnumerable<Categoty> CategoriesList()
        {
            var categories = new List<Categoty>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Usp_GetAllCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Categoty category = new Categoty();
                    category.CategoryId = Convert.ToInt32(rd["CategoryId"]);
                    category.CategoryName = rd["Name"].ToString();
                    categories.Add(category);
                }
                con.Close();
            }
            return categories;
        }

        public void SaveProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Usp_SaveProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@Type", "save");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Usp_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    product.ProductId = Convert.ToInt32(rd["ProductId"]);
                    product.ProductName = rd["Name"].ToString();
                    product.Description = rd["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(rd["UnitPrice"]);
                    product.CategoryId = Convert.ToInt32(rd["CategoryId"]);
                }
                con.Close();
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Usp_SaveProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@Type", "update");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Usp_SaveProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.Parameters.AddWithValue("@Type", "delete");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
