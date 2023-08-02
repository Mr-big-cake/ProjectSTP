using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectSTP.Models
{
    public class ClientByManager
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int ManagerID { get; set; }
        public string ManagerName { get; set; }

        public static SQLClientByManagerRepository ClientByManagerRepository { get; private set; } 
        static ClientByManager()
        {
            ClientByManagerRepository = new SQLClientByManagerRepository();
        }

        public static ClientByManager[] GetClientByManagers()
        {
            return ClientByManagerRepository.GetItemsList();
        }

        public static void UpdateCBM(int idClient, int idManager)
        {
            ClientByManagerRepository.Update(new ClientByManager()
            {
                ClientID = idClient,
                ManagerID = idManager
            });
        }
    }
}
