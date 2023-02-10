using System;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using System.Collections.ObjectModel;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {

        private ObservableCollection<Product> allProducts;

        public ObservableCollection<Product> AllProducts
        {
            get { return allProducts; }
            set { allProducts = value; OnPropertyChanged(); }
        }



        public RelayCommand InsertCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }
        public IRepository<Customer> _customerRepo { get; set; }
        public IRepository<Product> _productRepo { get; set; }


        public AdminViewModel()
        {
            _customerRepo = new CustomerRepository();
            _productRepo = new ProductRepository();


            DeleteCommand = new RelayCommand((o) =>
            {

                var myGrid = o as Grid;

                var stackpanel = myGrid.Children[1] as StackPanel;
                var customer_radio_button = stackpanel.Children[0] as RadioButton;
                var product_radio_button = stackpanel.Children[1] as RadioButton;
                var scroll = myGrid.Children[2] as ScrollViewer;
                var dataGrid = scroll.Content as DataGrid;


                if (customer_radio_button.IsChecked is true)
                {
                    try
                    {
                        var customer = dataGrid.SelectedItem as Customer;
                        _customerRepo.DeleteData(customer);
                        MessageBox.Show("Successfully, Deleted Customer", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else if (product_radio_button.IsChecked is true)
                {
                    try
                    {
                        var product = dataGrid.SelectedItem as Product;
                        _productRepo.DeleteData(product);
                        MessageBox.Show("Successfully, Deleted Product", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }


            });

            ShowAllCommand = new RelayCommand((o) =>
            {
                var myGrid = o as Grid;

                var stackpanel = myGrid.Children[1] as StackPanel;
                var customer_radio_button = stackpanel.Children[0] as RadioButton;
                var product_radio_button = stackpanel.Children[1] as RadioButton;
                var scroll = myGrid.Children[2] as ScrollViewer;
                var dataGrid = scroll.Content as DataGrid;


                if (dataGrid.ItemsSource != null)
                {
                    dataGrid.ItemsSource = null;
                }


                if (customer_radio_button.IsChecked is true)
                {
                    dataGrid.ItemsSource = _customerRepo.GetAllData();
                }
                else if (product_radio_button.IsChecked is true)
                {
                    dataGrid.ItemsSource = _productRepo.GetAllData();
                }
                else
                    MessageBox.Show("Zehmet olmasa secimlerden birini edin", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            });
        }


    }
}
