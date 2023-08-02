using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjectSTP.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public string ManagerName { get; set; }

        public static SQLManagerRepository ManagerRepository { get; private set; } 
        static Manager()
        {
            ManagerRepository = new SQLManagerRepository();
        }

        public static Manager[] GetManagers()
        {
            return ManagerRepository.GetItemsList();
        }
        public static void CreateManager(string name)
        {
            ManagerRepository.Create(new Manager() { ManagerName = name });
        }
        public static void UpdateManager(int id, string name)
        {
            ManagerRepository.Update(new Manager() { ManagerID =id, ManagerName = name });
        }

        public static void DeleteManager(int id)
        {
            ManagerRepository.Delete(id);
        }
    }
}
