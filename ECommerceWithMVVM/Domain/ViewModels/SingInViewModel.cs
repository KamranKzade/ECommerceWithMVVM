using ECommerceWithMVVM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class SingInViewModel : BaseViewModel
    {
        public RelayCommand Minimize_Btn { get; set; }
        public RelayCommand Close_Btn { get; set; }
        public RelayCommand SignIn { get; set; }
        public RelayCommand SignUp { get; set; }
        public SingInViewModel()
        {

            Minimize_Btn = new RelayCommand( (o) =>
            {
                var window = o as Window;
                window.WindowState = WindowState.Minimized;

            });

            Close_Btn = new RelayCommand( (o) =>
            {
                Application.Current.Shutdown();
            });
        }


    }
}

