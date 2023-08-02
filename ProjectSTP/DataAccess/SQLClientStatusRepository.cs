using ProjectSTP.Abstracts;
using ProjectSTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSTP.Models
{
    public class SQLClientByStatusRepository : IRepository<ClientByStatus>
    {
        public string ConnectionString { get; set; }
        public SQLClientByStatusRepository()
        {
            ConnectionString =
                "Server=DESKTOP-HT96TCP;Database=DatabaseProductsSTP;Trusted_Connection=True;";
        }
        public ClientByStatus[] GetItemsList()
        {
            List<ClientByStatus> result = new List<ClientByStatus>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetClientListByStatus]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ClientID = (int)reader["ClientID"];
                                string ClientName = (string)reader["ClientName"];
                                string ClientStatus = (string)reader["ClientStatus"];
                                result.Add(new ClientByStatus()
                                {
                                    ClientID = ClientID,
                                    ClientName = ClientName,
                                    ClientStatus = ClientStatus
                                });
                            }
                        }
                    }
                }
            }
            catch { }
            return result.ToArray();
        }

        public void Create(ClientByStatus item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(ClientByStatus item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[UpdateClientStatus]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", item.ClientID);
                        command.Parameters.AddWithValue("@StatusID", item.ClientStatus == "Обычный клиент" ? 1:0);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }

        public ClientByStatus GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
