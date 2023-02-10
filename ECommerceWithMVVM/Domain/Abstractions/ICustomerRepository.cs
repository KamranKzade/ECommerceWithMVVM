using ECommerceWithMVVM.DataAccess;

namespace ECommerceWithMVVM.Domain.Abstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void CheckCustomer(string username, string password, ref int? result);
    }
}
