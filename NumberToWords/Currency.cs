using System;
using System.Collections.Generic;
using System.Text;

/*
 Author : Theodorus Danang Wicaksono
 */
namespace NumberToWords
{
    class Currency
    {
        public decimal NumberCurrency { get; set; }
        public decimal WholeNumber { get; set; }
        public int PointNumber { get; set; }
        public string WordCurrency { get; set; }
    }
}
