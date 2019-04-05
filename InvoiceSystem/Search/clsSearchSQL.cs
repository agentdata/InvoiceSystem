using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using InvoiceSystem.Search;

namespace InvoiceSystem.Search
{
    class clsSearchSQL
    {
        //contains the SQL logic

        /// <summary>
        /// stores the invoice numbers in this collection
        /// </summary>
        ObservableCollection<InvoiceInfo> invoiceNums;
        /// <summary>
        /// stores the invoice costs in this 
        /// </summary>
        ObservableCollection<InvoiceInfo> invoiceCosts;
        /// <summary>
        /// stores the information from invoices here 
        /// </summary>
        ObservableCollection<InvoiceInfo> Invoices;
        /// <summary>
        /// clsDataAccess object
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        InvoiceData InvoiceData = new InvoiceData();
        ///returns the invoice numbers
        ///
        public ObservableCollection<InvoiceInfo> getInvoiceNums()
        {
            invoiceNums = new ObservableCollection<InvoiceInfo>();
            DataSet ds;

            int res = 0;

            string SQLstatment = "SELECT InvoiceNum FROM Invoices";

            ds = db.ExecuteSQLStatement(SQLstatment, ref res);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                invoiceNums.Add(new InvoiceInfo
                {
                    InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNum"].ToString()
                });
            }

            return invoiceNums;
        }

        /// <summary>
        /// returns the total costs
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> getTotalCosts()
        {
            invoiceCosts = new ObservableCollection<InvoiceInfo>();

            DataSet ds;

            int res = 0;
            string SQLStatment = "SELECT * FROM Invoices ORDER BY TotalCost ASC";
            ds = db.ExecuteSQLStatement(SQLStatment, ref res);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                invoiceCosts.Add(new InvoiceInfo
                {
                    TotalCosts = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                });
            }
            return invoiceCosts;
        }

        /// <summary>
        /// gets the invoices from the Invoices Database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> GetInvoices()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;
            string SQLstatement = "Select * FROM Invoices";

            ds = db.ExecuteSQLStatement(SQLstatement, ref res);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Invoices.Add(new InvoiceInfo
                {
                    InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNum"].ToString(),
                    InvoiceDates = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                    TotalCosts = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                });
            }
            return Invoices;
        }

        /// <summary>
        /// for the search if only the InvoiceNum ComboBox is used
        /// </summary>
        /// <param name="selected">the selected value from the combobox</param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchInvoiceNumbers(string selected)
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + selected;
            ds = db.ExecuteSQLStatement(SQLStatment, ref res);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Invoices.Add(new InvoiceInfo
                {
                    InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNum"].ToString(),
                    InvoiceDates = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                    TotalCosts = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                }
                    );
            }
            return Invoices;

        }

        /// <summary>
        /// does a search of the totalcosts chosen by the user and returns any Invoices from the database
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchTotalCosts(string selected)
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;
            string SQLStatment = "SELECT * FROM Invoices WHERE TotalCost = " + selected;
            ds = db.ExecuteSQLStatement(SQLStatment, ref res);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Invoices.Add(new InvoiceInfo
                {
                    InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNum"].ToString(),
                    InvoiceDates = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                    TotalCosts = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                }
                    );
            }
            return Invoices;

        }
    }
}
