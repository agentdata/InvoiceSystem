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
        public static InvoiceList getAllInvoices(InvoiceList Invoices)
        {   
            string strDSN = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + Directory.GetCurrentDirectory() + "\\InvoiceDB.accdb";
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
            //get Flights dataset
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strDSN))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand("Select * from Invoices", conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(InvoicesDS);
                    }
                }

                //Set the number of values returned
                Invoices_Rows = InvoicesDS.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            // do stuff with datasets

            //add flights into flightlist
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
            //get Flights dataset
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strDSN))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand("Select * from ItemDesc", conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ItemsDS);
                    }
                }

                //Set the number of values returned
                Item_Rows = ItemsDS.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

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
            //get Flights dataset
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strDSN))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand("Select * from LineItems", conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ItemLinkDS);
                    }
                }

                //Set the number of values returned
                LineItem_Rows = ItemLinkDS.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

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
            string strDSN = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + Directory.GetCurrentDirectory() + "\\InvoiceDB.accdb";
            int ItemDescRows = 0;

            //Create a new DataSet for flights
            DataSet itemDescRowsDS = new DataSet();
            #region Get Item Rows

            //get Flights dataset
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strDSN))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand("Select * from ItemDesc", conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(itemDescRowsDS);
                    }
                }

                //Set the number of values returned
                ItemDescRows = itemDescRowsDS.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            #endregion

            return ItemDescRows;
        }
    }
}