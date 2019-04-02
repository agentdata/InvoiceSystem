using InvoiceSystem.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.OtherClasses
{
    public class Invoice
    {
        public string InvoiceNum { get; set; }
        public string InvoiceDate { get; set;}
        public string TotalCost { get; set;}
        public ObservableCollection<LineItem> LineItems { get; set; } = new ObservableCollection<LineItem>();
        public int TotalItems { get; set; } = 0;

        public Invoice(string InvoiceNum, string InvoiceDate, string TotalCost)
        {
            this.InvoiceNum = InvoiceNum;
            this.InvoiceDate = InvoiceDate;
            this.TotalCost = TotalCost;
        }

        public void addItem(Item Item, int quantity)
        {
            LineItems.Add(new LineItem ( Item, quantity ));
            TotalItems += quantity;
        }
    }
}