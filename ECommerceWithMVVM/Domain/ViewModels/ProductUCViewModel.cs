using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWithMVVM.Domain.ViewModels
{
    public class ProductUCViewModel:BaseViewModel
    {
        private ObservableCollection<Product> allProducts;
        private readonly IRepository<Product> _productRepo;

        public ObservableCollection<Product> AllProducts
        {
            get { return allProducts; }
            set { allProducts = value; OnPropertyChanged(); }
        }

        public ProductUCViewModel(IRepository<Product> productRepo)
        {
            allProducts= new ObservableCollection<Product>();
            _productRepo= productRepo;
            allProducts = _productRepo.GetAllData();



        }
    }
}
