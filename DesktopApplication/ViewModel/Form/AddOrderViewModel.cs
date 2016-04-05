using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Datebase;
using System.Collections.ObjectModel;

namespace DesktopApplication.ViewModel.Form
{
    public class AddOrderViewModel : ViewModelBase
    {
        
        Order order = new Order();

        public AddOrderViewModel()
        {
            IdVisible = Visibility.Collapsed;
            CustomerName = "";
            CustomerSurname = "";
            CustomerAdress = "";
            DateCreate = DateTime.Now;

            IsEnabled = true;
        }

        #region Properties

        public Order NewOrder
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                RaisePropertyChanged("NewOrder");
            }
        }

        public int Id
        {
            get
            {
                return NewOrder.Id;
            }
            set
            {
                NewOrder.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public DateTime DateCreate
        {
            get
            {
                return NewOrder.DateCreate;
            }
            set
            {
                NewOrder.DateCreate = value;
                RaisePropertyChanged("Barecode");
            }
        }

        public string CustomerName
        {
            get
            {
                return NewOrder.CustomerName;
            }
            set
            {
                NewOrder.CustomerName = value;
                RaisePropertyChanged("CustomerName");
            }
        }

        public string CustomerSurname
        {
            get
            {
                return NewOrder.CustomerSurname;
            }
            set
            {
                NewOrder.CustomerSurname = value;
                RaisePropertyChanged("CustomerSurname");
            }
        }

        public string CustomerAdress
        {
            get
            {
                return NewOrder.CustomerAdress;
            }
            set
            {
                NewOrder.CustomerAdress = value;
                RaisePropertyChanged("CustomerAdress");
            }
        }

        public int DeliveryTypeId
        {
            get
            {
                return NewOrder.DeliveryTypeId;
            }
            set
            {
                NewOrder.DeliveryTypeId = value;
                RaisePropertyChanged("DeliveryTypeId");
            }
        }

        public DeliveryType DeliveryType
        {
            get
            {
                return NewOrder.DeliveryType;
            }
            set
            {
                NewOrder.DeliveryType = value;
                RaisePropertyChanged("DeliveryType");
            }
        }

        public DateTime? DateRealization
        {
            get
            {
                return NewOrder.DateRealization;
            }
            set
            {
                NewOrder.DateRealization = value;
                RaisePropertyChanged("DateRealization");
            }
        }

        public bool Realization
        {
            get
            {
                return NewOrder.Realization;
            }
            set
            {
                NewOrder.Realization = value;
                RaisePropertyChanged("Realization");
            }
        }

        ObservableCollection<AddProductViewModel> _addProductCollection;   
        public ObservableCollection<AddProductViewModel> AddProductCollection
        {
            get
            {
                return _addProductCollection;
            }
            set
            {
                _addProductCollection = value;
                RaisePropertyChanged("AddProductCollection");
            }
        }

        Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;

                CustomerName = value.Name;
                CustomerSurname = value.Surname;
                CustomerAdress = "ul. " + value.Street + ", " + value.PostCode + " " + value.Town;

                RaisePropertyChanged("SelectedCustomer");
            }
        }

        #endregion

        private Visibility _idVisible;
        public Visibility IdVisible
        {
            get
            {
                return _idVisible;
            }
            set
            {
                _idVisible = value;
                RaisePropertyChanged("IdVisible");
            }
        }

        bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
    }
}
