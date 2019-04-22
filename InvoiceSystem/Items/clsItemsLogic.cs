using InvoiceSystem.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Items
{
    class clsItemsLogic
    {
        #region Variables
        /// <summary>
        /// Instance of the SQL Query class for Items.
        /// </summary>
        public static clsItemsSQL SQL = new clsItemsSQL();
        /// <summary>
        /// Stores a list of Items returns from the database.
        /// </summary>
        public static BindingList<Item> ItemsList { get; set; }
        /// <summary>
        /// Stores an Item object.
        /// </summary>
        public static Item item { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Business Logic Constructor.  Instantiates the SQL Query class.
        /// </summary>
        public clsItemsLogic ()
        {
            //ItemsList = new BindingList<Item>();
        }
                
        /// <summary>
        /// Call the SQL query to get all items from the database and creates
        /// a bindingList of Items.
        /// </summary>
        public static void GetItems()
        {
            try
            {
                ItemsList = new BindingList<Item>(SQL.GetAllItems());

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }
        
        /// <summary>
        /// Call the SQL Add Item to add an item to the ItemDesc table.  Also adds an 
        /// Item to ItemsList.
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        public static void AddItem(string itemcode, string itemdesc, string cost)
        {
            try
            {
                SQL.AddNewItem(itemcode, itemdesc, cost);
                ItemsList.Add(new Item(itemcode, itemdesc, cost));

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Calls the SQL Update Item to update a matching Item in the database.  Also updates 
        /// an item at the given index in ItemsList.
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        /// <param name="index"></param>
        public static void UpdateItem(string itemcode, string itemdesc, string cost, int index)
        {
            try
            {
                SQL.UpdateItem(itemcode, itemdesc, cost);
                item = new Item(itemcode, itemdesc, cost);

                ItemsList[index] = item;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Call the SQL Delete Item to remove a corresponding record from the database and removes
        /// the item at the given index in ItemsList.
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        /// <param name="index"></param>
        public static void DeleteItem(string itemcode, string itemdesc, string cost, int index)
        {
            try
            {
                SQL.DeleteItem(itemcode, itemdesc, cost);

                ItemsList.RemoveAt(index);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Determines if the given Item is used on an invoice.  Returns true or false
        /// based on result.
        /// </summary>
        /// <param name="itemcode"></param>
        public static bool ItemIsUsed(string itemcode)
        {
            if (SQL.ItemOnInvoice(itemcode) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Hands the affected item code to update affected invoice costs.
        /// </summary>
        /// <param name="itemcode"></param>
        public static void InvoiceCostUpdate(string itemcode)
        {
            SQL.UpdateInvoice(itemcode);
        }
        #endregion

    }

    
}
