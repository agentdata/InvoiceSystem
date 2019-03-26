using InvoiceSystem.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Main
{
    class clsMainLogic
    {
        clsMainSQL sqlLogic = new clsMainSQL();
        //InvoiceList Invoices = new InvoiceList();
        public InvoiceList Invoices { get; set; }

        public clsMainLogic()
        {
            Invoices = new InvoiceList();
        }

        public void LoadInvoices()
        {
            Invoices = sqlLogic.getAllInvoices(Invoices);
        }
    }
}