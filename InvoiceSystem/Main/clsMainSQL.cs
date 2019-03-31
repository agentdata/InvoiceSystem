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

namespace InvoiceSystem.Main
{
    public static class clsMainSQL
    {
        public static InvoiceList getAllInvoices(InvoiceList Invoices)
        {   
            string strSQL = "SELECT * FROM Invoices";
            string strDSN = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + Directory.GetCurrentDirectory() + "\\InvoiceDB.accdb";
            int Invoices_Rows = 0;

            //Create a new DataSet for flights
            DataSet InvoicesDS = new DataSet();

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


            return Invoices;
        }
    }
}