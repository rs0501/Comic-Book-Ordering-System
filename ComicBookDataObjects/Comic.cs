using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookDataObjects
{
    public class Comic
    {
        public int? ComicID { get; set; }
        public string ComicType { get; set; }
        public int DistributorID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int Issue { get; set; }
        public string Publisher { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
