using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Items
{
    class clsItemsLogic
    {
        clsItemsSQL SQL;

        public clsItemsLogic ()
        {
            SQL = new clsItemsSQL();
        }

        //Dataset for grid


        //get all items
        public void GetItems()
        {
            SQL.GetAllItems();
        }
        
        //add item
        public void AddItem(string itemcode, string itemdesc, string cost)
        {
            SQL.AddNewItem(itemcode, itemdesc, cost);
        }


        //save item
        public void UpdateItem(string itemcode, string itemdesc, string cost)
        {
            SQL.UpdateItem(itemcode, itemdesc, cost);
        }

        //delete item
        public void DeleteItem(string itemcode, string itemdesc, string cost)
        {
            SQL.DeleteItem(itemcode, itemdesc, cost);
        }
    }

    
}
