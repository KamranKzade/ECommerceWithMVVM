using System.Linq;
using ECommerceWithMVVM.DataAccess;
using System.Collections.ObjectModel;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;

namespace ECommerceWithMVVM.Domain.Sevices
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepo;
        public ProductService()
        {
            _productRepo = new ProductRepository();
        }


        public ObservableCollection<Product> GetFromLowerToHigher(bool isLower = false)
        {
            IOrderedEnumerable<Product> items = null;

            if (isLower)
            {
                items = from p in _productRepo.GetAllData()
                        orderby p.Price ascending
                        select p;
            }
            else
            {
                items = from p in _productRepo.GetAllData()
                        orderby p.Price descending
                        select p;
            }
            
            return new ObservableCollection<Product>(items);
        }
    }
}
