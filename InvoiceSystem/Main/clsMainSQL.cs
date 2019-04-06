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
        /// <summary>
        /// This function is used to get all the invoices in the database.
        /// returns an InvoiceList which is built up by getting invoices, items then linking new LinItem objects to each invoice object.
        /// </summary>
        /// <param name="Invoices"></param>
        /// <returns></returns>
        public static InvoiceList getAllInvoices(InvoiceList Invoices)
        {   
            clsDataAccess dataAccess = new clsDataAccess();
            int Invoices_Rows = 0;
            int Item_Rows = 0;
            int LineItem_Rows = 0;

            //Create a new DataSet for Invoices
            DataSet InvoicesDS = new DataSet();
            //Create a new DataSet for Items
            DataSet ItemsDS= new DataSet();
            //Create a new DataSet for Items
            DataSet ItemLinkDS = new DataSet();

            ObservableCollection<Item> tempItemCollection = new ObservableCollection<Item>();

            #region Get Invoices
            //get Invoices dataset

            InvoicesDS = dataAccess.ExecuteSQLStatement("Select * from Invoices", ref Invoices_Rows);

            // do stuff with datasets

            //add Invoices into invoices list
            for (int i = 0; i < Invoices_Rows; i++)
            {
                Invoice nextinvoice = new Invoice(
                                                InvoicesDS.Tables[0].Rows[i]["InvoiceNum"].ToString(),
                                                InvoicesDS.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                                                InvoicesDS.Tables[0].Rows[i]["TotalCost"].ToString());
                Invoices.addInvoice(nextinvoice);
            }
            #endregion Get Invoices

            #region Get Items

            //get Item Descriptions dataset
            ItemsDS = dataAccess.ExecuteSQLStatement("Select * from ItemDesc", ref Item_Rows);

            // do stuff with datasets
            //add items to temporary collection
            for (int i = 0; i < Item_Rows; i++)
            {
                Item Item = new Item(
                                                ItemsDS.Tables[0].Rows[i]["ItemCode"].ToString(),
                                                ItemsDS.Tables[0].Rows[i]["ItemDesc"].ToString(),
                                                ItemsDS.Tables[0].Rows[i]["Cost"].ToString());
                tempItemCollection.Add(Item);
            }
            #endregion Get Items

            #region Link Items To Invoices
            //get LineItems dataset

            ItemLinkDS = dataAccess.ExecuteSQLStatement("Select * from LineItems", ref LineItem_Rows);

            // do stuff with datasets
            // Load invoices with correct number of items for the order
            for (int i = 0; i < LineItem_Rows; i++)
            {
                var invoiceNum = ItemLinkDS.Tables[0].Rows[i]["InvoiceNum"].ToString();
                var ItemCode = ItemLinkDS.Tables[0].Rows[i]["ItemCode"].ToString();
                var LineItemNumber = ItemLinkDS.Tables[0].Rows[i]["LineItemNum"].ToString();
                var Quantity = ItemLinkDS.Tables[0].Rows[i]["Quantity"].ToString();

                //get item
                Item itemToAdd = tempItemCollection.Where(x => x.ItemCode == ItemCode).FirstOrDefault();
                
                //find the invoice from invoices and add the item to it
                Invoices.InvoicesCollection.Where(x => x.InvoiceNum == invoiceNum).FirstOrDefault().addItem(itemToAdd, Int32.Parse(Quantity));
            }
            #endregion Link Items To Invoices

            return Invoices;
        }

        /// <summary>
        /// Query to get the total items in the database
        /// </summary>
        /// <returns> an int representing the number of total items in the database</returns>
        internal static int totalItems()
        {
            int ItemDescRows = 0;

            //Create a new DataSet for all Items
            DataSet itemDescRowsDS = new DataSet();
            //Set DS as the return value from 
            itemDescRowsDS =  new clsDataAccess().ExecuteSQLStatement("Select * from ItemDesc", ref ItemDescRows);
            return ItemDescRows;
        }

        /// <summary>
        /// This function updates
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="ItemCode"></param>
        /// <param name="quantity"></param>
        public static void UpdateLineItemSQL(string InvoiceNum, string ItemCode, int quantity)
        {
            string sqlCommand;
            if (quantity == 0)
            {
                //delete line item
                sqlCommand = "DELETE FROM LineItems WHERE InvoiceNum=" + InvoiceNum + " AND ItemCode='" + ItemCode+"'";
            }
            else
            {
                //update quantity
                sqlCommand = "UPDATE LineItems SET Quantity=" + quantity + " WHERE InvoiceNum=" + InvoiceNum + " AND ItemCode='" + ItemCode + "'";
            }
            new clsDataAccess().ExecuteNonQuery(sqlCommand);
        }

        public static ObservableCollection<Item> GetAllItems()
        {
            //Data
            clsDataAccess dataAccess = new clsDataAccess();

            // Create a new DataSet for Items
            DataSet ItemsDS = new DataSet();

            //Int for total rows
            int Item_Rows = 0;

            // Collection to load item objects into
            ObservableCollection<Item> ItemCollection = new ObservableCollection<Item>();

            #region Get Items

            // Get Item Descriptions dataset
            ItemsDS = dataAccess.ExecuteSQLStatement("Select * from ItemDesc", ref Item_Rows);

            // Do stuff with datasets
            // Add items to temporary collection
            for (int i = 0; i < Item_Rows; i++)
            {
                Item Item = new Item(
                                                ItemsDS.Tables[0].Rows[i]["ItemCode"].ToString(),
                                                ItemsDS.Tables[0].Rows[i]["ItemDesc"].ToString(),
                                                ItemsDS.Tables[0].Rows[i]["Cost"].ToString());
                ItemCollection.Add(Item);
            }
            #endregion Get Items

            return ItemCollection;
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

        /// <summary>
        /// This function updates the value for total cost on the specified invoice number in the program database
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="TotalCost"></param>
        public static void updateInvoiceTotalCost(string InvoiceNum, string TotalCost)
        {
            new clsDataAccess().ExecuteNonQuery("UPDATE Invoices SET TotalCost ="+TotalCost+" WHERE InvoiceNum=" + InvoiceNum);
        }

        #region Insert Commands
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
    }
}