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
    internal class ClientVM : DependencyObject
    {
        #region Ввод: ID Клиента
        public string ClientIDText
        {
            get { return (string)GetValue(ClientIDTextProperty); }
            set { SetValue(ClientIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientIDTextProperty =
            DependencyProperty.Register("ClientIDText", typeof(string), typeof(ClientVM), new PropertyMetadata("", ClientIDText_Change));

        private static void ClientIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled = 
                    RegularExpressions.IsValidClientName(current.ClientNameText) &&
                    RegularExpressions.IsValidInteger(current.ClientToManagerIDText);
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.ClientIDText) &&
                        RegularExpressions.IsValidClientName(current.ClientNameText) &&
                        RegularExpressions.IsValidInteger(current.ClientToManagerIDText) ;
                current.DeleteButtonIsEnabled = RegularExpressions.IsValidInteger(current.ClientIDText);
            }
        }
        #endregion

        #region Ввод: Имя
        public string ClientNameText
        {
            get { return (string)GetValue(ClientNameTextProperty); }
            set { SetValue(ClientNameTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientNameText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientNameTextProperty =
            DependencyProperty.Register("ClientNameText", typeof(string), typeof(ClientVM), new PropertyMetadata("", ClientNameText_Change));

        private static void ClientNameText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled =
                    RegularExpressions.IsValidClientName(current.ClientNameText) &&
                    RegularExpressions.IsValidInteger(current.ClientToManagerIDText);
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.ClientIDText) &&
                        RegularExpressions.IsValidClientName(current.ClientNameText) &&
                        RegularExpressions.IsValidInteger(current.ClientToManagerIDText);
                current.DeleteButtonIsEnabled = RegularExpressions.IsValidInteger(current.ClientIDText);
            }
        }
        #endregion

        #region Ввод: ID Менеджера
        public string ClientToManagerIDText
        {
            get { return (string)GetValue(ClientToManagerIDTextProperty); }
            set { SetValue(ClientToManagerIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientToManagerIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientToManagerIDTextProperty =
            DependencyProperty.Register("ClientToManagerIDText", typeof(string), typeof(ClientVM), new PropertyMetadata("", ClientToManagerIDText_Change));

        private static void ClientToManagerIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled =
                    RegularExpressions.IsValidClientName(current.ClientNameText) &&
                    RegularExpressions.IsValidInteger(current.ClientToManagerIDText);
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.ClientIDText) &&
                        RegularExpressions.IsValidClientName(current.ClientNameText) &&
                        RegularExpressions.IsValidInteger(current.ClientToManagerIDText);
                current.DeleteButtonIsEnabled = RegularExpressions.IsValidInteger(current.ClientIDText);
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
            DependencyProperty.Register("SelectedStatus", typeof(string), typeof(ClientVM), new PropertyMetadata("Обычный клиент"));

        #endregion

        #region Button: Удалить менеджера
        public ICommand ClickToDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (DeleteButtonIsEnabled)
                    {
                        int id;
                        int.TryParse(ClientIDText, out id);
                        Client.DeleteClient(id);
                        Update();

                    }
                    ClientNameText = "";
                    ClientIDText = "";
                    ClientToManagerIDText = "";
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
            DependencyProperty.Register("DeleteButtonIsEnabled", typeof(bool), typeof(ClientVM), new PropertyMetadata(false));
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
                        int idClient, idManager;
                        int.TryParse(ClientIDText, out idClient);
                        int.TryParse(ClientToManagerIDText, out idManager);
                        Client.UpdateClient(idClient, ClientNameText, idManager, SelectedStatus == "Обычный клиент" ? 1:0);
                        Update();

                    }
                    ClientNameText = "";
                    ClientIDText = "";
                    ClientToManagerIDText = "";
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
            DependencyProperty.Register("UpdateButtonIsEnabled", typeof(bool), typeof(ClientVM), new PropertyMetadata(false));
        #endregion

        #region Button: Добавить менеджера
        public ICommand ClickToCreate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (CreateButtonIsEnabled)
                    {
                        int idManager;
                        int.TryParse(ClientToManagerIDText, out idManager);
                        Client.CreateClient( ClientNameText, idManager, SelectedStatus == "Обычный клиент" ? 1 : 0);
                        Update();

                    }
                    ClientNameText = "";
                    ClientIDText = "";
                    ClientToManagerIDText = "";
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
            DependencyProperty.Register("CreateButtonIsEnabled", typeof(bool), typeof(ClientVM), new PropertyMetadata(false));
        #endregion

        #region Items 
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ClientVM), new PropertyMetadata(null));



        #endregion

        #region Поиск
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ClientVM), new PropertyMetadata("", FilterText_Change));

        private static void FilterText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientVM;
            if(current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterClient;
            }

        }

        private bool FilterClient(object obj)
        {
            bool result = true;
            Client current = (Client)obj;
            if (!string.IsNullOrWhiteSpace(FilterText) && current != null)
            {
                if (!current?.ClientName?.Contains(FilterText) == true)
                    result = false;
            }
            return result;
        }

        #endregion

        #region Конструктор + Update
        public ClientVM()
        {
            Update();
            Items.Filter = FilterClient;
        }

        public void Update()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Items = CollectionViewSource.GetDefaultView(Client.GetClients());
                Items.Filter = FilterClient;
            });
        }
        #endregion
    }
}
