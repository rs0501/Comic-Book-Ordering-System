using ComicBookDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentation.Models
{
    public class Cart
    {
        private List<OrderLine> lineCollection = new List<OrderLine>();
        
        public void AddItem(Comic comic, int quantity)
        {
            OrderLine line = lineCollection
                .Where(p => p.ComicID == comic.ComicID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new OrderLine
                {
                    ComicID = (int)comic.ComicID,
                    Quantity = quantity
                });
            } else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Comic comic)
        {
            lineCollection.RemoveAll(l => l.ComicID == comic.ComicID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<OrderLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Comic Comic { get; set; }
        public int Quantity { get; set; }
    }
}