using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Items
{
    class clsItemsSQL
    {

        //get all items in dataset


        public void GetAllItems()
        {
            string sSQL = "SELECT * FROM ItemDesc";
        }

        //add item
        public void AddNewItem(string itemcode, string itemdesc, string cost)
        {
            string sSQL = "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES (" + itemcode + ", " + 
                itemdesc + ", " + cost + ")";
        }

        //save item

        public void UpdateItem(string itemcode, string itemdesc, string cost)
        {
            string sSQL = "Update ItemDesc Set ItemCode = " + itemcode + ", ItemDesc = " + itemdesc + ", Cost = " + cost;
        }

        //delete item
        public void DeleteItem(string itemcode, string itemdesc, string cost)
        {
            string sSQL = "DELTE FROM ItemDesc WHERE ItemCode = " + itemcode + " AND ItemDesc = " + itemdesc + " AND Cost = " + cost;
        }
    }

   

}
