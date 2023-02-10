using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class InsertProductViewModel : BaseViewModel
    {
        public RelayCommand InsertCommand { get; set; }
        public IRepository<Product> _productRepo { get; set; }


        public InsertProductViewModel(IRepository<Product> customers)
        {
            _productRepo = new ProductRepository();

            _productRepo = customers;

            InsertCommand = new RelayCommand((o) =>
            {
                var window = o as Window;
                var grid = window.Content as Grid;


                var mygrid = grid.Children[1] as Grid;
                var productName = (mygrid.Children[5] as TextBox).Text;
                var description = (mygrid.Children[6] as TextBox).Text;
                var priceText = (mygrid.Children[7] as TextBox).Text;
                int price = int.Parse(priceText);
                var discountText = (mygrid.Children[8] as TextBox).Text;
                int discount = int.Parse(discountText); 
                var quantityText = (mygrid.Children[9] as TextBox).Text;
                int quantity = int.Parse(quantityText);

                int id = _productRepo.GetAllData().Last().Id;

                Product product = new Product
                {
                    Id = ++id,
                    Name = productName,
                    Description = description,
                    Price = price,
                    Discount = discount,
                    Quantity= quantity
                };

                _productRepo.AddData(product);

                MessageBox.Show("Successfully, Insert Product", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                window.Close();
            });
        }
    }
}
