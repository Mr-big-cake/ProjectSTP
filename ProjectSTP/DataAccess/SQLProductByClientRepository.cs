using ProjectSTP.Abstracts;
using ProjectSTP.Interfaces;
using ProjectSTP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectSTP.Models
{
    public class SQLProductByClientRepository : IRepository<ProductByClient>
    {
        public string ConnectionString { get; set; }
        public SQLProductByClientRepository()
        {
            ConnectionString =
                "Server=DESKTOP-HT96TCP;Database=DatabaseProductsSTP;Trusted_Connection=True;";
        }
        public ProductByClient[] GetItemsList()
        {
            List<ProductByClient> result = new List<ProductByClient>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetProductListByClient]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ClientID = (int)reader["ClientID"];
                                string ClientName = (string)reader["ClientName"];
                                int ProductID = (int)reader["ProductID"];
                                string ProductName = (string)reader["ProductName"];

                                result.Add(new ProductByClient()
                                {
                                    ProductID = ProductID,
                                    ProductName = ProductName,
                                    ClientID = ClientID,
                                    ClientName = ClientName
                                });
                            }
                        }
                    }
                }
            }
            catch { }
            return result.ToArray();
        }

        public void Create(ProductByClient item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[CreatePurchasedProduct]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", item.ClientID);
                        command.Parameters.AddWithValue("@ProductID", item.ProductID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(ProductByClient item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[DeletePurchasedProduct]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", item.ClientID);
                        command.Parameters.AddWithValue("@ProductID", item.ProductID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }
        public void Update(ProductByClient item)
        {
            throw new NotImplementedException();
        }

        public ProductByClient GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
