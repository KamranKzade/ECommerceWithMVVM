using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.DataAccess.Repositories;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace ECommerceWithMVVM.Domain.ViewModels
{

    public class SingInViewModel : BaseViewModel
    {
        private readonly IAdminRepository _adminRepo;
        private readonly ICustomerRepository _customerRepo;

        public RelayCommand SignIn { get; set; }
        public RelayCommand SignUp { get; set; }
        public RelayCommand Close_Btn { get; set; }
        public RelayCommand Minimize_Btn { get; set; }




        public SingInViewModel(bool _IsCustomer)
        {

            _adminRepo = new AdminRepository();
            _customerRepo = new CustomerRepository();


            Minimize_Btn = new RelayCommand((o) =>
            {
                var window = o as Window;
                window.WindowState = WindowState.Minimized;

            });

            Close_Btn = new RelayCommand((o) =>
            {
                Application.Current.Shutdown();
            });


            SignIn = new RelayCommand((o) =>
            {
                int? result = null;

                // Customer Sehifesi
                if (_IsCustomer is true)
                {

                    var stackPanel = o as StackPanel;
                    var username = stackPanel.Children[2] as TextBox;
                    var password = stackPanel.Children[4] as PasswordBox;

                    _customerRepo.CheckCustomer(username.Text, password.Password, ref result);

                    if (result is 1)
                    {
                        var productRepo = new ProductRepository();
                        CustomerWindow window = new CustomerWindow();
                        var vm = new CustomerViewModel(productRepo);

                        window.DataContext = vm;

                        window.ShowDialog();
                    }
                    else
                        MessageBox.Show("Username or Password is Wrong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Admin sehifesi
                else
                {
                    var stackPanel = o as StackPanel;
                    var username = stackPanel.Children[2] as TextBox;
                    var password = stackPanel.Children[4] as PasswordBox;


                    _adminRepo.CheckAdmin(username.Text, password.Password, ref result);

                    if (result is 1)
                    {
                        MessageBox.Show("New Page is loading");
                    }
                    else
                        MessageBox.Show("Username or Password is Wrong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });


            SignUp = new RelayCommand((o) =>
            {
                if (_IsCustomer is true)
                {
                    SignUpWindow window = new SignUpWindow();
                    var vm = new SignUpViewModel();
                    window.DataContext = vm;
                    window.ShowDialog();

                }
                else
                    MessageBox.Show("Admin adi ile qeydiyyatdan kecmek mummkun deyil", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            });


        }
    }
}