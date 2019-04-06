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
    }

    //Dataset for grid


    //get all items
    SQL.GetAllItems();

    //add item
    SQL.AddNewItem();

    //save item
    SQL.UpdateItem();

    //delete item
    SQL.DeleteItem();
}
