using ProjectSTP.Abstracts;
using ProjectSTP.Interfaces;
using ProjectSTP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectSTP.Models
{
    public class SQLClientByManagerRepository : IRepository<ClientByManager>
    {
        public string ConnectionString { get; set; }
        public SQLClientByManagerRepository()
        {
            ConnectionString =
                "Server=DESKTOP-HT96TCP;Database=DatabaseProductsSTP;Trusted_Connection=True;";
        }
        public ClientByManager[] GetItemsList()
        {
            List<ClientByManager> result = new List<ClientByManager>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetClientListByManager]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ClientID = (int)reader["ClientID"];
                                string ClientName = (string)reader["ClientName"];
                                int ManagerID = (int)reader["ManagerID"];
                                string ManagerName = (string)reader["ManagerName"];

                                result.Add(new ClientByManager()
                                {
                                    ManagerID = ManagerID,
                                    ManagerName = ManagerName,
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

        public void Create(ClientByManager item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(ClientByManager item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[UpdateClientManager]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", item.ClientID);
                        command.Parameters.AddWithValue("@ManagerID", item.ManagerID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }

        public ClientByManager GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
