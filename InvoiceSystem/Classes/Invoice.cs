using InvoiceSystem.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.OtherClasses
{
    public class Invoice: IEnumerable
    {
        #region Properties
        public string InvoiceNum { get; set; }
        public string InvoiceDate { get; set;}
        public string TotalCost { get; set;}
        public LineItems LineItems { get; set; } = new LineItems();
        public int TotalItems { get; set; } = 0;
        #endregion Properties

        /// <summary>
        /// Constructor that takes an invoice number, invoice date and total cost
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        public Invoice(string InvoiceNum, string InvoiceDate, string TotalCost)
        {
            this.InvoiceNum = InvoiceNum;
            this.InvoiceDate = InvoiceDate;
            this.TotalCost = TotalCost;
        }

        /// <summary>
        /// Constructor that takes an invoice date and total cost and lineitems object
        /// this is used when a new invoice is created from the gui., invoicenum is auto generated.
        /// </summary>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        public Invoice(string InvoiceDate, string TotalCost, LineItems LineItems)
        {
            
            this.InvoiceDate = InvoiceDate;
            this.TotalCost = TotalCost;
            this.LineItems = LineItems;
        }

        /// <summary>
        /// This method is important for adding all new Items to an invoice so the line item is updated and that the totalItems 
        /// property is also updated.
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="quantity"></param>
        public void addItem(Item Item, int quantity)
        {
            LineItems.addLineItem(new LineItem ( Item, quantity ));
            TotalItems += quantity;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}