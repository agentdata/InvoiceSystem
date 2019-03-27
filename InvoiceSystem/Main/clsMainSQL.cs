using System;
using InvoiceSystem.OtherClasses;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace InvoiceSystem.Main
{
    class clsMainSQL
    {
        string strDSN = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Invoice.accdb";
        public clsMainSQL()
        {

        }

        public InvoiceList getAllInvoices(InvoiceList Invoices)
        {   
            string strSQL = "SELECT * FROM Invoices";
            // create Objects of ADOConnection and ADOCommand  
            OleDbConnection myConn = new OleDbConnection(strDSN);
            OleDbDataAdapter myAdapter = new OleDbDataAdapter(strSQL, myConn);
            
            //var command = myConn.CreateCommand();
            //command.CommandText = "SELECT * FROM Invoices";

            //do stuff with invoicelist object passed in
            


            return Invoices;
        }
    }
}