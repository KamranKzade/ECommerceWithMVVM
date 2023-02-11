using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.DataAccess.Repositories;
using ECommerceWithMVVM.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class UpdateProductViewModel : BaseViewModel
    {
        public IRepository<Product> Repository { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        public UpdateProductViewModel(IRepository<Product> _Repository, Product product)
        {
            Repository = new ProductRepository();
            Repository = _Repository;

            UpdateCommand = new RelayCommand((o) =>
            {
                var window = o as Window;
                var grid = window.Content as Grid;


                var mygrid = grid.Children[1] as Grid;
                
                var productName = (mygrid.Children[5] as TextBox).Text;
                var description = (mygrid.Children[6] as TextBox).Text;
                var priceText = (mygrid.Children[7] as TextBox).Text;
                decimal price = decimal.Parse(priceText);
                var discountText = (mygrid.Children[8] as TextBox).Text;
                int discount = int.Parse(discountText);
                var quantityText = (mygrid.Children[9] as TextBox).Text;
                int quantity = int.Parse(quantityText);

                Product productNew = new Product
                {
                    Name = productName,
                    Description = description,
                    Price = price,
                    Discount = discount,
                    Quantity = quantity,
                    Id= product.Id
                };

                try
                {
                    Repository.UpdateData(productNew);
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
