using ProjectSTP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectSTP.Models
{
    internal class Client
    {
        public int ClientID { get; set; }
        public int ManagerID { get; set; }
        public int StatusID { get; set; }
        public string ClientName { get; set; }

        public static SQLClientRepository ClientRepository { get; private set; } 
        static Client()
        {
            ClientRepository = new SQLClientRepository();
        }

        public static Client[] GetClients()
        {
            return ClientRepository.GetItemsList();
        }

        public static void CreateClient(string name, int managerID, int statusID)
        {
            ClientRepository.Create(new Client() {
                ClientName = name,
                ManagerID = managerID,
                StatusID = statusID
            });
        }
        public static void UpdateClient(int id, string name, int managerID, int statusID)
        {
            ClientRepository.Update(new Client() {
                ClientID = id,
                ClientName = name,
                ManagerID = managerID,
                StatusID = statusID
            });
        }

        public static void DeleteClient(int id)
        {
            ClientRepository.Delete(id);
        }
    }
}
