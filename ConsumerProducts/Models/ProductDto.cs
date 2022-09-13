using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerProducts.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? FullDescription { get; set; }
        public string? ShortDescription { get; set; }
        public decimal  Price { get; set; }
        public bool Published { get; set; }



    }
}
