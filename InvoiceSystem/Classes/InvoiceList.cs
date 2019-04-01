using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.OtherClasses
{
    
    public class InvoiceList
    {
        public ObservableCollection<Invoice> InvoicesCollection { get; set; } = new ObservableCollection<Invoice>();
        public int totalInvoices { get { return InvoicesCollection.Count(); } }
        public InvoiceList()
        {

        }
        
        public void addInvoice(Invoice Invoice)
        {
            InvoicesCollection.Add(Invoice);
        }
    }
}