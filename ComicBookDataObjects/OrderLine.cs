using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookDataObjects
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }
        public int ComicID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
