using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }
    }
}
