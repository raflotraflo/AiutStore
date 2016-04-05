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
    public class AddProductViewModel : ViewModelBase
    {
        Product product = new Product();

        public AddProductViewModel()
        {
            IdVisible = Visibility.Collapsed;
            Name = "";
            Barecode = "";
            DateWarianty = DateTime.Now;

            IsEnabled = true;
        }

        public AddProductViewModel(Product p)
        {
            IdVisible = Visibility.Collapsed;
            IsEnabled = true;
            Prod = p;
        }

        #region Properties
        
        public Product Prod
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                RaisePropertyChanged("Prod");
            }
        }

        public int Id
        {
            get
            {
                return Prod.Id;
            }
            set
            {
                Prod.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Barecode
        {
            get
            {
                return Prod.Barecode;
            }
            set
            {
                Prod.Barecode = value;
                RaisePropertyChanged("Barecode");
            }
        }

        public string Name
        {
            get
            {
                return Prod.Name;
            }
            set
            {
                Prod.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public DateTime DateWarianty
        {
            get
            {
                return Prod.DateWarianty;
            }
            set
            {
                Prod.DateWarianty = value;
                RaisePropertyChanged("DateWarianty");
            }
        }

        public int ProductTypeId
        {
            get
            {
                return Prod.ProductTypeId;
            }
            set
            {
                Prod.ProductTypeId = value;
                RaisePropertyChanged("ProductTypeId");
            }
        }

        public ProductType ProductType
        {
            get
            {
                return Prod.ProductType;
            }
            set
            {
                Prod.ProductType = value;
                RaisePropertyChanged("ProductType");
            }
        }

        public int Count
        {
            get
            {
                return Prod.Count;
            }
            set
            {
                Prod.Count = value;
                RaisePropertyChanged("Count");
            }
        }

        public int AddCount
        {
            get
            {
                return Prod.AddCount;
            }
            set
            {

                if (value <= Count)
                {
                    Prod.AddCount = value;
                }
                else
                    throw new ApplicationException("Dodawana ilość nie może być większa niż ilość dostępna");
                RaisePropertyChanged("AddCount");
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
