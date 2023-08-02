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
    internal class ClientByManagerVM : DependencyObject
    {
        #region Ввод: ID Клиента
        public string CBMClientIDText
        {
            get { return (string)GetValue(CBMClientIDTextProperty); }
            set { SetValue(CBMClientIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBMClientIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBMClientIDTextProperty =
            DependencyProperty.Register("CBMClientIDText", typeof(string), typeof(ClientByManagerVM), new PropertyMetadata("", CBMClientIDText_Change));

        private static void CBMClientIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientByManagerVM;

            if (current != null)
            {
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.CBMClientIDText) &&
                        RegularExpressions.IsValidInteger(current.CBMManagerIDText);
            }
        }
        #endregion

        #region Ввод: ID Менеджера
        public string CBMManagerIDText
        {
            get { return (string)GetValue(CBMManagerIDTextProperty); }
            set { SetValue(CBMManagerIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBMManagerIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBMManagerIDTextProperty =
            DependencyProperty.Register("CBMManagerIDText", typeof(string), typeof(ClientByManagerVM), new PropertyMetadata("", CBMManagerIDText_Change));

        private static void CBMManagerIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientByManagerVM;

            if (current != null)
            {
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.CBMClientIDText) &&
                        RegularExpressions.IsValidInteger(current.CBMManagerIDText);
            }
        }
        #endregion

        #region Button: Поменять Менеджера
        public ICommand ClickToUpdate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (UpdateButtonIsEnabled)
                    {
                        int idClient, idManager;
                        int.TryParse(CBMClientIDText, out idClient);
                        int.TryParse(CBMManagerIDText, out idManager);
                        ClientByManager.UpdateCBM(idClient, idManager);
                        Update();

                    }
                    CBMClientIDText = "";
                    CBMManagerIDText = "";
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
            DependencyProperty.Register("UpdateButtonIsEnabled", typeof(bool), typeof(ClientByManagerVM), new PropertyMetadata(false));
        #endregion

        #region Items
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ClientByManagerVM), new PropertyMetadata(null));
        #endregion

        #region Поиск
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ClientByManagerVM), new PropertyMetadata("", FilterText_Change));

        private static void FilterText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientByManagerVM;
            if(current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterClientByManager;
            }

        }

        private bool FilterClientByManager(object obj)
        {
            bool result = true;
            ClientByManager current = (ClientByManager)obj;
            if(!string.IsNullOrWhiteSpace(FilterText) && current != null)
            {
                if ((!current?.ManagerName?.Contains(FilterText) == true) &&
                    (!current?.ClientName?.Contains(FilterText) == true))
                    result = false;
            }
            return result;
        }
        #endregion

        #region Конструктор + Update()
        public ClientByManagerVM()
        {
            Update();
            Items.Filter = FilterClientByManager;
        }
        public void Update()
        {
            Items = CollectionViewSource.GetDefaultView(ClientByManager.GetClientByManagers());
        }
        #endregion
    }
}
