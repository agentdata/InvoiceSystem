using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace InvoiceSystem.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// objuect for the class clsSearchSQL
        /// </summary>
        clsSearchSQL searchSQL;
        /// <summary>
        /// stores the invoice numbers in this collection
        /// </summary>
        ObservableCollection<InvoiceInfo> invoiceNums { get; set; } = new ObservableCollection<InvoiceInfo>();
        /// <summary>
        /// stores the invoice costs in this 
        /// </summary>
        ObservableCollection<InvoiceInfo> invoiceCosts { get; set; } = new ObservableCollection<InvoiceInfo>();
        /// <summary>
        /// stores the information from invoices here 
        /// </summary>
        ObservableCollection<InvoiceInfo> Invoices;
        /// <summary>
        /// clsDataAccess object
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// stores a copy of the invoice Number
        /// </summary>
        private string InvoiceNumber;
        /// <summary>
        /// stores a copy of the Invoice Date
        /// </summary>
        private string InvoiceDate;
        /// <summary>
        /// stores a copy of the Total Cost
        /// </summary>
        private string TotalCost;
        public clsSearchLogic()
        {
            searchSQL = new clsSearchSQL();
        }
        #region Properties
        public string getNumber
        {
            get
            {
                return InvoiceNumber;
            }
            set
            {
                InvoiceNumber = value;
            }
        }

        public string getDate
        {
            get
            {
                return InvoiceDate;
            }
            set
            {
                InvoiceDate = value;
            }
        }

        public string getCosts
        {
            get
            {
                return TotalCost;
            }
            set
            {
                TotalCost = value;
            }
        }
        public ObservableCollection<InvoiceInfo> InvoiceNumbers
        {
            get
            {
                return invoiceNums;
            }
        }

        public ObservableCollection<InvoiceInfo> InvoiceTotalCosts
        {
            get
            {
                return invoiceCosts;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// gets checks to see if InvoiceNumber is not empty
        /// if it is it runs the search
        /// </summary>
        public bool SearchInvoiceNum()
        {
            if (InvoiceNumber != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks to see if the TotalCost is not empty
        ///
        /// </summary>
        /// <returns></returns>
        public bool SearchTotalCosts()
        {
            if (TotalCost != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks to see if the invoiceDate is not the default or empty
        /// </summary>
        /// <returns></returns>
        public bool SearchDate()
        {
            if (InvoiceDate != null)//temp for the default date
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// reset the variables for a new search 
        /// </summary>
        public void resetSearch()
        {
            InvoiceNumber = null;
            InvoiceDate = null;//set to a default date // temp for now 
            TotalCost = null;
        }

        /// <summary>
        /// runs the method to get the invoice numbers from the database
        /// </summary>
        /// <returns></returns>
        public void getInvoiceNums()
        {
            invoiceNums = new ObservableCollection<InvoiceInfo>();
            DataSet ds;

            int res = 0;


            ds = db.ExecuteSQLStatement(searchSQL.getInvoiceNums(), ref res);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                invoiceNums.Add(new InvoiceInfo
                {
                    InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNum"].ToString()
                });
            }

        }

        /// <summary>
        /// returns the total costs
        /// </summary>
        /// <returns></returns>
        public void getTotalCosts()
        {

            DataSet ds;

            int res = 0;

            ds = db.ExecuteSQLStatement(searchSQL.getTotalCosts(), ref res);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                invoiceCosts.Add(new InvoiceInfo
                {
                    TotalCosts = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                });
            }

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


            ds = db.ExecuteSQLStatement(searchSQL.getInvoices(), ref res);

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
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchInvoiceNumbers()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;

            ds = db.ExecuteSQLStatement(searchSQL.SearchInvoiceNums(InvoiceNumber), ref res);
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
        /// takes the selected date from the user and returns observable collection from database 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchInvoiceDates()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;

            ds = db.ExecuteSQLStatement(searchSQL.SearchInvoiceDates(InvoiceDate), ref res);
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
        public ObservableCollection<InvoiceInfo> SearchTotalCost()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;

            ds = db.ExecuteSQLStatement(searchSQL.SearchtotalCosts(TotalCost), ref res);
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
        /// does the search if the date and the invoice number are selected
        /// </summary>
        /// <param name="num">selected invoice number</param>
        /// <param name="date">selected invoice date</param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchNumber_Date()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;

            ds = db.ExecuteSQLStatement(searchSQL.SearchInvoiceNum_Date(InvoiceNumber, InvoiceDate), ref res);
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
        /// does the search if the invoice number and total cost are selected
        /// </summary>  
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchNumber_Cost()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;
            //    string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND TotalCost = " + cost;
            ds = db.ExecuteSQLStatement(searchSQL.SearchInvoiceNum_Cost(InvoiceNumber, TotalCost), ref res);
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
        /// does the search if the date and totalcost are selected
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchDate_Cost()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;
            ds = db.ExecuteSQLStatement(searchSQL.SearchInvoiceDate_Cost(InvoiceDate, TotalCost), ref res);

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
        /// does the search if there is more than all invoice number, invoice date, and total cost are selected
        /// </summary>
        /// <param name="num"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchAll()
        {
            Invoices = new ObservableCollection<InvoiceInfo>();

            DataSet ds;
            int res = 0;

            ds = db.ExecuteSQLStatement(searchSQL.Search_All(InvoiceNumber, InvoiceDate, TotalCost), ref res);
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

        #endregion
    }
}
