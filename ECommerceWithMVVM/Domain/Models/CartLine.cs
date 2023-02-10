using ECommerceWithMVVM.DataAccess;


namespace ECommerceWithMVVM.Domain.Models
{
    public class CartLine
    {
        public int Amount { get; set; }
        public Product Product { get; set; }
    }
}
