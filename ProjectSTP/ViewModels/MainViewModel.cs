using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSTP.ViewModels
{
    internal class MainViewModel : DependencyObject
    {
        public ManagerVM Manager_VM { get; set; }
        public ClientVM Client_VM { get; set; }
        public ProductVM Product_VM { get; set; }
        public ProductByClientVM ProductByClient_VM { get; set; }
        public ClientByStatusVM ClientByStatus_VM { get; set; }
        public ClientByManagerVM ClientByManager_VM { get; set; }

        public MainViewModel() {
            Manager_VM = new ManagerVM();
            Client_VM = new ClientVM();
            Product_VM = new ProductVM();
            ProductByClient_VM = new ProductByClientVM();
            ClientByStatus_VM = new ClientByStatusVM();
            ClientByManager_VM = new ClientByManagerVM();
                
        }
    }
}
