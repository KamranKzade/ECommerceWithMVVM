using System.Windows;
using ECommerceWithMVVM.Domain.ViewModels;


namespace ECommerceWithMVVM.Domain.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel vm = new MainViewModel();
            this.DataContext= vm;
        }
    }
}
