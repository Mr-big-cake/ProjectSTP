using ProjectSTP.Abstracts;
using ProjectSTP.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ProjectSTP.Utilities;

namespace ProjectSTP.ViewModels
{
    internal class ManagerVM : BaseVM
    {
        #region Ввод: ID
        public string ManagerIDText
        {
            get { return (string)GetValue(ManagerIDTextProperty); }
            set { SetValue(ManagerIDTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ManagerIDText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManagerIDTextProperty =
            DependencyProperty.Register("ManagerIDText", typeof(string), typeof(ManagerVM), new PropertyMetadata("", ManagerIDText_Change));

        private static void ManagerIDText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ManagerVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled = RegularExpressions.IsValidName(current.ManagerNameText);
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.ManagerIDText) &&
                        RegularExpressions.IsValidName(current.ManagerNameText);
                current.DeleteButtonIsEnabled = RegularExpressions.IsValidInteger(current.ManagerIDText);
            }
        }
        #endregion

        #region Ввод: Имя
        public string ManagerNameText
        {
            get { return (string)GetValue(ManagerNameTextProperty); }
            set { SetValue(ManagerNameTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ManagerNameText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManagerNameTextProperty =
            DependencyProperty.Register("ManagerNameText", typeof(string), typeof(ManagerVM), new PropertyMetadata("", ManagerNameText_Change));

        private static void ManagerNameText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ManagerVM;

            if (current != null)
            {
                current.CreateButtonIsEnabled = RegularExpressions.IsValidName(current.ManagerNameText);
                current.UpdateButtonIsEnabled =
                        RegularExpressions.IsValidInteger(current.ManagerIDText) &&
                        RegularExpressions.IsValidName(current.ManagerNameText);
                current.DeleteButtonIsEnabled = RegularExpressions.IsValidInteger(current.ManagerIDText);
            }
        }
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
                        int.TryParse(ManagerIDText, out id);
                        Manager.DeleteManager(id);
                        Update();

                    }
                    ManagerNameText = "";
                    ManagerIDText = "";
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
            DependencyProperty.Register("DeleteButtonIsEnabled", typeof(bool), typeof(ManagerVM), new PropertyMetadata(false));
        #endregion

        #region Button: Обновить Менеджера
        public ICommand ClickToUpdate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (CreateButtonIsEnabled && UpdateButtonIsEnabled)
                    {
                        int id;
                        int.TryParse(ManagerIDText, out id);
                        Manager.UpdateManager(id, ManagerNameText);
                        Update();

                    }
                    ManagerIDText = "";
                    ManagerNameText = "";
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
            DependencyProperty.Register("UpdateButtonIsEnabled", typeof(bool), typeof(ManagerVM), new PropertyMetadata(false));
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
                        Manager.CreateManager(ManagerNameText);
                        Update();

                    }
                    ManagerIDText = "";
                    ManagerNameText = "";
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
            DependencyProperty.Register("CreateButtonIsEnabled", typeof(bool), typeof(ManagerVM), new PropertyMetadata(false));
        #endregion

        #region Item
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ManagerVM), new PropertyMetadata(null));
        #endregion

        #region Поиск
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ManagerVM), new PropertyMetadata("", FilterText_Change));

        private static void FilterText_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ManagerVM;
            if(current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterManager;
            }

        }
        
        private bool FilterManager(object obj)
        {
            bool result = true;
            Manager current = (Manager)obj;
            if(!string.IsNullOrWhiteSpace(FilterText) && current != null)
            {
                if (!current?.ManagerName?.Contains(FilterText) == true)
                    result = false;
            }
            return result;
        }
        #endregion

        #region Конструктор + Update DataGrid
        public ManagerVM()
        {
            Update();
            Items.Filter = FilterManager;
        }

        public void Update()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Items = CollectionViewSource.GetDefaultView(Manager.GetManagers());
                Items.Filter = FilterManager;
            });
        }
        #endregion 
    }
}
