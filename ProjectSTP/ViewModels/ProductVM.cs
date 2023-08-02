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
    internal class ProductVM : DependencyObject
    {
        #region SetEnabled
        private static void SetEnabled(ProductVM current)
        {
            current.CreateButtonIsEnabled =
                    current.ProductNameText.Length != 0 &&
                    RegularExpressions.IsValidDecimal(current.PriceText);
            current.UpdateButtonIsEnabled =
                    RegularExpressions.IsValidInteger(current.ProductIDText) &&
                    current.ProductNameText.Length != 0 &&
                    RegularExpressions.IsValidDecimal(current.PriceText);
            current.DeleteButtonIsEnabled = RegularExpressions.IsValidInteger(current.ProductIDText);
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
            DependencyProperty.Register("ProductIDText", typeof(string), typeof(ProductVM), new PropertyMetadata("", ProductIDText_Change));

        private static void ProductIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductVM;

            if (current != null)
            {
                SetEnabled(current);
            }
        }
        #endregion

        #region Ввод: Имя
        public string ProductNameText
        {
            get { return (string)GetValue(ProductNameTextProperty); }
            set { SetValue(ProductNameTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductNameText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductNameTextProperty =
            DependencyProperty.Register("ProductNameText", typeof(string), typeof(ProductVM), new PropertyMetadata("", ProductNameText_Change));

        private static void ProductNameText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductVM;

            if (current != null)
            {
                SetEnabled(current);
            }
        }
        #endregion

        #region Ввод: Цена
        public string PriceText
        {
            get { return (string)GetValue(PriceTextProperty); }
            set { SetValue(PriceTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PriceText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PriceTextProperty =
            DependencyProperty.Register("PriceText", typeof(string), typeof(ProductVM), new PropertyMetadata("", PriceText_Change));

        private static void PriceText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductVM;

            if (current != null)
            {
                SetEnabled(current);
            }
        }
        #endregion

        #region Ввод: Тип

        public string SelectedType
        {
            get { return (string)GetValue(SelectedStatusProperty); }
            set { SetValue(SelectedStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedStatusProperty =
            DependencyProperty.Register("SelectedStatus", typeof(string), typeof(ProductVM), new PropertyMetadata("Подписка"));

        #endregion

        #region Ввод: Срок

        public string SelectedSubscriptionDuration
        {
            get { return (string)GetValue(SelectedSubscriptionDurationProperty); }
            set { SetValue(SelectedSubscriptionDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSubscriptionDurationProperty =
            DependencyProperty.Register("SelectedSubscriptionDuration", typeof(string), typeof(ProductVM), new PropertyMetadata("Год"));

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
                        int id;
                        int.TryParse(ProductIDText, out id);
                        Product.DeleteProduct(id);
                        Update();

                    }
                    ProductNameText = "";
                    ProductIDText = "";
                    PriceText = "0";
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
            DependencyProperty.Register("DeleteButtonIsEnabled", typeof(bool), typeof(ProductVM), new PropertyMetadata(false));
        #endregion

        #region Button: Обновить продукт
        public ICommand ClickToUpdate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (UpdateButtonIsEnabled)
                    {
                        int idProduct, idManager;
                        int.TryParse(ProductIDText, out idProduct);
                        int.TryParse(PriceText, out idManager);
                        Product.UpdateProduct(idProduct, ProductNameText, idManager,
                            SelectedType == "Подписка" ? 0 : 1,
                            SelectedSubscriptionDuration == "Месяц"? 0 :
                            SelectedSubscriptionDuration == "Квартал" ? 1 : 2);
                        Update();

                    }
                    ProductNameText = "";
                    ProductIDText = "";
                    PriceText = "0";
                    FilterText = "";
                });
            }
        }
        public bool UpdateButtonIsEnabled
        {
            get { return (bool)GetValue(UpdateButtonIsEnabledProperty); }
            set { SetValue(UpdateButtonIsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdateButtonIsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdateButtonIsEnabledProperty =
            DependencyProperty.Register("UpdateButtonIsEnabled", typeof(bool), typeof(ProductVM), new PropertyMetadata(false));
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
                        int idManager;
                        int.TryParse(PriceText, out idManager);
                        Product.CreateProduct(ProductNameText, idManager,
                            SelectedType == "Подписка" ? 0 : 1,
                            SelectedSubscriptionDuration == "Месяц" ? 0 :
                            SelectedSubscriptionDuration == "Квартал" ? 1 : 2);
                        Update();

                    }
                    ProductNameText = "";
                    ProductIDText = "";
                    PriceText = "0";
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
            DependencyProperty.Register("CreateButtonIsEnabled", typeof(bool), typeof(ProductVM), new PropertyMetadata(false));
        #endregion

        #region Items
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ProductVM), new PropertyMetadata(null));
        #endregion

        #region Поиск
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ProductVM), new PropertyMetadata("", FilterText_Change));

        private static void FilterText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProductVM;
            if(current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterProduct;
            }

        }

        private bool FilterProduct(object obj)
        {
            bool result = true;
            Product current = (Product)obj;
            if(!string.IsNullOrWhiteSpace(FilterText) && current != null)
            {
                if (!current?.ProductName?.Contains(FilterText) == true)
                    result = false;
            }
            return result;
        }
        #endregion

        #region Конструктор + Update
        public ProductVM()
        {
            Update();
            Items.Filter = FilterProduct;
            
        }
        public void Update()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Items = CollectionViewSource.GetDefaultView(Product.GetProducts());
                Items.Filter = FilterProduct;
            });
        }
        #endregion
    }
}
