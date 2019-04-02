using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class LineItem
    {
        Item Item { get; set; }
        int Quantity { get; set; }

        public LineItem(Item Item, int Quantity)
        {
            this.Item = Item;
            this.Quantity = Quantity;
        }
    }
}
