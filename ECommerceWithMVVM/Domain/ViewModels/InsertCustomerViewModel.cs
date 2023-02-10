using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;


namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class InsertCustomerViewModel : BaseViewModel
    {
        public RelayCommand InsertCommand { get; set; }
        public IRepository<Customer> _customerRepo { get; set; }


        public InsertCustomerViewModel(IRepository<Customer> customers)
        {
            _customerRepo = new CustomerRepository();

            _customerRepo = customers;

            InsertCommand = new RelayCommand((o) =>
            {
                var window = o as Window;
                var grid = window.Content as  Grid;


               var mygrid = grid.Children[1] as Grid;
               var username = (mygrid.Children[2] as TextBox).Text;
               var password = (mygrid.Children[3] as TextBox).Text;

                int id = _customerRepo.GetAllData().Last().Id;
                Customer customer = new Customer
                {
                    Id = ++id,
                    Username = username,
                    Password = password
                };

                _customerRepo.AddData(customer);
                MessageBox.Show("Successfully, Insert Customer", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                window.Close();
            });
        }
    }
}
