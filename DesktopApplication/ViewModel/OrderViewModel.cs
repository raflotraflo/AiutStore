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
    public class OrderViewModel : ViewModelBase
    {
        private DB db = new DB(Properties.Settings.Default.conString); // obiekt bazy danych DB

        public OrderViewModel()
        {
            ShowWindowAddDeliveryTypeCommand = new RelayCommand(() => ShowWindowAddDeliveryType());
            SaveDeliveryTypeCommand = new RelayCommand(() => SaveDeliveryType());
            DeleteDeliveryTypeCommand = new RelayCommand(() => DeleteDeliveryType());
            ShowWindowEditDeliveryTypeCommand = new RelayCommand(() => ShowWindowEditDeliveryType());

            ShowWindowAddOrderCommand = new RelayCommand(() => ShowWindowAddOrder());
            SaveOrderCommand = new RelayCommand(() => SaveOrder());
            DeleteOrderCommand = new RelayCommand(() => DeleteOrder());
            RealizeOrderCommand = new RelayCommand(() => RealizeOrder());
            ShowProductListOfOrderCommand = new RelayCommand(() => ShowProductListOfOrder());

            Get();
        }

        void Get()
        {
            
            try
            {
                OrderCollection = db.GetOrder();
                DeliveryTypeCollection = db.GetDeliveryType();

            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd podczas pobierania danych z bazy: " + e.Message);
            }

            foreach (Order o in OrderCollection)
            {
                try
                {
                    //fu = _listaSzczegolyFaktur.Where(x => x.IdFaktury == f.Id).First();
                    o.DeliveryType = DeliveryTypeCollection.Where(x => x.Id == o.DeliveryTypeId).First();
                }
                catch (Exception) { }
            }
        }

        #region Order

        ObservableCollection<Order> _orderCollection;   //obserwowalna kolekcja obiektów DeliveryType potrzebna do wyświetlania danych 
        public ObservableCollection<Order> OrderCollection
        {
            get
            {
                return _orderCollection;
            }
            set
            {
                _orderCollection = value;
                RaisePropertyChanged("OrderCollection");
            }
        }

        private Order _selectedOrder;   //obserwowalna kolekcja obiektów DeliveryType potrzebna do wyświetlania danych 
        public Order SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                _selectedOrder = value;
                RaisePropertyChanged("SelectedOrder");
            }
        }

        AddOrderViewModel _newOrderVM;
        public AddOrderViewModel NewOrderVM
        {
            get
            {
                return _newOrderVM;
            }
            set
            {
                _newOrderVM = value;
                RaisePropertyChanged("NewOrderVM");
            }
        }

        public AddOrderWindow _addOrderWindow;
        void ShowWindowAddOrder() // Wyświetlenie okna dialogowego do wprowadzenia nowego typu produktu
        {
            NewOrderVM = new AddOrderViewModel();
            var productViewModel = SimpleIoc.Default.GetInstance<ProductViewModel>();
            ObservableCollection<Product> _productCollection = productViewModel.ProductCollection;
            NewOrderVM.AddProductCollection = new ObservableCollection<AddProductViewModel>();
            foreach (Product p in _productCollection)
            {
                try
                {
                    if (p.Count > 0)
                        NewOrderVM.AddProductCollection.Add(new AddProductViewModel(p));
                }
                catch (Exception) { }
            }
           
            _addOrderWindow = new AddOrderWindow();
            _addOrderWindow.DataContext = NewOrderVM;
            _addOrderWindow.ShowDialog();
        }

        void RealizeOrder() // Wyświetlenie okna dialogowego do edycji typu produktu
        {
            if (SelectedOrder != null)
            {
                if (MessageBox.Show("Czy napewno zrealizować to zamówienie?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status st = db.RealizeOrder(SelectedOrder);
                    if (st.State)
                    {
                        Get();
                        RefreshProduct();
                    }
                    else
                        MessageBox.Show("Błąd: " + st.Info);
                }
            }
        }

        void SaveOrder() // Dodanei typu produktu do bazy
        {
            if (NewOrderVM != null)
            {
                Status st = db.AddOrder(NewOrderVM.NewOrder);
                if (st.State)
                {
                    //dodanie produktów do zamówienia hmmmm

                    foreach (AddProductViewModel p in NewOrderVM.AddProductCollection)
                    {
                        if (p.AddCount > 0)
                        {
                            if (!db.AddProductToOrder(p.Id, st.Id, p.AddCount).State)
                                MessageBox.Show("Błąd podczas dodawania " + p.Name);
                            //if(!db.AddProductToOrder(p.)
                        }
                    }

                    if (_addOrderWindow != null) //sprawdzenie czy okno istnieje i zamknięcie go po poprawnym dodaniu
                        if (_addOrderWindow.IsInitialized)
                            _addOrderWindow.Close();

                    Get();
                    RefreshProduct();
                }
                else
                {
                    MessageBox.Show("Popraw dane");
                }
            }
        }

        void DeleteOrder() // Usunięcie Typu produktu z bazy
        {
            if (SelectedOrder != null)
            {
                if (MessageBox.Show("Czy napewno chcesz usunąć?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status st = db.DeleteOrder(SelectedOrder);
                    if (st.State)
                    {
                        Get();
                        RefreshProduct();
                    }
                    else
                        MessageBox.Show("Błąd: " + st.Info);
                }
            }
        }

        void RefreshProduct()
        {
            var productViewModel = SimpleIoc.Default.GetInstance<ProductViewModel>();
            productViewModel.Get();
        }

        ObservableCollection<Product> _productCollection;
        ProductFromOrderWindow _productFromOrderWindow;
        void ShowProductListOfOrder()
        {
            if (SelectedOrder != null)
            {
                _productCollection = db.GetProductListFromOrder(SelectedOrder.Id);

                _productFromOrderWindow = new ProductFromOrderWindow();
                _productFromOrderWindow.DataContext = _productCollection;
                _productFromOrderWindow.ShowDialog();

            }
        }


        public RelayCommand ShowWindowAddOrderCommand { get; private set; }
        public RelayCommand SaveOrderCommand { get; private set; }
        public RelayCommand DeleteOrderCommand { get; private set; }
        public RelayCommand RealizeOrderCommand { get; private set; }
        public RelayCommand ShowProductListOfOrderCommand { get; private set; }


        #endregion


        #region DeliveryType

        public AddDeliveryTypeWindow _addDeliveryTypeWindow;
        void ShowWindowAddDeliveryType() // Wyświetlenie okna dialogowego do wprowadzenia nowego typu produktu
        {
            TemporaryDeliveryTypeName = "";
            _addDeliveryTypeWindow = new AddDeliveryTypeWindow();
            _addDeliveryTypeWindow.cbIsEdit.IsChecked = false;
            _addDeliveryTypeWindow.tbId.Visibility = Visibility.Collapsed;
            _addDeliveryTypeWindow.ShowDialog();
        }

        void ShowWindowEditDeliveryType() // Wyświetlenie okna dialogowego do edycji typu produktu
        {
            if (SelectedDeliveryType != null)
            {
                TemporaryDeliveryTypeName = SelectedDeliveryType.Name;
                _addDeliveryTypeWindow = new AddDeliveryTypeWindow();
                _addDeliveryTypeWindow.cbIsEdit.IsChecked = true;
                _addDeliveryTypeWindow.tbId.Visibility = Visibility.Visible;
                _addDeliveryTypeWindow.Title = "Edycja typu produktu";
                _addDeliveryTypeWindow.tbId.Text = "Id: " + SelectedDeliveryType.Id;
                _addDeliveryTypeWindow.ShowDialog();
            }
        }

        void SaveDeliveryType() // Dodanei typu produktu do bazy
        {

            if (_addDeliveryTypeWindow.cbIsEdit.IsChecked == true) // sprawdzenie czy jest tryb edycji czy dodawania
            {
                //edycja typu Deliveryu
                //var a = db.AddEditDeliveryType(new DeliveryType() { Id = SelectedDeliveryType.Id, Name = TemporaryDeliveryTypeName });
                if (db.AddEditDeliveryType(new DeliveryType() { Id = SelectedDeliveryType.Id, Name = TemporaryDeliveryTypeName }))
                    DeliveryTypeCollection = db.GetDeliveryType();

            }
            else
            {
                //dodanie typu Deliveryu
                //var a = db.AddEditDeliveryType(new DeliveryType() { Id = 0, Name = TemporaryDeliveryTypeName });
                if (db.AddEditDeliveryType(new DeliveryType() { Id = 0, Name = TemporaryDeliveryTypeName }))
                    DeliveryTypeCollection = db.GetDeliveryType();
            }

            if (_addDeliveryTypeWindow != null) //sprawdzenie czy okno istnieje i zamknięcie go po poprawnym dodaniu
                if (_addDeliveryTypeWindow.IsInitialized)
                    _addDeliveryTypeWindow.Close();


            //odświeżenei listy
        }

        void DeleteDeliveryType() // Usunięcie Typu produktu z bazy
        {
            if (SelectedDeliveryType != null)
            {
                if (MessageBox.Show("Czy napewno chcesz usunąć?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status st = db.DeleteDeliveryType(SelectedDeliveryType);
                    if (st.State)
                        DeliveryTypeCollection = db.GetDeliveryType();
                    else
                        MessageBox.Show("Błąd: " + st.Info);
                }
            }
        }


        ObservableCollection<DeliveryType> _deliveryTypeCollection;   //obserwowalna kolekcja obiektów DeliveryType potrzebna do wyświetlania danych 
        public ObservableCollection<DeliveryType> DeliveryTypeCollection
        {
            get
            {
                return _deliveryTypeCollection;
            }
            set
            {
                _deliveryTypeCollection = value;
                RaisePropertyChanged("DeliveryTypeCollection");
            }
        }

        DeliveryType _selectedDeliveryType;   //wybrany obiekt kolekcji obiektów DeliveryType potrzebna do wyświetlania danych 
        public DeliveryType SelectedDeliveryType
        {
            get
            {
                return _selectedDeliveryType;
            }
            set
            {
                _selectedDeliveryType = value;
                RaisePropertyChanged("SelectedDeliveryType");
            }
        }

        String _temporaryDeliveryTypeName;   //zmienna wykorzystywana do dodawania i edycji typu produktu
        public String TemporaryDeliveryTypeName
        {
            get
            {
                return _temporaryDeliveryTypeName;
            }
            set
            {
                _temporaryDeliveryTypeName = value;
                RaisePropertyChanged("TemporaryDeliveryTypeName");
            }
        }

        public RelayCommand ShowWindowAddDeliveryTypeCommand { get; private set; }
        public RelayCommand ShowWindowEditDeliveryTypeCommand { get; private set; }
        public RelayCommand SaveDeliveryTypeCommand { get; private set; }
        public RelayCommand DeleteDeliveryTypeCommand { get; private set; }
        

        #endregion

    }
}
