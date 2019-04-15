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
        //contains the SQL Statements

        /// <summary>
        /// sql statment to get the invoice numbers
        /// </summary>
        public string getInvoiceNums()
        {

            string sql = "SELECT InvoiceNum FROM Invoices";
            return sql;
        }


        /// <summary>
        /// this sql statment gets the total costs
        /// </summary>
        public string getTotalCosts()
        {
            string sql = "SELECT distinct(TotalCost) FROM Invoices ORDER BY TotalCost ASC";
            return sql;
        }



        /// <summary>
        /// sql statement to get all information for the invoices
        /// </summary>
        public string getInvoices()
        {
            string sql = "Select * FROM Invoices";
            return sql;
        }




        /// <summary>
        /// sql statment to search for the inoice numbers 
        /// </summary>
        /// <param name="num">the selected invoice number</param>
        public string SearchInvoiceNums(string num)
        {
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num;
            return SQLStatment;
        }



        /// <summary>
        /// this search sql statment uses the total costs chosen by the user
        /// </summary>
        /// <param name="costs"> the total costs </param>
        public string SearchtotalCosts(string costs)
        {
            string sql = "SELECT * FROM Invoices WHERE TotalCost = " + costs;
            return sql;
        }



        /// <summary>
        /// this sql statement takes the date selected from the user to do the search
        /// </summary>
        /// <param name="date">the invoice date</param>
        public string SearchInvoiceDates(string date)
        {
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "#";
            return SQLStatment;
        }



        /// <summary>
        /// this sql statment does the search with the invoice number and the invoice date 
        /// </summary>
        /// <param name="num">the invoice number</param>
        /// <param name="date">the invoice date</param>
        public string SearchInvoiceNum_Date(string num, string date)
        {
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND InvoiceDate = #" + date + "#";
            return SQLStatment;
        }



        /// <summary>
        /// this sql statement does the search with the invoice number and the total cost
        /// </summary>
        /// <param name="num">the invoice number</param>
        /// <param name="cost">the invoice cost</param>
        public string SearchInvoiceNum_Cost(string num, string cost)
        {
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND TotalCost = " + cost;
            return SQLStatment;
        }



        /// <summary>
        /// this sql statement does the search with the invoice date and the total cost
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <param name="cost">invoice cost</param>
        public string SearchInvoiceDate_Cost(string date, string cost)
        {
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "# " + "AND TotalCost = " + cost;
            return SQLStatment;
        }

        /// <summary>
        /// this sql statment does the search with the invoice number, the invoice date, and the total cost 
        /// </summary>
        /// <param name="num">the invoice number</param>
        /// <param name="date">the invoice date</param>
        /// <param name="cost">the total cost</param>
        public string Search_All(string num, string date, string cost)
        {
            string SQLStatment = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND InvoiceDate = #"
                + date + "# " + "AND TotalCost = " + cost;
            return SQLStatment;
        }

    }
}
