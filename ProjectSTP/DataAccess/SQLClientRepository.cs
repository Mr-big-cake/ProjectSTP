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
    public class SQLClientRepository : IRepository<Client>
    {
        public string ConnectionString { get; set; }
        public SQLClientRepository()
        {
            ConnectionString =
                "Server=DESKTOP-HT96TCP;Database=DatabaseProductsSTP;Trusted_Connection=True;";
        }
        public Client[] GetItemsList()
        {
            List<Client> result = new List<Client>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetClients]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ClientID = (int)reader["ClientID"];
                                string name = (string)reader["Name"];

                                result.Add(new Client() { ClientID = ClientID, ClientName = name });
                            }
                        }
                    }
                }
            }
            catch { }
            return result.ToArray();
        }

        public void Create(Client item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[InsertClient]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", item.ClientName);
                        command.Parameters.AddWithValue("@ManagerID", item.ManagerID);
                        command.Parameters.AddWithValue("@StatusID", item.StatusID);

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

                    using (SqlCommand command = new SqlCommand("[dbo].[DeleteClient]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }
        public void Update(Client item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[UpdateClient]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", item.ClientID);
                        command.Parameters.AddWithValue("@Name", item.ClientName);
                        command.Parameters.AddWithValue("@ManagerID", item.ManagerID);
                        command.Parameters.AddWithValue("@StatusID", item.StatusID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }

        public Client GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
