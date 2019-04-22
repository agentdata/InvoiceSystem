using InvoiceSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;

namespace InvoiceSystem.Items
{
    class clsItemsSQL
    {
        /// <summary>
        /// Instance of the data Access Class.
        /// </summary>
        clsDataAccess Data;
        
        /// <summary>
        /// clsItemsSQL Constructor.  Instanitiates data access class.
        /// </summary>
        public clsItemsSQL ()
        {
            Data = new clsDataAccess();
        }

        #region Queries
        /// <summary>
        /// Retrieves all items in the database and returns an Item List.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllItems()
        {
            try
            {
                List<Item> output = new List<Item>();

                string sSQL = "SELECT * FROM ItemDesc";
                int iRetVal = 0;

                DataSet ds = Data.ExecuteSQLStatement(sSQL, ref iRetVal);

                foreach  (DataRow dr in ds.Tables[0].Rows)
                {
                    output.Add(new Item(dr[0].ToString(), dr[1].ToString(), dr[2].ToString()));
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public int ItemOnInvoice(string itemcode)
        {
            //return count
            string sSQL = "SELECT * FROM LineItems WHERE ItemCode = '" + itemcode + "'";
            int iRetVal = 0;

            Data.ExecuteSQLStatement(sSQL, ref iRetVal);

            return iRetVal;
        }
        #endregion

        #region NonQueries
        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        public void AddNewItem(string itemcode, string itemdesc, string cost)
        {
            try
            {
                string sSQL = "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('" + itemcode + "', '" +
                itemdesc + "', '" + cost + "')";

                Data.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the corresponding Item in the Database.  Sets the Cost and Item Description
        /// based on a matching Item code.
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        public void UpdateItem(string itemcode, string itemdesc, string cost)
        {
            try
            {
                string sSQL = "Update ItemDesc Set ItemDesc = '" + itemdesc + "', Cost = '" + cost + "' WHERE ItemCode = '" + itemcode + "'";

                Data.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Deletes the corresponding Item in the database based on Item Code.
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        public void DeleteItem(string itemcode, string itemdesc, string cost)
        {
            try
            {
                string sSQL = "DELETE FROM ItemDesc WHERE ItemCode = '" + itemcode + "'"; 

                Data.ExecuteNonQuery(sSQL);

                clsItemsLogic.ItemsList.ResetBindings();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates Total cost of invoices
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="itemdesc"></param>
        /// <param name="cost"></param>
        public void UpdateInvoice(string itemcode)
        {
            try
            {
                List<string> invoice = new List<string>();
                List<string> quantityList = new List<string>();
                List<string> costlist = new List<string>();
                int prod = 0;

                List<string> quantityList2 = new List<string>();
                List<string> costlist2 = new List<string>();
                int prod2 = 0;

                string sSQL = "SELECT l.invoicenum, l.quantity, i.cost from LineItems l INNER JOIN itemdesc i on l.Itemcode = i.itemcode WHERE l.itemcode = '" + itemcode + "'";
                int iRetVal = 0;

                DataSet ds = Data.ExecuteSQLStatement(sSQL, ref iRetVal);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.Add(dr[0].ToString());
                    quantityList.Add(dr[1].ToString());
                    costlist.Add(dr[2].ToString());
                }

                for (int i = 0; i < iRetVal; i++)
                {
                    sSQL = "Select SUM(Cost) AS Price, l.quantity FROM LineItems l INNER JOIN ItemDesc i on i.Itemcode = l.itemcode where i.itemcode <> '" + itemcode + "' AND InvoiceNum = " + invoice[i] + " GROUP BY l.Quantity";
                    int iRetVal2 = 0;

                    DataSet ds2 = Data.ExecuteSQLStatement(sSQL, ref iRetVal2);

                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        quantityList2.Add(dr[0].ToString());
                        costlist2.Add(dr[1].ToString());
                    }

                    Int32.TryParse(costlist2[i], out int cost2);
                    Int32.TryParse(quantityList2[i], out int quantity2);
                    prod2 = quantity2 * cost2;


                    Int32.TryParse(costlist[i], out int cost);
                    Int32.TryParse(quantityList[i], out int quantity);
                    prod = quantity * cost;
                    prod += prod2;

                    sSQL = "Update Invoices Set TotalCost = '" + prod.ToString() + "' WHERE InvoiceNum = " + invoice[i]; // + "'";
                    Data.ExecuteNonQuery(sSQL);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }

   

}
