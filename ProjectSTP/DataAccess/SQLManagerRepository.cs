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
    internal class SQLManagerRepository : IRepository<Manager>
    {
        public string ConnectionString { get; set; }
        public SQLManagerRepository()
        {
            ConnectionString =
                "Server=DESKTOP-HT96TCP;Database=DatabaseProductsSTP;Trusted_Connection=True;";
        }
        public Manager[] GetItemsList()
        {
            List<Manager> result = new List<Manager>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[GetManagers]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int managerID = (int)reader["ManagerID"];
                                string name = (string)reader["Name"];

                                result.Add(new Manager() { ManagerID = managerID, ManagerName = name });
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            return result.ToArray();
        }

        public void Create(Manager item)
        {
            try{
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[InsertManager]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", item.ManagerName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception) { }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[DeleteManager]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ManagerID", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }
        public void Update(Manager item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("[dbo].[UpdateManager]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ManagerID", item.ManagerID);
                        command.Parameters.AddWithValue("@Name", item.ManagerName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { }
        }

        public Manager GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
