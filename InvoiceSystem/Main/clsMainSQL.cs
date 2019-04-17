using System;
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
    public static class clsMainSQL
    {
        #region Query
        public static DataSet getAllInvoices(ref int Invoices_Rows)
        {
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from Invoices", ref Invoices_Rows);
        }
        public static DataSet getLineItems(ref int LineItem_Rows)
        {
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from LineItems", ref LineItem_Rows);
        }
        public static DataSet getItems(ref int Item_Rows)
        {
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from ItemDesc", ref Item_Rows);
        }

        /// <summary>
        /// Query to get the total items in the database, this
        /// </summary>
        /// <returns> an int representing the number of total items in the database</returns>
        internal static void totalNumItems(ref int ItemDescRows)
        {
            (new clsDataAccess()).ExecuteSQLStatement("Select * from ItemDesc", ref ItemDescRows);
        }


        public static DataSet GetAllItems(ref int Item_Rows)
        {
            // Get Item Descriptions dataset
            return (new clsDataAccess()).ExecuteSQLStatement("Select * from ItemDesc", ref Item_Rows);
        }
        #endregion Query

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
            (new clsDataAccess()).ExecuteNonQuery("UPDATE LineItems SET Quantity=" + quantity + " WHERE InvoiceNum=" + InvoiceNum + " AND ItemCode='" + ItemCode + "'");
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
        public static void DeleteLineItem(ref string InvoiceNum, ref string ItemCode)
        {
            (new clsDataAccess()).ExecuteNonQuery("DELETE FROM LineItems WHERE InvoiceNum=" + InvoiceNum + " AND ItemCode='" + ItemCode + "'");
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
        public static void InsertInvoice(string invoiceNum, int totalCost)
        {
            //delete line item
            //string sqlCommand = " INSERT INTO Invoices (InvoiceNum, InvoiceDate, ItemCode, Quantity) " +
            //                                    "VALUES ('" + InvoiceNum + "', '" + LineItemNum + "', '" + lineItem.Item.ItemCode + "', '" + Quantity + "')";


            //new clsDataAccess().ExecuteNonQuery(sqlCommand);
        }

        /// <summary>
        /// Add a new lineitem with the specified invoicenum, itemcode and quantity.
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="lineItem"></param>
        /// <param name="LineItemNum"></param>
        /// <param name="Quantity"></param>
        internal static void addNewLineItemSQL(string InvoiceNum, LineItem lineItem, string LineItemNum, int Quantity)
        {
            //delete line item
            string sqlCommand = " INSERT INTO LineItems (InvoiceNum,LineItemNum, ItemCode, Quantity) " +
                                                "VALUES ('" + InvoiceNum + "', '" + LineItemNum + "', '" + lineItem.Item.ItemCode + "', '" + Quantity + "')";


            new clsDataAccess().ExecuteNonQuery(sqlCommand);
        }
        #endregion Insert Commands
        #endregion NON QUERY
    }
}