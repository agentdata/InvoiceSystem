using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wndSearch
{
    class InvoiceInfo
    {
        /// <summary>
        /// stores the invoice numbers
        /// </summary>
        public string InvoiceNumber { get; set; }
        ///<summary>stores the invoiceDates from Invoices
        ///</summary>
        public string InvoiceDates { get; set; }
        /// <summary>
        /// stores the total costs from Invoices
        /// </summary>
        public string TotalCosts { get; set; }
        

    }
}
