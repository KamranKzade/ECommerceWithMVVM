using System;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class UpdateCustomerViewModel : BaseViewModel
    {
        public RelayCommand UpdateCommand { get; set; }
        public IRepository<Customer> Repository { get; set; }


        public UpdateCustomerViewModel(Customer customer, IRepository<Customer> customers)
        {
            Repository = new CustomerRepository();
            Repository = customers;


            UpdateCommand = new RelayCommand((o) =>
            {
                var window = o as Window;
                var grid1 = window.Content as Grid;


                var mygrid1 = grid1.Children[1] as Grid;
                var username1 = (mygrid1.Children[2] as TextBox).Text;
                var password1 = (mygrid1.Children[3] as TextBox).Text;

                Customer customer1 = new Customer
                {
                    Id = customer.Id,
                    Username = customer.Username,
                    Password = password1,
                    Orders = customer.Orders
                };

                try
                {
                    Repository.UpdateData(customer1);
                    MessageBox.Show("Successfully, Update Customer", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    window.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }

            });
        }
    }
}
