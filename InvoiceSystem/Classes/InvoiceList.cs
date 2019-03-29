using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.OtherClasses
{
    
    public class InvoiceList
    {
        List<Invoice> Invoices { get; set; }
        
        public void addInvoice(Invoice Invoice)
        {
            Invoices.Add(Invoice);
        }
    }
}