using System.Collections.Generic;


namespace ECommerceWithMVVM.Domain.Models
{
    public class Cart
    {
        public string CustomerName { get; set; }
        public List<CartLine> CartLines { get; set; }
    }
}
