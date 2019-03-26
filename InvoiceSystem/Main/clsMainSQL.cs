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
        string strDSN = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = InvoiceDB.accdb";
        public clsMainSQL()
        {

        }

        public InvoiceList getAllInvoices(InvoiceList Invoices)
        {   
            string strSQL = "SELECT * FROM Invoices";
            // create Objects of ADOConnection and ADOCommand  
            OleDbConnection myConn = new OleDbConnection(strDSN);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(strSQL, myConn);

            myConn.Open();


            //do stuff with invoicelist object passed in

            myConn.Close();


            return Invoices;
        }
    }
}