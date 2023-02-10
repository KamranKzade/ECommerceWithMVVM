using System;
using System.Windows;
using ECommerceWithMVVM.Commands;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Sevices;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class ProductViewModel :BaseViewModel
    {

        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly CustomerService _customerService;
        public RelayCommand OrderCommand { get; set; }
        public ProductViewModel()
        {
            _productRepo = new ProductRepository();
            _orderRepo = new OrderRepository();
            _customerService = new CustomerService();

            ProductInfo = new Product();

            OrderCommand = new RelayCommand((o) =>
            {
                var customer = _customerService.GetCustomerByUsername(Username);
                if (customer != null)
                {
                    if (ProductInfo.Quantity >= Amount)
                    {
                        ProductInfo.Quantity -= Amount;

                        var order = new Order
                        {
                            Amount = Amount,
                            CustomerId = customer.Id,
                            ProductId = ProductInfo.Id,
                            Date = DateTime.Now
                        };

                        _productRepo.UpdateData(ProductInfo);
                        _orderRepo.AddData(order);

                        MessageBox.Show("Order added successfully");
                    }
                    else
                    {
                        MessageBox.Show($"We have only this {ProductInfo.Quantity} amount");
                    }
                }
                else
                {
                    MessageBox.Show($"{username} customer does not exist");
                }
            });
        }

        private Product productInfo;

        public Product ProductInfo
        {
            get { return productInfo; }
            set { productInfo = value; OnPropertyChanged(); }
        }


        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

    }
}
