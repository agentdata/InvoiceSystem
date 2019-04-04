using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class LineItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public LineItem(Item Item, int Quantity)
        {
            this.Item = Item;
            this.Quantity = Quantity;
        }
    }
}