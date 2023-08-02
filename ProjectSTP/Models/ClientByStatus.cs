using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectSTP.Models
{
    public class ClientByStatus
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientStatus { get; set; }

        public static SQLClientByStatusRepository ClientByStatusRepository { get; private set; } 
        static ClientByStatus()
        {
            ClientByStatusRepository = new SQLClientByStatusRepository();
        }

        public static ClientByStatus[] GetClientByStatuss()
        {
            return ClientByStatusRepository.GetItemsList();
        }

        public static void UpdateClientStatus(int id, string statusID)
        {
            ClientByStatusRepository.Update(new ClientByStatus()
            {
                ClientID = id,
                ClientStatus = statusID
            });
        }
    }
}
