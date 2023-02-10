using System.Linq;
using System.Collections.ObjectModel;
using ECommerceWithMVVM.Domain.Abstractions;

namespace ECommerceWithMVVM.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDataClassesDataContext _dataContext;

        public OrderRepository()
        {
            _dataContext = new ECommerceDataClassesDataContext();
        }

        public void AddData(Order data)
        {
            _dataContext.Orders.InsertOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public void DeleteData(Order data)
        {
            _dataContext.Orders.DeleteOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public ObservableCollection<Order> GetAllData()
        {
            var orders = from o in _dataContext.Orders
                         select o;
            return new ObservableCollection<Order>(orders);
        }

        public Order GetData(int id)
        {
            return _dataContext.Orders.SingleOrDefault(o => o.Id == id);
        }

        public void UpdateData(Order data)
        {
            var item = _dataContext.Orders.SingleOrDefault(o => o.Id == data.Id);

            item = new Order
            {
                Id = data.Id,
                Date = data.Date,
                Amount = data.Amount,
                ProductId = data.ProductId,
                CustomerId = data.CustomerId,
            };
            _dataContext.SubmitChanges();
        }
    }
}
