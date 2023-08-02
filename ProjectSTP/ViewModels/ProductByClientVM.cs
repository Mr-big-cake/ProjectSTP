using ProjectSTP.Models;
using ProjectSTP.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectSTP.ViewModels
{
    internal class ProductByClientVM : DependencyObject
    {
        #region Ввод: ID Клиента
        public string ClientIDText
        {
            get { return (string)GetValue(ClientIDTextProperty); }
            set { SetValue(ClientIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientIDTextProperty =
            DependencyProperty.Register("ClientIDText", typeof(string), typeof(ProductByClientVM), new PropertyMetadata("", ClientIDText_Change));

        private static void ClientIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductByClientVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled =
                     RegularExpressions.IsValidInteger(current.ClientIDText) &&
                     RegularExpressions.IsValidInteger(current.ProductIDText);
                current.DeleteButtonIsEnabled =
                     RegularExpressions.IsValidInteger(current.ClientIDText) &&
                     RegularExpressions.IsValidInteger(current.ProductIDText);
            }
        }
        #endregion

        #region Ввод: ID Продукта
        public string ProductIDText
        {
            get { return (string)GetValue(ProductIDTextProperty); }
            set { SetValue(ProductIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductIDTextProperty =
            DependencyProperty.Register("ProductIDText", typeof(string), typeof(ProductByClientVM), new PropertyMetadata("", ProductIDText_Change));

        private static void ProductIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductByClientVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled =
                    RegularExpressions.IsValidInteger(current.ClientIDText) &&
                    RegularExpressions.IsValidInteger(current.ProductIDText);
                current.DeleteButtonIsEnabled =
                    RegularExpressions.IsValidInteger(current.ClientIDText) &&
                    RegularExpressions.IsValidInteger(current.ProductIDText);
            }
        }
        #endregion

        #region Button: Удалить продукт
        public ICommand ClickToDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (DeleteButtonIsEnabled)
                    {
                        int clientId, productId;
                        int.TryParse(ProductIDText, out productId);
                        int.TryParse(ClientIDText, out clientId);
                        ProductByClient.DeleteProductByClient(clientId, productId);
                        Update();

                    }
                    ProductIDText = "";
                    ClientIDText = "";
                    FilterText = "";
                });
            }
        }
        public bool DeleteButtonIsEnabled
        {
            get { return (bool)GetValue(DeleteButtonIsEnabledProperty); }
            set { SetValue(DeleteButtonIsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteButtonIsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteButtonIsEnabledProperty =
            DependencyProperty.Register("DeleteButtonIsEnabled", typeof(bool), typeof(ProductByClientVM), new PropertyMetadata(false));
        #endregion

        #region Button: Добавить продукт
        public ICommand ClickToCreate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (CreateButtonIsEnabled)
                    {
                        int clientId, productId;
                        int.TryParse(ProductIDText, out productId);
                        int.TryParse(ClientIDText, out clientId);
                        ProductByClient.CreateProductByClient(clientId, productId);
                        Update();

                    }
                    ProductIDText = "";
                    ClientIDText = "";
                    FilterText = "";
                });
            }
        }
        public bool CreateButtonIsEnabled
        {
            get { return (bool)GetValue(CreateButtonIsEnabledProperty); }
            set { SetValue(CreateButtonIsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CreateButtonIsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreateButtonIsEnabledProperty =
            DependencyProperty.Register("CreateButtonIsEnabled", typeof(bool), typeof(ProductByClientVM), new PropertyMetadata(false));
        #endregion

        #region Items
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ProductByClientVM), new PropertyMetadata(null));

        #endregion

        #region Поиск
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ProductByClientVM), new PropertyMetadata("", FilterText_Change));

        private static void FilterText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductByClientVM;
            if(current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterProductByClient;
            }

        }

        private bool FilterProductByClient(object obj)
        {
            bool result = true;
            ProductByClient current = (ProductByClient)obj;
            if(!string.IsNullOrWhiteSpace(FilterText) && current != null)
            {
                if ((!current?.ProductName?.Contains(FilterText) == true) &&
                    (!current?.ClientName?.Contains(FilterText) == true))
                    result = false;
            }
            return result;
        }
        #endregion

        #region Конструктор + Updata()

        public ProductByClientVM()
        {
            Update();
            Items.Filter = FilterProductByClient;
        }

        public void Update()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Items = CollectionViewSource.GetDefaultView(ProductByClient.GetProductByClients());
                Items.Filter = FilterProductByClient;
            });
        }

        #endregion
    }
}
