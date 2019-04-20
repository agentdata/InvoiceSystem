using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class Item
    {
        /// <summary>
        /// Corresponds to the ItemCode Attribute in the ItemDesc Table.
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Corresponds to the ItemDesc Attribute in the ItemDesc Table.
        /// </summary>
        public string ItemDesc { get; set; }
        /// <summary>
        /// Corresponds ot the Cost Attribute in the ItemDesc Table.
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// Item Class constructor.  Sets all public properties.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        public Item(string ItemCode, string ItemDesc, string Cost)
        {
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Cost = Cost;
        }
    }
}