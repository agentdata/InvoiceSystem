﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.OtherClasses
{
    public class Invoice
    {
        public string InvoiceNum { get; set; }
        public string InvoiceDate { get; set;}
        public string TotalCost { get; set;}
        
        public Invoice(string InvoiceNum, string InvoiceDate, string TotalCost)
        {
            this.InvoiceNum = InvoiceNum;
            this.InvoiceDate = InvoiceDate;
            this.TotalCost = TotalCost;

        }
    }
}
