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
        #endregion
    }

   

}
