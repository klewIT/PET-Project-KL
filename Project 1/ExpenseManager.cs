using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Project_1
{
    public class ExpenseManager
    {
        private List<Expense> expenses = new List<Expense>();

        public void AddExpense(Expense expense)
        { 
            expenses.Add(expense);
        }
        public void UpdateExpense(int index,Expense updExpense)
        {
            if(index >= 0 && index < expenses.Count)
            {
                expenses[index] = updExpense;
            }
            else
            {
                throw new IndexOutOfRangeException("Invaild expense index");
            }
        }
        public void DeleteExpense(int index)
        {
            if(index>=0 && index <expenses.Count)
            {
                expenses.RemoveAt(index);
            }
            else
            {
                throw new IndexOutOfRangeException("Invaild expense index");
            }
        }
        public void SaveExpenses(string filepath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(expenses, Formatting.Indented);
                File.WriteAllText(filepath, json);
                Console.WriteLine("Expenses saved successfully");
            }
            catch (Exception ex) 
            {  Console.WriteLine($"Error saving expenses: {ex.Message}"); }
           
        }
        public void LoadExpenses(string filepath)
        {
            try 
            {
                if (File.Exists(filepath))
                {
                    string json = File.ReadAllText(filepath);
                    expenses = JsonConvert.DeserializeObject<List<Expense>>(json);
                    Console.WriteLine("Expenses loaded successfully");
                }
                else
                {
                    Console.WriteLine("File not found"); 
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error loading expenses: {ex.Message}");
            }
        }
        public List<Expense> GetExpenses()
        {
            return expenses;
        }
        public List<Expense> GetExpensesC(string category)
        {
            return expenses.Where(e => e.ExCategory == category).ToList();
        }
        public List<Expense> GetExpensesDR(DateTime startDate, DateTime endDate) 
        { 
            return expenses.Where(e => e.ExDate >= startDate && e.ExDate <= endDate).ToList();
        } 
        public decimal GetTotalEDR(DateTime startDate, DateTime endDate)
        {
            return expenses
                .Where(e => e.ExDate >= startDate && e.ExDate <= endDate)
                .Sum(e => e.ExAmount);
        }
    }
}
