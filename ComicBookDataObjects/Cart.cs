using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookDataObjects
{
    public class Cart
    {
        public List<OrderLine> OrderLines { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }

    }
}
