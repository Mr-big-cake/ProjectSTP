using ProjectSTP.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ProjectSTP.Utilities;
using System.Timers;
using System.Threading;

namespace ProjectSTP.ViewModels
{
    internal class ClientByStatusVM : DependencyObject
    {
        #region Ввод: ID Клиента
        public string ClientIDText
        {
            get { return (string)GetValue(ClientIDTextProperty); }
            set { SetValue(ClientIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientIDTextProperty =
            DependencyProperty.Register("ClientIDText", typeof(string), typeof(ClientByStatusVM), new PropertyMetadata("", ClientIDText_Change));

        private static void ClientIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientByStatusVM;

            if (current != null)
            {
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.ClientIDText);
            }
        }
        #endregion

        #region Ввод: Статус

        public string SelectedStatus
        {
            get { return (string)GetValue(SelectedStatusProperty); }
            set { SetValue(SelectedStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedStatusProperty =
            DependencyProperty.Register("SelectedStatus", typeof(string), typeof(ClientByStatusVM), new PropertyMetadata("Обычный клиент"));

        #endregion

        #region Button: Обновить Менеджера
        public ICommand ClickToUpdate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (UpdateButtonIsEnabled)
                    {
                        int idClient;
                        int.TryParse(ClientIDText, out idClient);
                        ClientByStatus.UpdateClientStatus(idClient, SelectedStatus);
                        Update();

                    }
                    ClientIDText = "";
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
            DependencyProperty.Register("UpdateButtonIsEnabled", typeof(bool), typeof(ClientByStatusVM), new PropertyMetadata(false));
        #endregion

        #region Items
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ClientByStatusVM), new PropertyMetadata(null));

        #endregion

        #region Поиск
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ClientByStatusVM), new PropertyMetadata("", FilterText_Change));

        private static void FilterText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientByStatusVM;
            if(current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterClientByStatus;
            }

        }

        private bool FilterClientByStatus(object obj)
        {
            bool result = true;
            ClientByStatus current = (ClientByStatus)obj;
            if(!string.IsNullOrWhiteSpace(FilterText) && current != null)
            {
                if (!current?.ClientName?.Contains(FilterText) == true)
                    result = false;
            }
            return result;
        }
        #endregion

        #region Конструктор + Update
        public ClientByStatusVM()
        {
            Update();
            Items.Filter = FilterClientByStatus;
        }

        public void Update()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Items = CollectionViewSource.GetDefaultView(ClientByStatus.GetClientByStatuss());
                Items.Filter = FilterClientByStatus;
            });
        }
        #endregion

    }
}
