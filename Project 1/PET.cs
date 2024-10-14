using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Net;

namespace Project_1
{
    internal class PET
    {
        static void AddExpense(ExpenseManager m)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Add New Expense ===");

                Console.Write("Enter Date (MM/DD/YYYY): ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                Console.Write("Enter Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter Amount: ");
                decimal amount = Decimal.Parse(Console.ReadLine());

                Expense expense = new Expense(date, category, description, amount);
                m.AddExpense(expense);
                Console.WriteLine("Expense added successfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }
        static void ViewExpenses(ExpenseManager m)
        {
            Console.Clear();
            Console.WriteLine("=== All Expenses ===");

            var expenses = m.GetExpenses();
            if(expenses.Count == 0)
            {
                Console.WriteLine("No expenses recorded");
            }
            else
            {
                foreach (var expense in expenses)
                {
                    Console.WriteLine(expense.ToString());
                }
            }
            Console.ReadLine();
        }
        static void VEC(ExpenseManager m)
        {
            Console.Clear();
            Console.WriteLine("=== Expenses by Category ===");

            Console.WriteLine("Enter Category: ");
            string category = Console.ReadLine();

            var expensesC = m.GetExpensesC(category);
            if (expensesC.Count == 0)
            {
                Console.WriteLine("No expenses recorded under categoey");
            }
            else
            {
                foreach (var expense in expensesC)
                {
                    Console.WriteLine(expense.ToString());

                }
            }
            Console.ReadLine();
        }
        static void VEDR(ExpenseManager m)
        {
            Console.Clear();
            Console.WriteLine("=== Expenses by Date Range ===");

            Console.Write("Enter Start Date for search (MM/DD/YYYY): ");
            DateTime SD = DateTime.Parse(Console.ReadLine().Trim());
            Console.Write("Enter Start Date for search (MM/DD/YYYY): ");
            DateTime ED = DateTime.Parse(Console.ReadLine().Trim());

            var expensesDR = m.GetExpensesDR(SD,ED);
            if (expensesDR.Count == 0)
            {
                Console.WriteLine("No expenses recorded by daterange");
            }
            else
            {
                foreach (var expense in expensesDR)
                {
                    Console.WriteLine(expense.ToString());
                }
            }
            Console.ReadLine();
        }

        static void GSR(ExpenseManager m)
        {
            Console.Clear();
            Console.WriteLine("=== Expense Summary Report ===");

            Console.Write("Enter Start Date (MM/DD/YYYY): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (MM/DD/YYYY): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            decimal total = m.GetTotalEDR(startDate, endDate);
            Console.WriteLine($"Total Expense from{startDate.ToShortDateString()} to {endDate.ToShortDateString()}: ${total:F2}");

            Console.ReadLine() ;
        }
        static void SaveFile(ExpenseManager m)
        {
            Console.WriteLine("Do u wish to save the file (y/n): ");
            char sanswer = char.Parse(Console.ReadLine());
          
            if(sanswer == 'y')
            {
                string filepath = "expenses.json";
                m.SaveExpenses(filepath);
            }
            else
            {
                Console.ReadLine(); 
            }
        }

        static void LoadFile(ExpenseManager m)
        {
            Console.WriteLine("Do u wish to save the file (y/n): ");
            char lanswer = char.Parse(Console.ReadLine());

            if (lanswer == 'y')
            {
                string filepath = "expenses.json";
                m.LoadExpenses(filepath);
            }
            else
            {
                Console.ReadLine(); 
            }
        }

        static void UpdateExpense(ExpenseManager m)
        {
            Console.Clear();
            Console.WriteLine("=== Update Expense ===");
            ViewExpenses(m);

            try
            {
                Console.Write("Enter the expense number to update: ");
                int index = Int32.Parse(Console.ReadLine());

                Console.Write("Enter New Date (MM/DD/YYYY): ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter New Category: ");
                string category = Console.ReadLine();

                Console.WriteLine("Enter New Description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter New Amount: ");
                decimal amount = Decimal.Parse(Console.ReadLine());

                Expense updExpense = new Expense(date, category, description, amount);
                m.UpdateExpense(index, updExpense);
                Console.WriteLine("Expense updated successfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input:{ex.Message}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            Console.ReadLine();
        }

        static void DeleteExpense(ExpenseManager m)
        {
            Console.Clear();
            Console.WriteLine("=== Delete Expense ===");
            ViewExpenses(m);

            try
            {
                Console.Write("Enter the expense number to delete: ");
                int index = Int32.Parse(Console.ReadLine())-1;

                m.DeleteExpense(index);
                Console.WriteLine("Expense deleted successfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}"); 
            }
        }



        static void Main(string[] args)
        {
            ExpenseManager manager = new ExpenseManager();
            bool running = true;

            while(running == true)
            {
                Console.Clear();
                Console.WriteLine("=== Personal Expense Tracker ===");
                Console.WriteLine("1.) Add New Expense\n" +
                    "2.) View Expenses\n" +
                    "3.) View Expenses by Category\n" +
                    "4.) View Expenses by Date Range\n" +
                    "5.) Generate Summary Report\n" +
                    "6.) Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddExpense(manager);
                        break;
                    case "2":
                        ViewExpenses(manager);
                        break;
                    case "3":
                        VEC(manager);
                        break;
                    case "4":
                        VEDR(manager);
                        break;
                    case "5" :
                        GSR (manager);
                        break;
                    case "6":
                        SaveFile(manager);  
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again");
                        break;
                }
            }
        }
    }
}
