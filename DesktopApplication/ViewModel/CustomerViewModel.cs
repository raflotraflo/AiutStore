using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

using Datebase;
using System.Windows;
using DesktopApplication.Windows;
using DesktopApplication.ViewModel.Form;

namespace DesktopApplication.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private DB db = new DB(Properties.Settings.Default.conString); // obiekt bazy danych DB

        public CustomerViewModel()
        {
            ShowWindowAddCustomerCommand = new RelayCommand(() => ShowWindowAddCustomer());
            SaveCustomerCommand = new RelayCommand<Customer>((x) => SaveCustomer(x));
            DeleteCustomerCommand = new RelayCommand(() => DeleteCustomer());
            ShowWindowEditCustomerCommand = new RelayCommand(() => ShowWindowEditCustomer());

            Get();
        }

        void Get()
        {
            CustomerCollection = db.GetCustomer();
        }


        ObservableCollection<Customer> _customerCollection;   //obserwowalna kolekcja obiektów Customer potrzebna do wyświetlania danych 
        public ObservableCollection<Customer> CustomerCollection
        {
            get
            {
                return _customerCollection;
            }
            set
            {
                _customerCollection = value;
                RaisePropertyChanged("CustomerCollection");
            }
        }

        private Customer _selectedCustomer;   //wybrany obiekt Customer potrzebny do wyświetlania danych 
        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
            }
        }

        AddCustomerViewModel _newCustomerVM;
        public AddCustomerViewModel NewCustomerVM
        {
            get
            {
                return _newCustomerVM;
            }
            set
            {
                _newCustomerVM = value;
                RaisePropertyChanged("NewCustomerVM");
            }
        }

        public AddCustomerWindow _addCustomerWindow;
        void ShowWindowAddCustomer() // Wyświetlenie okna dialogowego do wprowadzenia nowego typu produktu
        {        
            NewCustomerVM = new AddCustomerViewModel();
            _addCustomerWindow = new AddCustomerWindow();
            _addCustomerWindow.DataContext = NewCustomerVM;
            _addCustomerWindow.ShowDialog();
        }

        void ShowWindowEditCustomer() // Wyświetlenie okna dialogowego do edycji typu produktu
        {
            if (SelectedCustomer != null)
            {

                NewCustomerVM = new AddCustomerViewModel();
                NewCustomerVM.Client = SelectedCustomer;
                NewCustomerVM.IdVisible = Visibility.Visible;

                _addCustomerWindow = new AddCustomerWindow();
                _addCustomerWindow.DataContext = NewCustomerVM;
                _addCustomerWindow.Title = "Edycja Klienta";
                _addCustomerWindow.ShowDialog();
            }
        }

        void SaveCustomer(Customer cu) // zapisanie użytkownika do bazy
        {

            if (db.AddEditCustomer(cu))
            {
                CustomerCollection = db.GetCustomer();


                if (_addCustomerWindow != null) //sprawdzenie czy okno istnieje i zamknięcie go po poprawnym dodaniu
                    if (_addCustomerWindow.IsInitialized)
                        _addCustomerWindow.Close();
            }
            else
            {
                MessageBox.Show("Nieoczekiwany błąd");
            }

        }

        void DeleteCustomer() // Usunięcie Typu produktu z bazy
        {
            if (SelectedCustomer != null)
            {
                if (MessageBox.Show("Czy napewno chcesz usunąć?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status st = db.DeleteCustomer(SelectedCustomer);
                    if (st.State)
                        CustomerCollection = db.GetCustomer();
                    else
                        MessageBox.Show("Błąd: " + st.Info);
                }
            }
        }


        //String _temporaryDeliveryTypeName;   //zmienna wykorzystywana do dodawania i edycji typu produktu
        //public String TemporaryDeliveryTypeName
        //{
        //    get
        //    {
        //        return _temporaryDeliveryTypeName;
        //    }
        //    set
        //    {
        //        _temporaryDeliveryTypeName = value;
        //        RaisePropertyChanged("TemporaryDeliveryTypeName");
        //    }
        //}

        public RelayCommand ShowWindowAddCustomerCommand { get; private set; }
        public RelayCommand ShowWindowEditCustomerCommand { get; private set; }
        public RelayCommand<Customer> SaveCustomerCommand { get; private set; }
        public RelayCommand DeleteCustomerCommand { get; private set; }
        

    }
}
