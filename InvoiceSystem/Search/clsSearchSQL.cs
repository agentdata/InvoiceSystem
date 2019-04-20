using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using InvoiceSystem.Search;
using System.Windows;


namespace InvoiceSystem.Search
{
    class clsSearchSQL
    {
        //contains the SQL Statements

        /// <summary>
        /// sql statment to get the invoice numbers
        /// </summary>
        public string getInvoiceNums()
        {
            try
            {
                string sql = "SELECT InvoiceNum FROM Invoices";
                return sql;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// this sql statment gets the total costs
        /// </summary>
        public string getTotalCosts()
        {
            try
            {
                string sql = "SELECT distinct(TotalCost) FROM Invoices ORDER BY TotalCost ASC";
                return sql;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// sql statement to get all information for the invoices
        /// </summary>
        public string getInvoices()
        {
            try
            {
                string sql = "Select * FROM Invoices";
                return sql;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// sql statment to search for the inoice numbers 
        /// </summary>
        /// <param name="num">the selected invoice number</param>
        public string SearchInvoiceNums(string num)
        {
            try
            {
                string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num;
                return SQLStatment;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// this search sql statment uses the total costs chosen by the user
        /// </summary>
        /// <param name="costs"> the total costs </param>
        public string SearchtotalCosts(string costs)
        {
            try
            {
                string sql = "SELECT * FROM Invoices WHERE TotalCost = " + costs;
                return sql;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// this sql statement takes the date selected from the user to do the search
        /// </summary>
        /// <param name="date">the invoice date</param>
        public string SearchInvoiceDates(string date)
        {
            try
            {
                string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "#";
                return SQLStatment;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// this sql statment does the search with the invoice number and the invoice date 
        /// </summary>
        /// <param name="num">the invoice number</param>
        /// <param name="date">the invoice date</param>
        public string SearchInvoiceNum_Date(string num, string date)
        {
            try
            {
                string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND InvoiceDate = #" + date + "#";
                return SQLStatment;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// this sql statement does the search with the invoice number and the total cost
        /// </summary>
        /// <param name="num">the invoice number</param>
        /// <param name="cost">the invoice cost</param>
        public string SearchInvoiceNum_Cost(string num, string cost)
        {
            try
            {
                string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND TotalCost = " + cost;
                return SQLStatment;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// this sql statement does the search with the invoice date and the total cost
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <param name="cost">invoice cost</param>
        public string SearchInvoiceDate_Cost(string date, string cost)
        {
            try
            {
                string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "# " + "AND TotalCost = " + cost;
                return SQLStatment;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// this sql statment does the search with the invoice number, the invoice date, and the total cost 
        /// </summary>
        /// <param name="num">the invoice number</param>
        /// <param name="date">the invoice date</param>
        /// <param name="cost">the total cost</param>
        public string Search_All(string num, string date, string cost)
        {
            try
            {
                string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND InvoiceDate = #"
                    + date + "# " + "AND TotalCost = " + cost;
                return SQLStatment;
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

    }
}
