using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Expense
    {
        public DateTime ExDate { get; set; }
        public string ExCategory { get; set; }
        public string ExDescription { get; set; }
        public decimal ExAmount { get; set; }

        public Expense(DateTime date, string category, string description, decimal amount)
        {
            ExDate = date;
            ExCategory = category;
            ExDescription = description;
            ExAmount = amount;
        }
        public override string ToString()
        {
            return $"{ExDate.ToShortDateString()} | {ExCategory} | {ExDescription} | ${ExAmount:F2}";
        }
    }
}
