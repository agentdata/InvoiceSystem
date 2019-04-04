using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace InvoiceSystem.Search
{
    class InvoiceData
    {
       public ObservableCollection<InvoiceInfo> invoices { get; set; } = new ObservableCollection<InvoiceInfo>();

        public void addInvoices(string InvoiceNumbers, string InvoiceDate,string TotalCost)
        {
            invoices.Add(new InvoiceInfo
            {
                InvoiceNumber = InvoiceNumbers,
                InvoiceDates = InvoiceDate,
                TotalCosts = TotalCost
            });
        }
    }
}
