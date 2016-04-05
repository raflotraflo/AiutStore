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
    public class ProductViewModel : ViewModelBase
    {
        private DB db = new DB(Properties.Settings.Default.conString); // obiekt bazy danych DB

        public ProductViewModel()
        {
            //deklaracja Commandów:
            ShowWindowAddProductTypeCommand = new RelayCommand(() => ShowWindowAddProductType());
            SaveProductTypeCommand = new RelayCommand(() => SaveProductType());
            DeleteProductTypeCommand = new RelayCommand(() => DeleteProductType());
            ShowWindowEditProductTypeCommand = new RelayCommand(() => ShowWindowEditProductType());

            ShowWindowAddProductCommand = new RelayCommand(() => ShowWindowAddProduct());
            SaveProductCommand = new RelayCommand<Product>((x) => SaveProduct(x));
            DeleteProductCommand = new RelayCommand(() => DeleteProduct());
            ShowWindowEditProductCommand = new RelayCommand(() => ShowWindowEditProduct());
            DeleteOneProductCommand = new RelayCommand(() => DeleteOneProduct());
            AddOneProductCommand = new RelayCommand(() => AddOneProduct());

            ShowProductListCommand = new RelayCommand(() => ShowProductList());
            SearchProductCommand = new RelayCommand<object>((x) => SearchProduct(x));


            Get();
        }

        public void Get()
        {
            // ObservableCollection<Product> list = new ObservableCollection<Product>();

            try
            {
                ProductTypeCollection = db.GetProductType();
                ProductCollection = db.GetProduct();

            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd podczas pobierania danych z bazy: " + e.Message);
            }

            foreach (Product p in ProductCollection)
            {
                try
                {
                    //fu = _listaSzczegolyFaktur.Where(x => x.IdFaktury == f.Id).First();
                    p.ProductType = ProductTypeCollection.Where(x => x.Id == p.ProductTypeId).First();
                }
                catch (Exception) { }
            }

            // ProductCollectionGridView = ProductCollection;
        }

        #region Product

        ObservableCollection<Product> _productCollection;   //obserwowalna kolekcja obiektów Product potrzebna do wyświetlania danych 
        public ObservableCollection<Product> ProductCollection
        {
            get
            {
                return _productCollection;
            }
            set
            {
                _productCollection = value;
                ProductCollectionGridView = value;
                RaisePropertyChanged("ProductCollection");
            }
        }

        ObservableCollection<Product> _productCollectionGridView;   //obserwowalna kolekcja obiektów Product potrzebna do wyświetlania danych 
        public ObservableCollection<Product> ProductCollectionGridView
        {
            get
            {
                return _productCollectionGridView;
            }
            set
            {
                _productCollectionGridView = value;
                RaisePropertyChanged("ProductCollectionGridView");
            }
        }

        Product _selectedProduct;   //wybrany obiekt kolekcji obiektów Product potrzebna do wyświetlania danych 
        public Product SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }

        AddProductViewModel _newProductVM;
        public AddProductViewModel NewProductVM
        {
            get
            {
                return _newProductVM;
            }
            set
            {
                _newProductVM = value;
                RaisePropertyChanged("NewProductVM");
            }
        }

        public AddProductWindow _addProductWindow;
        void ShowWindowAddProduct() // Wyświetlenie okna dialogowego do wprowadzenia nowego typu produktu
        {
            NewProductVM = new AddProductViewModel();
            _addProductWindow = new AddProductWindow();
            _addProductWindow.DataContext = NewProductVM;
            _addProductWindow.ShowDialog();
        }

        void ShowWindowEditProduct() // Wyświetlenie okna dialogowego do edycji typu produktu
        {
            if (SelectedProduct != null)
            {

                NewProductVM = new AddProductViewModel();
                NewProductVM.Prod = SelectedProduct;
                NewProductVM.ProductType = ProductTypeCollection.Where(x => x.Id == SelectedProduct.ProductTypeId).First();
                NewProductVM.IdVisible = Visibility.Visible;
                NewProductVM.IsEnabled = false;

                _addProductWindow = new AddProductWindow();
                _addProductWindow.DataContext = NewProductVM;
                _addProductWindow.Title = "Edycja produktu";
                _addProductWindow.ShowDialog();
            }
        }

        void SaveProduct(Product pr) // Dodanei typu produktu do bazy
        {
            Status st = db.AddEditProduct(pr);
            if (st.State)
            {
                if (pr.Id == 0)
                {
                    //dodanie ilości towaru
                    if(!db.AddOneProduct(st.Id, pr.Count))
                        MessageBox.Show("Błąd podczas dodawania ilości produktu");
                   // int i = st.Id; // id nowego rekordu
                }


                if (_addProductWindow != null) //sprawdzenie czy okno istnieje i zamknięcie go po poprawnym dodaniu
                    if (_addProductWindow.IsInitialized)
                        _addProductWindow.Close();

                Get();
            }
            else
            {
                MessageBox.Show(st.Info);
            }
        }

        void DeleteProduct() // Usunięcie Typu produktu z bazy
        {
            if (SelectedProduct != null)
            {
                if (MessageBox.Show("Czy napewno chcesz usunąć?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status st = db.DeleteProduct(SelectedProduct);
                    if (st.State)
                        Get();
                    else
                        MessageBox.Show("Błąd: " + st.Info);
                }
            }
        }

        void SearchProduct(object p)
        {
             var textBox = p as System.Windows.Controls.TextBox;

             try
             {
                 string phrase = textBox.Text.ToLower();

                 //var a = from ZDmodel x in PzList where (x.Numer.ToLower().Contains(fraza) || x.NumerObcy.ToLower().Contains(fraza) || x.Kontrahent.ToLower().Contains(fraza)) select x;
                 ObservableCollection<Product> list = new ObservableCollection<Product>();

                 var a = ProductCollection.Where(x => x.Name.ToLower().Contains(phrase) || x.Barecode.Contains(phrase));

                 foreach (var x in a)
                 {
                     list.Add(x);
                 }

                 ProductCollectionGridView = list;
             }
             catch (Exception) { }
        }


        public RelayCommand ShowWindowAddProductCommand { get; private set; }
        public RelayCommand<Product> SaveProductCommand { get; private set; }
        public RelayCommand DeleteProductCommand { get; private set; }
        public RelayCommand ShowWindowEditProductCommand { get; private set; }
        public RelayCommand<object> SearchProductCommand { get; private set; }

        #endregion


        #region OneProduct 

        Product temporarySelectedProduct;

        public void  DeleteOneProduct()
        {
            try
            {
                var list = _productListWindow.RadGridViewProductListCollection.SelectedItems;
                if (MessageBox.Show("Czy napewno chcesz usunąć "+list.Count+ "towarów?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (OneProduct op in list)
                    {
                        db.DeleteOneProduct(op.Id);
                    }
                }

            }
            catch (Exception) { }

            ProductListCollection = db.GetProductList(temporarySelectedProduct);
            Get();
        }

        ProductListWindow _productListWindow;
        public void ShowProductList()
        {
            if (SelectedProduct != null)
            {
                temporarySelectedProduct = SelectedProduct;
                ProductListCollection = db.GetProductList(temporarySelectedProduct);
                
                _productListWindow = new ProductListWindow();
                _productListWindow.ShowDialog();
            }
        }

        int _countOfAddProduct;   //wybrany obiekt kolekcji obiektów Product potrzebna do wyświetlania danych 
        public int CountOfAddProduct
        {
            get
            {
                return _countOfAddProduct;
            }
            set
            {
                _countOfAddProduct = value;
                RaisePropertyChanged("CountOfAddProduct");
            }
        }

        ObservableCollection<OneProduct> _productListCollection;   //obserwowalna kolekcja obiektów Product potrzebna do wyświetlania danych 
        public ObservableCollection<OneProduct> ProductListCollection
        {
            get
            {
                return _productListCollection;
            }
            set
            {
                _productListCollection = value;
                RaisePropertyChanged("ProductListCollection");
            }
        }

        public void AddOneProduct()
        {
            if (temporarySelectedProduct != null)
            {
                if (CountOfAddProduct > 0)
                {
                    if (!db.AddOneProduct(temporarySelectedProduct.Id, CountOfAddProduct))
                    {
                        MessageBox.Show("Błąd podczas dodawania ilości produktu");
                    }
                    else
                    {
                        CountOfAddProduct = 0;
                        ProductListCollection = db.GetProductList(temporarySelectedProduct);
                        Get();
                    }
                }
            }
        }


        public RelayCommand ShowProductListCommand { get; private set; }
        public RelayCommand DeleteOneProductCommand { get; private set; }
        public RelayCommand AddOneProductCommand { get; private set; }

        #endregion


        #region ProductType

        public AddProductTypeWindow _addProductTypeWindow;
        void ShowWindowAddProductType() // Wyświetlenie okna dialogowego do wprowadzenia nowego typu produktu
        {
            TemporaryProductTypeName = "";
            _addProductTypeWindow = new AddProductTypeWindow();
            _addProductTypeWindow.cbIsEdit.IsChecked = false;
            _addProductTypeWindow.tbId.Visibility = Visibility.Collapsed;
            _addProductTypeWindow.ShowDialog();
        }

        void ShowWindowEditProductType() // Wyświetlenie okna dialogowego do edycji typu produktu
        {
            if (SelectedProductType != null)
            {
                TemporaryProductTypeName = SelectedProductType.Name;
                _addProductTypeWindow = new AddProductTypeWindow();
                _addProductTypeWindow.cbIsEdit.IsChecked = true;
                _addProductTypeWindow.tbId.Visibility = Visibility.Visible;
                _addProductTypeWindow.Title = "Edycja typu produktu";
                _addProductTypeWindow.tbId.Text = "Id: " + SelectedProductType.Id;
                _addProductTypeWindow.ShowDialog();
            }
        }

        void SaveProductType() // Dodanei typu produktu do bazy
        {
            
            if (_addProductTypeWindow.cbIsEdit.IsChecked == true) // sprawdzenie czy jest tryb edycji czy dodawania
            {
                //edycja typu productu
                //var a = db.AddEditProductType(new ProductType() { Id = SelectedProductType.Id, Name = TemporaryProductTypeName });
                if (db.AddEditProductType(new ProductType() { Id = SelectedProductType.Id, Name = TemporaryProductTypeName }))
                    ProductTypeCollection = db.GetProductType();

            }
            else
            {
                //dodanie typu productu
                //var a = db.AddEditProductType(new ProductType() { Id = 0, Name = TemporaryProductTypeName });
                if (db.AddEditProductType(new ProductType() { Id = 0, Name = TemporaryProductTypeName }))
                    ProductTypeCollection = db.GetProductType();
            }

            if (_addProductTypeWindow != null) //sprawdzenie czy okno istnieje i zamknięcie go po poprawnym dodaniu
                if (_addProductTypeWindow.IsInitialized)
                    _addProductTypeWindow.Close();


            //odświeżenei listy
        }

        void DeleteProductType() // Usunięcie Typu produktu z bazy
        {
            if (SelectedProductType != null)
            {
                if (MessageBox.Show("Czy napewno chcesz usunąć?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status st = db.DeleteProductType(SelectedProductType);
                    if (st.State)
                        ProductTypeCollection = db.GetProductType();
                    else
                        MessageBox.Show("Błąd: " + st.Info);
                }
            }
        }

        

        ObservableCollection<ProductType> _productTypeCollection;   //obserwowalna kolekcja obiektów ProductType potrzebna do wyświetlania danych 
        public ObservableCollection<ProductType> ProductTypeCollection
        {
            get
            {
                return _productTypeCollection;
            }
            set
            {
                _productTypeCollection = value;
                RaisePropertyChanged("ProductTypeCollection");
            }
        }

        ProductType _selectedProductType;   //wybrany obiekt kolekcji obiektów ProductType potrzebna do wyświetlania danych 
        public ProductType SelectedProductType
        {
            get
            {
                return _selectedProductType;
            }
            set
            {
                _selectedProductType = value;
                RaisePropertyChanged("SelectedProductType");
            }
        }

        String _temporaryProductTypeName;   //zmienna wykorzystywana do dodawania i edycji typu produktu
        public String TemporaryProductTypeName
        {
            get
            {
                return _temporaryProductTypeName;
            }
            set
            {
                _temporaryProductTypeName = value;
                RaisePropertyChanged("TemporaryProductTypeName");
            }
        }

        public RelayCommand ShowWindowAddProductTypeCommand { get; private set; }
        public RelayCommand SaveProductTypeCommand { get; private set; }
        public RelayCommand DeleteProductTypeCommand { get; private set; }
        public RelayCommand ShowWindowEditProductTypeCommand { get; private set; }

        #endregion


    }
}
