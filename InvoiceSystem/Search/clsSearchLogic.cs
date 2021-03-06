﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows;

namespace InvoiceSystem.Search
{
    class clsSearchLogic
    {
        #region Attributes
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
        clsDataAccess db;
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
        /// <summary>
        /// this stores the invoice number from selected row in the datagrid
        /// </summary>
        private string selectedNumber;
        /// <summary>
        /// this stores the invoice Date from the selected row in the datagrid 
        /// </summary>
        private string selectedDate;
        /// <summary>
        /// this stores the total cost from the selected row in the datagrid 
        /// </summary>
        private string selectedCost;


        public clsSearchLogic()
        {
            try
            {
                searchSQL = new clsSearchSQL();
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// this property gets the invoice number 
        /// </summary>
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

        /// <summary>
        /// this property sets the invoice date
        /// </summary>
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

        /// <summary>
        /// this property sets the invoice total costs
        /// </summary>
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
        /// <summary>
        /// this property allows the InvoiceNumbers Oberservable collection be be used throughout the program 
        /// </summary>
        public ObservableCollection<InvoiceInfo> InvoiceNumbers
        {
            get
            {
                return invoiceNums;
            }
        }

        /// <summary>
        /// this property allows the InvoiceTotalCosts Oberservable collection to be used throughout the program
        /// </summary>
        public ObservableCollection<InvoiceInfo> InvoiceTotalCosts
        {
            get
            {
                return invoiceCosts;
            }
        }

        /// <summary>
        /// this property sets the selectedNumber 
        /// </summary>
        public string SelectedInvoiceNum
        {
            get
            {
                return selectedNumber; 
            }
            set
            {
                selectedNumber = value;
            }
        }

        /// <summary>
        /// this property sets the selectedDate
        /// </summary>
        public string SelectedInvoiceDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
            }  
        }

        /// <summary>
        /// this property sets the selectedCost
        /// </summary>
        public string SelectedTotalCost
        {
            get
            {
                return selectedCost;
            }
            set
            {
                selectedCost = value;
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
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// checks to see if the TotalCost is not empty
        ///
        /// </summary>
        /// <returns></returns>
        public bool SearchTotalCosts()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// checks to see if the invoiceDate is not the default or empty
        /// </summary>
        /// <returns></returns>
        public bool SearchDate()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// reset the variables for a new search 
        /// </summary>
        public void resetSearch()
        {
            try
            {
                InvoiceNumber = null;
                InvoiceDate = null;//set to a default date // temp for now 
                TotalCost = null;
                selectedNumber = "";
                selectedDate = "";
                SelectedTotalCost = "";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// runs the method to get the invoice numbers from the database
        /// </summary>
        /// <returns></returns>
        public void getInvoiceNums()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns the total costs
        /// </summary>
        /// <returns></returns>
        public void getTotalCosts()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// gets the invoices from the Invoices Database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> GetInvoices()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// for the search if only the InvoiceNum ComboBox is used
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchInvoiceNumbers()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// takes the selected date from the user and returns observable collection from database 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchInvoiceDates()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// does a search of the totalcosts chosen by the user and returns any Invoices from the database
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchTotalCost()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// does the search if the date and the invoice number are selected
        /// </summary>
        /// <param name="num">selected invoice number</param>
        /// <param name="date">selected invoice date</param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchNumber_Date()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// does the search if the invoice number and total cost are selected
        /// </summary>  
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchNumber_Cost()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// does the search if the date and totalcost are selected
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchDate_Cost()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// does the search if there is more than all invoice number, invoice date, and total cost are selected
        /// </summary>
        /// <param name="num"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public ObservableCollection<InvoiceInfo> SearchAll()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }

        #endregion
    }
}
