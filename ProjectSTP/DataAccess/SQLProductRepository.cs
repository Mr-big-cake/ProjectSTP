using ProjectSTP.Abstracts;
using ProjectSTP.Interfaces;
using ProjectSTP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectSTP.Models
{
    internal class SQLProductRepository : IRepository<Product>
    {
        public string ConnectionString { get; set; }
        public SQLProductRepository()
        {
            ConnectionString =
                "Server=DESKTOP-HT96TCP;Database=DatabaseProductsSTP;Trusted_Connection=True;";
        }
        public Product[] GetItemsList()
        {
            List<Product> result = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetProducts]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ProductID = (int)reader["ProductID"];
                                string name = (string)reader["Name"];

                                result.Add(new Product() { ProductID = ProductID, ProductName = name });
                            }
                        }
                    }
                }
            }
            catch { }
            return result.ToArray();
        }

        public void Create(Product item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[InsertProduct]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", item.ProductName);
                        command.Parameters.AddWithValue("@Price", item.Price);
                        command.Parameters.AddWithValue("@TypeID", item.TypeID);
                        command.Parameters.AddWithValue("@SubscriptionDurationID", item.SubscriptionDurationID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[DeleteProduct]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }
        public void Update(Product item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[UpdateProduct]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", item.ProductID);
                        command.Parameters.AddWithValue("@Name", item.ProductName);
                        command.Parameters.AddWithValue("@Price", item.Price);
                        command.Parameters.AddWithValue("@TypeID", item.TypeID);
                        command.Parameters.AddWithValue("@SubscriptionDurationID", item.SubscriptionDurationID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }

        public Product GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
