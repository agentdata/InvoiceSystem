﻿using InvoiceSystem.Classes;
using InvoiceSystem.OtherClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InvoiceSystem.Main
{
    public static class clsMainLogic
    {
        /// <summary>
        /// This method calculates the new totalcost for an invoice after the lineitems are updated.
        /// </summary>
        /// <param name="Invoice"></param>
        public static void updateInvoiceTotalCost(Invoice Invoice)
        {
            //get new total cost
            int newCost = 0;
            foreach (LineItem LineItem in Invoice.LineItems.lineItems)
            {
                int itemCost;
                if (Int32.TryParse(LineItem.Item.Cost, out itemCost))
                {
                    if (LineItem.Quantity > 1)
                    {
                        itemCost = itemCost * LineItem.Quantity;
                    }
                    newCost += itemCost;
                }
            }
            clsMainSQL.updateInvoiceTotalCost(Invoice.InvoiceNum, newCost.ToString());
        }

        /// <summary>
        /// This just returns the number of items currently in the databse.
        /// </summary>
        /// <returns></returns>
        internal static int totalNumItems()
        {
            int Item_Rows = 0;
            clsMainSQL.totalNumItems(ref Item_Rows);
            return Item_Rows;
        }

        /// <summary>
        /// This function updates
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="ItemCode"></param>
        /// <param name="quantity"></param>
        public static void UpdateLineItemSQL(string InvoiceNum, string ItemCode, int quantity)
        {
            clsMainSQL.UpdateLineItemSQL(InvoiceNum, ItemCode, quantity);
        }

        /// <summary>
        /// This creates a collection of Item objects which can be presented to the user.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Item> GetAllItems()
        {
            // Create a new DataSet for Items
            DataSet ItemsDS = new DataSet();

            //Int for total rows
            int Item_Rows = 0;

            // Collection to load item objects into
            ObservableCollection<Item> ItemCollection = new ObservableCollection<Item>();

            #region Get Items

            // Get Item Descriptions dataset
            ItemsDS = clsMainSQL.getItems(ref Item_Rows);

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
        /// This function gets the lineitems associated with this invoice based on the invoicenum
        /// it then adds all the lineitems to the invoice object by querying the database.
        /// </summary>
        /// <param name="currentInvoice"></param>
        internal static void getLineItems(ref Invoice currentInvoice)
        {
            #region Get Line Items
            DataSet LineItems;
            //get LineItems dataset
            int LineItem_Rows = 0;

            LineItems = clsMainSQL.getLineItemsForThisInvoice(currentInvoice.InvoiceNum, ref LineItem_Rows);

            // do stuff with datasets
            // Load invoices with correct number of items for the order
            for (int i = 0; i < LineItem_Rows; i++)
            {
                var invoiceNum = LineItems.Tables[0].Rows[i]["InvoiceNum"].ToString();
                var ItemCode = LineItems.Tables[0].Rows[i]["ItemCode"].ToString();
                var LineItemNumber = LineItems.Tables[0].Rows[i]["LineItemNum"].ToString();
                var Quantity = LineItems.Tables[0].Rows[i]["Quantity"].ToString();

                //get this line item's item info and cost
                DataSet itemInfo;
                itemInfo = clsMainSQL.getItemInfo(ItemCode);
                var cost = itemInfo.Tables[0].Rows[0]["Cost"].ToString();
                var desc = itemInfo.Tables[0].Rows[0]["ItemDesc"].ToString();

                currentInvoice.addItem(new Item(ItemCode, desc, cost), Int32.Parse(Quantity));
            }
            #endregion Link Items To Invoices
        }

        /// <summary>
        /// This creates a new
        /// </summary>
        /// <param name="NewInvoice"></param>
        internal static void submitNewInvoice(Invoice NewInvoice)
        {
                //create invoice
                clsMainSQL.InsertInvoice(NewInvoice.InvoiceDate, Int32.Parse(NewInvoice.TotalCost));

                //get invoiceNum so that line items can be associated with the invoice.
                string invoiceNum = clsMainSQL.getLastInvoiceAutoGeneratedID();

                //add lineitems to DB corresponding to the invoiceNum
                foreach (LineItem newLineItem in NewInvoice.LineItems)
                {
                    int rows = new int();
                    //getnumber oflineitems so that the lineitemnum can be set for the next lineItem to be added.
                    clsMainSQL.getNumLineItems(invoiceNum ,ref rows);
                    int lineItemNum = rows + 1;
                    clsMainSQL.addNewLineItemSQL(rows + 1, invoiceNum, newLineItem);                    
                }
        }

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
            DataSet ItemsDS = new DataSet();
            //Create a new DataSet for Items
            DataSet ItemLinkDS = new DataSet();

            ObservableCollection<Item> tempItemCollection = new ObservableCollection<Item>();

            #region Get Invoices
            //get Invoices dataset
            InvoicesDS = clsMainSQL.getAllInvoices(ref Invoices_Rows);

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
            ItemsDS = clsMainSQL.getItems(ref Item_Rows);

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

            ItemLinkDS = clsMainSQL.getLineItems(ref LineItem_Rows);

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

        internal static void AddNewLineItemToExistingInvoice(ref Invoice currentInvoice, Item selectedItem, int v)
        {
            int NumLineItemRows = new int();
            clsMainSQL.getLineItems(ref NumLineItemRows);
            clsMainSQL.addNewLineItemSQL(NumLineItemRows, currentInvoice.InvoiceNum,new LineItem(selectedItem,1));
        }

        internal static void addItemToCart(object sender, ref LineItems _shoppingCart, ref int _runningTotal)
        {
            //if(CurrentInvoice.LineItems.Where())
            DataGridRow row = sender as DataGridRow;
            Item selectedItem = (row.Item as Item);
            if (selectedItem != null)
            {
                //ifshoppingcart already contains item with same itemcode, then just add 1
                if (_shoppingCart.containsItem(selectedItem.ItemCode))
                {
                    _shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedItem.ItemCode).FirstOrDefault().Quantity++;
                }

                //otherwise add selectedItem to the running list
                else
                    _shoppingCart.addLineItem(new LineItem(selectedItem, 1));
                _runningTotal += Int32.Parse(selectedItem.Cost);
            }
        }

        internal static bool deleteInvoice(object sender)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var row = (DataGridRow)contextMenu.PlacementTarget;
            MessageBoxResult shouldBeDeleted = MessageBox.Show("Are you sure you want to delete invoice #" + (row.Item as Invoice).InvoiceNum, "Confirm Delete Invoice?", MessageBoxButton.YesNo);

            if (row != null && shouldBeDeleted == MessageBoxResult.Yes)
            {
                clsMainSQL.deleteInvoice(row.Item as Invoice);
                return true;
            }
            return false;
        }

        internal static void updateLineItemQuantity(ref Invoice _CurrentInvoice, ref LineItem selectedLineItem, string increaseOrDecrease)
        {
            if(increaseOrDecrease == "increase")
            {
                selectedLineItem.Quantity++;
                clsMainSQL.UpdateLineItemSQL(_CurrentInvoice.InvoiceNum, selectedLineItem.Item.ItemCode, selectedLineItem.Quantity);
                
            }
            else if (increaseOrDecrease == "decrease")
            {
                //if decrease results in 0 then delete the item
                if (selectedLineItem.Quantity == 1)
                {
                    var _selectedLineItem = selectedLineItem;
                    _CurrentInvoice.LineItems.Remove(_CurrentInvoice.LineItems.lineItems.Where(x => x == _selectedLineItem).FirstOrDefault());
                    clsMainSQL.DeleteLineItem(_CurrentInvoice.InvoiceNum, selectedLineItem.Item.ItemCode);
                }
                else
                {
                    selectedLineItem.Quantity--;
                    clsMainSQL.UpdateLineItemSQL(_CurrentInvoice.InvoiceNum, selectedLineItem.Item.ItemCode, selectedLineItem.Quantity);
                }
            }
            else if(increaseOrDecrease == "delete")
            {
                var _selectedLineItem = selectedLineItem;
                _CurrentInvoice.LineItems.Remove(_CurrentInvoice.LineItems.lineItems.Where(x => x == _selectedLineItem).FirstOrDefault());
                clsMainSQL.DeleteLineItem(_CurrentInvoice.InvoiceNum, selectedLineItem.Item.ItemCode);
            }
        }

        internal static void DeleteLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            clsMainSQL.DeleteLineItem(invoiceNum.ToString(), itemCode);
        }
    }
}