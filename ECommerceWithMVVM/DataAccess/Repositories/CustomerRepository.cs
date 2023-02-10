using System.Linq;
using System.Collections.ObjectModel;
using ECommerceWithMVVM.Domain.Abstractions;

namespace ECommerceWithMVVM.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly ECommerceDataClassesDataContext _dataContext;

        public CustomerRepository()
        {
            _dataContext = new ECommerceDataClassesDataContext();
        }

        public void AddData(Customer data)
        {
            _dataContext.Customers.InsertOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public void DeleteData(Customer data)
        {
            _dataContext.Customers.DeleteOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public ObservableCollection<Customer> GetAllData()
        {
            var customers = from c in _dataContext.Customers
                            select c;

            return new ObservableCollection<Customer>(customers);
        }

        public Customer GetData(int id)
        {
            return _dataContext.Customers.SingleOrDefault(c => c.Id == id);
        }

        public void UpdateData(Customer data)
        {
            var item = _dataContext.Customers.SingleOrDefault(c => c.Id == data.Id);

            item = new Customer
            {
                Id = data.Id,
                Orders = data.Orders,
                Username = data.Username,
                Password = data.Password,
            };
            _dataContext.SubmitChanges();
        }
    }
}
