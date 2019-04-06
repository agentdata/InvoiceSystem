using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Search
{
    class clsSearchLogic
    {
       
            //contains the business logic
        private string InvoiceNumber;
        private string InvoiceDate;
        private string TotalCost;

        #region Properties
        public string getNumber
        {
            get
            {
                return InvoiceNumber;
            }
            set
            {
                InvoiceNumber = value;
            }
        }

        public string getDate
        {
            get
            {
                return InvoiceDate;
            }
            set
            {
                InvoiceDate = value;
            }
        }

        public string getCosts
        {
            get
            {
                return TotalCost;
            }
            set
            {
                TotalCost = value;
            }
        }

        #endregion

        /// <summary>
        /// gets checks to see if InvoiceNumber is not empty
        /// if it is it runs the search
        /// </summary>
        public bool SearchInvoiceNum()
        {
            if (InvoiceNumber != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks to see if the TotalCost is not empty
        ///
        /// </summary>
        /// <returns></returns>
        public bool SearchTotalCosts()
        {
            if (TotalCost != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks to see if the invoiceDate is not the default or empty
        /// </summary>
        /// <returns></returns>
        public bool SearchDate()
        {
            if (InvoiceDate != null)//temp for the default date
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// reset the variables for a new search 
        /// </summary>
        public void resetSearch()
        {
            InvoiceNumber = null;
            InvoiceDate = null;//set to a default date // temp for now 
            TotalCost = null;
        }
    }
}
