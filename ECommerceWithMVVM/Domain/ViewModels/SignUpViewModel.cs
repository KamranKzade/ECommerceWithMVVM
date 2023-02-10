using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;


namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private ICustomerRepository _customerRepo;

        public RelayCommand Minimize_Btn { get; set; }
        public RelayCommand Close_Btn { get; set; }
        public RelayCommand SignUp { get; set; }

        public SignUpViewModel()
        {
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

            SignUp = new RelayCommand((o) =>
            {
                var window = o as Window;
                var border = window.Content as Border;
                var border2 = border.Child as Border;
                var grid = border2.Child as Grid;

                var stackPanel = grid.Children[1] as StackPanel;

                var username = stackPanel.Children[2] as TextBox;
                var password = stackPanel.Children[4] as PasswordBox;
                var confirmpassword = stackPanel.Children[6] as PasswordBox;


                int count = 0;
                StringBuilder sb = new StringBuilder();

                if (string.IsNullOrWhiteSpace(username.Text))
                {
                    sb.Append("Username-i daxil edin\n");
                    count++;
                }
                if (string.IsNullOrWhiteSpace(password.Password))
                {
                    sb.Append("Password-u daxil edin\n");
                    count++;
                }
                if (string.IsNullOrWhiteSpace(confirmpassword.Password))
                {
                    sb.Append("Confirm Password-u daxil edin\n");
                    count++;
                }
                if (count > 0)
                    MessageBox.Show($"{sb}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (!char.IsUpper(username.Text[0]))
                    {
                        sb.Append("Username-in ilk herfi boyuk ile baslamalidir\n");
                        count++;
                    }
                    if (!char.IsUpper(password.Password[0]))
                    {
                        sb.Append("Password-un ilk herfi boyuk ile baslamalidir\n");
                        count++;
                    }
                    if (!char.IsUpper(confirmpassword.Password[0]))
                    {
                        sb.Append("Confirm Password-un ilk herfi boyuk ile baslamalidir\n");
                        count++;
                    }
                    if (username.Text.Length < 4)
                    {
                        sb.Append($"Username 3 herfden boyuk olmalidir\n");
                        count++;
                    }
                    if (password.Password.Length < 4)
                    {
                        sb.Append($"Username 3 herfden boyuk olmalidir\n");
                        count++;
                    }
                    if (confirmpassword.Password.Length < 4)
                    {
                        sb.Append($"Username 3 herfden boyuk olmalidir\n");
                        count++;
                    }
                    if (count > 0)
                        MessageBox.Show($"{sb}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        if (password.Password != confirmpassword.Password)
                        {
                            MessageBox.Show("Password = Confirm Passworda", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            var customersId = _customerRepo.GetAllData().Last().Id;


                            Customer customer = new Customer
                            {
                                Id = ++customersId,
                                Username = username.Text,
                                Password = password.Password,
                            };

                            _customerRepo.AddData(customer);

                            MessageBox.Show("Customer Successfully Added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            window.Close();
                        }
                    }
                }

            });
        
        
        }


    }
}
