using System;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using System.Collections.ObjectModel;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;
using ECommerceWithMVVM.Domain.Views;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        public RelayCommand InsertCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }

        public IRepository<Customer> _customerRepo { get; set; }
        public IRepository<Product> _productRepo { get; set; }


        private Customer selectedProduct;
        public Customer SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; OnPropertyChanged(); }
        }

        public AdminViewModel()
        {
            _customerRepo = new CustomerRepository();
            _productRepo = new ProductRepository();
            SelectedProduct = new Customer();


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

                        if (customer != null)
                        {
                            _customerRepo.DeleteData(customer);
                            MessageBox.Show("Successfully, Deleted Customer", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Zehmet olmasa Customerlerden birini secin", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

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

                        if (product != null)
                        {
                            _productRepo.DeleteData(product);
                            MessageBox.Show("Successfully, Deleted Product", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Zehmet olmasa Productlardan birini secin", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

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

            InsertCommand = new RelayCommand((o) =>
            {
                var myGrid = o as Grid;

                var stackpanel = myGrid.Children[1] as StackPanel;
                var customer_radio_button = stackpanel.Children[0] as RadioButton;
                var product_radio_button = stackpanel.Children[1] as RadioButton;
                var scroll = myGrid.Children[2] as ScrollViewer;
                var dataGrid = scroll.Content as DataGrid;

                if (customer_radio_button.IsChecked is true)
                {
                    InsertCustomerWindow window = new InsertCustomerWindow();
                    var vm = new InsertCustomerViewModel(_customerRepo);
                    window.DataContext = vm;

                    window.ShowDialog();
                }
                else if (product_radio_button.IsChecked is true)
                {
                    InsertProductWindow window = new InsertProductWindow();
                    var vm = new InsertProductViewModel(_productRepo);
                    window.DataContext = vm;

                    window.ShowDialog();
                }
                else
                    MessageBox.Show("Zehmet olmasa secimlerden birini edin", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            });

            UpdateCommand = new RelayCommand((o) =>
            {
                var myGrid = o as Grid;

                var stackpanel = myGrid.Children[1] as StackPanel;
                var customer_radio_button = stackpanel.Children[0] as RadioButton;
                var product_radio_button = stackpanel.Children[1] as RadioButton;
                var scroll = myGrid.Children[2] as ScrollViewer;
                var dataGrid = scroll.Content as DataGrid;

                if (customer_radio_button.IsChecked is true)
                {
                    var customer = dataGrid.SelectedItem as Customer;

                    var view = new UpdateCustomerWindow();

                    view.userName.Text = customer.Username;
                    view.password.Text = customer.Password;

                    var vm = new UpdateCustomerViewModel(customer, _customerRepo);

                    view.DataContext = vm;

                    view.ShowDialog();

                }
                else if (product_radio_button.IsChecked is true)
                {
                    var product = dataGrid.SelectedItem as Product;

                    var view = new UpdateProductWindow();
                    view.productName.Text = product.Name;
                    view.descr.Text = product.Description;
                    view.price.Text=product.Price.ToString();
                    view.discount.Text=product.Discount.ToString();
                    view.quantity.Text = product.Quantity.ToString();

                    var vm = new UpdateProductViewModel(_productRepo, product);
                    view.DataContext = vm;
                    view.ShowDialog();
                }
                else
                    MessageBox.Show("Zehmet olmasa secimlerden birini edin", "Information", MessageBoxButton.OK, MessageBoxImage.Information);


            });
        }
    }
}