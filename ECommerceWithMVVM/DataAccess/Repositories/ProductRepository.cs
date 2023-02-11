using System.Linq;
using ECommerceWithMVVM.Domain.Abstractions;
using System.Collections.ObjectModel;

namespace ECommerceWithMVVM.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDataClassesDataContext _dataContext;

        public ProductRepository()
        {
            _dataContext = new ECommerceDataClassesDataContext();
        }

        public void AddData(Product data)
        {
            _dataContext.Products.InsertOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public void DeleteData(Product data)
        {
            _dataContext.Products.DeleteOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public ObservableCollection<Product> GetAllData()
        {
            var products = from p in _dataContext.Products
                           select p;

            return new ObservableCollection<Product>(products);
        }

        public Product GetData(int id)
        {
            return _dataContext.Products.SingleOrDefault(p => p.Id == id);
        }

        public void UpdateData(Product data)
        {
            var item = _dataContext.Products.SingleOrDefault(p => p.Id == data.Id);

            item.Price = data.Price;
            item.Quantity = data.Quantity;
            item.Discount = data.Discount;


            _dataContext.SubmitChanges();
        }
    }
}
