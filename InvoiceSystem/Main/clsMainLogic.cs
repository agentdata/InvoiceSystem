using InvoiceSystem.Classes;
using InvoiceSystem.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Main
{
    public static class clsMainLogic
    {
        /// <summary>
        /// This method calculates the new totalcost for an invoice after the lineitems are updated.
        /// </summary>
        /// <param name="Invoice"></param>
        public static void updateInvoiceTotalCost(Invoice Invoice)
        {
            //get new total cost
            int newCost = 0;
            foreach(LineItem LineItem in Invoice.LineItems.lineItems)
            {
                int itemCost;
                if (Int32.TryParse(LineItem.Item.Cost, out itemCost))
                {
                    if (LineItem.Quantity > 1)
                    {
                        itemCost = itemCost * LineItem.Quantity;
                    }
                    newCost += itemCost;
                }
            }
            clsMainSQL.updateInvoiceTotalCost(Invoice.InvoiceNum, newCost.ToString());
        }
    }
}