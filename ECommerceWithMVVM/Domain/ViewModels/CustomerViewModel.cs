using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Views;
using System.Collections.ObjectModel;
using ECommerceWithMVVM.Domain.Sevices;
using ECommerceWithMVVM.Domain.Abstractions;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ProductService _productService;

        private ObservableCollection<Product> allProducts;

        public ObservableCollection<Product> AllProducts
        {
            get { return allProducts; }
            set { allProducts = value; OnPropertyChanged(); }
        }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; OnPropertyChanged(); }
        }

        private string filterText;

        public string FilterText
        {
            get { return filterText; }
            set { filterText = value; OnPropertyChanged(); }
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged(); }
        }


        public bool IsLower { get; set; } = false;

        public RelayCommand ToLowerCommand { get; set; }
        public RelayCommand SelectProductCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public Window myWindow { get; set; }
       
        public CustomerViewModel(IRepository<Product> productRepo, bool _isguest)
        {
            SearchText = String.Empty;
            FilterText = "Higher To Lower";
            _productRepo = productRepo;
            _productService = new ProductService();

            SelectedProduct = new Product();

            AllProducts = _productRepo.GetAllData();


            SearchCommand = new RelayCommand((o) =>
            {
                AllProducts = new ObservableCollection<Product>(_productRepo.GetAllData().Where(p => p.Name.ToLower().Contains(SearchText.ToLower()) || p.Name == String.Empty));
            });

            ToLowerCommand = new RelayCommand((o) =>
            {
                if (!IsLower)
                {
                    FilterText = "Lower To Higher";
                }
                else
                {
                    FilterText = "Higher To Lower";
                }
                IsLower = !IsLower;
                AllProducts = _productService.GetFromLowerToHigher(IsLower);
            });



            SelectProductCommand = new RelayCommand((o) =>
            {
                var view = new ProductWindow();
                var vm = new ProductViewModel();
                vm.ProductInfo = SelectedProduct;
                vm.MyWindow = view.window;


                if(_isguest is true)
                {
                    var grid = vm.MyWindow.Content as Grid;
                    var stackpanel = (grid.Children[0]as StackPanel).Children[0] as StackPanel;
                    var button = stackpanel.Children[11] as Button;
                    button.Content = string.Empty;
                    button.Visibility= Visibility.Hidden;
                }

                view.DataContext = vm;
                view.ShowDialog();
            });

        }
    }
}
