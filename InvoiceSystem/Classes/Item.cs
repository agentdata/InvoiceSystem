using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public string Cost { get; set; }

        public Item(string ItemCode, string ItemDesc, string Cost)
        {
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Cost = Cost;
        }
    }
}