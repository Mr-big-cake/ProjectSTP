using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectSTP.Models
{
    internal class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public int TypeID { get; set; }

        public int SubscriptionDurationID { get; set; }
        public static SQLProductRepository ProductRepository { get; private set; } 
        static Product()
        {
            ProductRepository = new SQLProductRepository();
        }

        public static Product[] GetProducts()
        {
            return ProductRepository.GetItemsList();
        }

        public static void CreateProduct(string name, decimal price, int typeID, int subscriptionDurationID)
        {
            ProductRepository.Create(new Product()
            {
                ProductName = name,
                Price = price,
                TypeID = typeID,
                SubscriptionDurationID = subscriptionDurationID
            });
        }
        public static void UpdateProduct(int id, string name, decimal price, int typeID, int subscriptionDurationID)
        {
            ProductRepository.Update(new Product()
            {
                ProductID = id,
                ProductName = name,
                Price = price,
                TypeID = typeID,
                SubscriptionDurationID = subscriptionDurationID
            });
        }

        public static void DeleteProduct(int id)
        {
            ProductRepository.Delete(id);
        }
    }
}
