using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess.Repositories;
using ECommerceWithMVVM.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand Click { get; set; }
        public RelayCommand Minimize_Btn { get; set; }
        public RelayCommand Close_Btn { get; set; }

        bool _isCustomer;

        public MainViewModel()
        {
            Minimize_Btn = new RelayCommand((o) =>
            {
                var window = o as Window;
                window.WindowState = WindowState.Minimized;

            });

            Close_Btn = new RelayCommand((o) =>
            {
                Application.Current.Shutdown();
            });

            Click = new RelayCommand((o) =>
            {
                var button = o as Button;

                if (button.Name == "Admin")
                {
                    _isCustomer = false;

                    SingInWindow singInWindow = new SingInWindow();
                    SingInViewModel singInViewModel = new SingInViewModel(_isCustomer);
                    singInWindow.DataContext = singInViewModel;

                    singInWindow.ShowDialog();
                }
                else if (button.Name == "Customer")
                {
                    _isCustomer = true;

                    SingInWindow singInWindow = new SingInWindow();
                    SingInViewModel singInViewModel = new SingInViewModel(_isCustomer);
                    singInWindow.DataContext = singInViewModel;

                    singInWindow.ShowDialog();
                }
                else
                {

                    var productRepo = new ProductRepository();
                    CustomerWindow window = new CustomerWindow();
                    var vm = new CustomerViewModel(productRepo);

                    window.DataContext = vm;

                    window.ShowDialog();
                }
            });
        }
    }
}
