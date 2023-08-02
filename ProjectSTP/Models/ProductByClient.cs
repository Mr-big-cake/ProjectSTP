using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectSTP.Models
{
    public class ProductByClient
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }

        public static SQLProductByClientRepository ProductByClientRepository { get; private set; } 
        static ProductByClient()
        {
            ProductByClientRepository = new SQLProductByClientRepository();
        }

        public static ProductByClient[] GetProductByClients()
        {
            return ProductByClientRepository.GetItemsList();
        }

        public static void CreateProductByClient(int clientId, int productId)
        {
            ProductByClientRepository.Create(new ProductByClient() { ProductID = productId, ClientID = clientId });
        }
        public static void UpdateProductByClient(int clientId, int productId)
        {
            ProductByClientRepository.Update(new ProductByClient() { ProductID = productId, ClientID = clientId });
        }

        public static void DeleteProductByClient(int clientId, int productId)
        {
            ProductByClientRepository.Delete(new ProductByClient() { ProductID = productId, ClientID = clientId });
        }
    }
}
