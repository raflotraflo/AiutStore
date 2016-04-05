using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Datebase;

namespace DesktopApplication.ViewModel.Form
{
    public class AddCustomerViewModel : ViewModelBase
    {

        Customer customer = new Customer();

        public AddCustomerViewModel()
        {
            IdVisible = Visibility.Collapsed;
            Name = "";
            Surname = "";
            Street = "";
            Town = "";
            PostCode = "";
            Tel = "";
            Id = 0;

            IsEnabled = true;
        }

        #region Properties

        public Customer Client
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
                RaisePropertyChanged("Client");
            }
        }

        public int Id
        {
            get
            {
                return Client.Id;
            }
            set
            {
                Client.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Name
        {
            get
            {
                return Client.Name;
            }
            set
            {
                Client.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Surname
        {
            get
            {
                return Client.Surname;
            }
            set
            {
                Client.Surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        public string Street
        {
            get
            {
                return Client.Street;
            }
            set
            {
                Client.Street = value;
                RaisePropertyChanged("Street");
            }
        }

        public string Town
        {
            get
            {
                return Client.Town;
            }
            set
            {
                Client.Town = value;
                RaisePropertyChanged("Town");
            }
        }

        public string PostCode
        {
            get
            {
                return Client.PostCode;
            }
            set
            {
                Client.PostCode = value;
                RaisePropertyChanged("PostCode");
            }
        }

        public string Tel
        {
            get
            {
                return Client.Tel;
            }
            set
            {
                Client.Tel = value;
                RaisePropertyChanged("Tel");
            }
        }


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

        #endregion
    }
}
