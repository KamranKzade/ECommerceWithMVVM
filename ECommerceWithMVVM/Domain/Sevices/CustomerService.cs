using System.Linq;
using ECommerceWithMVVM.DataAccess;
using ECommerceWithMVVM.Domain.Abstractions;
using ECommerceWithMVVM.DataAccess.Repositories;

namespace ECommerceWithMVVM.Domain.Sevices
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _customerRepo;

        public CustomerService()
        {
            _customerRepo = new CustomerRepository();
        }

        public Customer GetCustomerByUsername(string username)
        {
            var customer = _customerRepo.GetAllData().FirstOrDefault(c => c.Username == username);
            return customer;
        }


    }
}
