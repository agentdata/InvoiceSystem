﻿using System;
using InvoiceSystem.OtherClasses;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using InvoiceSystem.Classes;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// This class is meant to be the place where all SQL statements for the windows/views in the Main folder WndMain
    /// </summary>
    public static class clsMainSQL
    {
        #region QUERY
        /// <summary>
        /// Returns a dataset with all the invoices.
        /// </summary>
        /// <param name="Invoices_Rows"></param>
        /// <returns></returns>
        public static DataSet getAllInvoices(ref int Invoices_Rows)
        {
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from Invoices", ref Invoices_Rows);
        }

        /// <summary>
        /// Returns a dataset with all the lineItems.
        /// </summary>
        /// <param name="LineItem_Rows"></param>
        /// <returns></returns>
        public static DataSet getLineItems(ref int LineItem_Rows)
        {
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from LineItems", ref LineItem_Rows);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="rows"></param>
        internal static void getNumLineItems(string invoiceNum, ref int rows)
        {
            new clsDataAccess().ExecuteSQLStatement("Select * from LineItems WHERE InvoiceNum ="+invoiceNum, ref rows);
        }

        /// <summary>
        /// Returns a dataset with all the items.
        /// </summary>
        /// <param name="Item_Rows"></param>
        /// <returns></returns>
        public static DataSet getItems(ref int Item_Rows)
        {
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from ItemDesc", ref Item_Rows);
        }

        /// <summary>
        /// Returns the total number of items in the database
        /// </summary>
        /// <returns> an int representing the number of total items in the database</returns>
        internal static void totalNumItems(ref int ItemDescRows)
        {
            //this could be more efficient by not querying all, but using some system property
            (new clsDataAccess()).ExecuteSQLStatement("Select * from ItemDesc", ref ItemDescRows);
        }
        #endregion QUERY

        #region NON QUERY
        #region Update Commands
        /// <summary>
        /// This function updates
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="ItemCode"></param>
        /// <param name="quantity"></param>
        public static void UpdateLineItemSQL(string InvoiceNum, string ItemCode, int quantity)
        {
            new clsDataAccess().ExecuteNonQuery("UPDATE LineItems SET Quantity=" + quantity + " WHERE InvoiceNum=" + InvoiceNum + " AND ItemCode='" + ItemCode + "'");
        }

        /// <summary>
        /// This function updates the value for total cost on the specified invoice number in the program database
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="TotalCost"></param>
        public static void updateInvoiceTotalCost(string InvoiceNum, string TotalCost)
        {
            new clsDataAccess().ExecuteNonQuery("UPDATE Invoices SET TotalCost =" + TotalCost + " WHERE InvoiceNum=" + InvoiceNum);
        }
        #endregion Update Commands
        #region Delete Commands
        /// <summary>
        /// This should be executed when the update amount is zero so that it removes it from the lineitems 
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="ItemCode"></param>
        public static void DeleteLineItem(string InvoiceNum, string ItemCode)
        {
            new clsDataAccess().ExecuteNonQuery("DELETE FROM LineItems WHERE InvoiceNum=" + InvoiceNum + " AND ItemCode='" + ItemCode + "'");
        }

        internal static string getLastInvoiceAutoGeneratedID()
        {
            //SELECT TOP 1 Passenger_ID FROM Passenger ORDER BY Passenger_ID DESC
            return new clsDataAccess().ExecuteScalarSQL("SELECT TOP 1 InvoiceNum FROM Invoices ORDER BY InvoiceNum DESC");
        }


        /// <summary>
        /// This function is called when an invoice is to be deleted, the invoice object to be deleted is passed in
        /// the lineitems are all deleted and then the invoice itself.
        /// </summary>
        /// <param name="InvoiceToDelete"></param>
        public static void deleteInvoice(Invoice InvoiceToDelete)
        {
            //delete LineItems
            new clsDataAccess().ExecuteNonQuery("DELETE FROM LineItems WHERE InvoiceNum=" + InvoiceToDelete.InvoiceNum);

            //delete Invoice
            new clsDataAccess().ExecuteNonQuery("DELETE FROM Invoices WHERE InvoiceNum=" + InvoiceToDelete.InvoiceNum);
        }
        #endregion Delete Commands

        #region Insert Commands
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCost"></param>
        public static void InsertInvoice(string invoiceDate, int totalCost)
        {
            new clsDataAccess().ExecuteNonQuery(" INSERT INTO Invoices (InvoiceDate, TotalCost) " +
                                                "VALUES ('" + invoiceDate + "', '" + totalCost + "')");
        }

        /// <summary>
        /// Add a new lineitem with the specified invoicenum, itemcode and quantity.
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="LineItem"></param>
        /// <param name="LineItemNum"></param>
        /// <param name="Quantity"></param>
        internal static void addNewLineItemSQL(int LineItemNum, string InvoiceNum, LineItem LineItem)
        {
            new clsDataAccess().ExecuteNonQuery("INSERT INTO LineItems (LineItemNum, InvoiceNum, ItemCode, Quantity) " +
                                                "VALUES ('" + LineItemNum + "','" + InvoiceNum + "','" + LineItem.Item.ItemCode + "', '" + LineItem.Quantity + "')");
        }
        #endregion Insert Commands
        #endregion NON QUERY
    }
}