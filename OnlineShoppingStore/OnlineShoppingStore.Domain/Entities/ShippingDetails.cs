using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage="Please enter a name")]
        public string name { get; set; }
        [Required(ErrorMessage ="Please enter the first adress line")]
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
        [Required(ErrorMessage ="Please enter a city name")]
        public string city { get; set; }
        [Required(ErrorMessage ="please enter a state name")]
        public string state { get; set; }
        [Required(ErrorMessage ="Please enter a country name")]
        public string Country { get; set; }
        public bool Giftwrap { get; set; }
    }
}
