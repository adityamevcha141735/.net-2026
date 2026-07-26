using System;
using System.Collections.Generic;
using System.Linq;

// Custom Exceptions
public class InvalidExpenseException : Exception
{
    public InvalidExpenseException(string message) : base(message) { }
}

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message) { }
}

// Expense class
public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }

    public override string ToString()
    {
        return $"[ID: {Id}] {Description} - ₹{Amount:F2} ({Category})";
    }
}

// Expense Tracker
public class ExpenseTracker
{
    private List<Expense> expenses = new List<Expense>();
    private decimal budget;
    private int nextId = 1;

    public ExpenseTracker(decimal budgetLimit)
    {
        if (budgetLimit < 0)
            throw new InvalidExpenseException("Budget cannot be negative.");
        budget = budgetLimit;
    }

    public void AddExpense(string description, decimal amount, string category)
    {
        if (amount <= 0)
            throw new InvalidExpenseException("Amount must be greater than zero.");

        decimal total = expenses.Sum(e => e.Amount);
        if (total + amount > budget)
            throw new InsufficientFundsException($"Budget exceeded! Current: ₹{total:F2}, Attempting: ₹{amount:F2}");

        expenses.Add(new Expense 
        { 
            Id = nextId++, 
            Description = description, 
            Amount = amount, 
            Category = category 
        });
        Console.WriteLine("✓ Expense added!");
    }

    public void RemoveExpense(int id)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == id);
        if (expense == null)
            throw new InvalidExpenseException($"Expense ID {id} not found.");
        
        expenses.Remove(expense);
        Console.WriteLine("✓ Expense removed!");
    }

    public decimal GetTotal() => expenses.Sum(e => e.Amount);

    public decimal GetByCategory(string category) => 
        expenses.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.Amount);

    public void DisplayAll()
    {
        Console.WriteLine("\n--- All Expenses ---");
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.");
            return;
        }
        foreach (var exp in expenses)
            Console.WriteLine(exp);
    }

    public void DisplaySummary()
    {
        decimal total = GetTotal();
        Console.WriteLine("\n=== SUMMARY ===");
        Console.WriteLine($"Total: ₹{total:F2}");
        Console.WriteLine($"Budget: ₹{budget:F2}");
        Console.WriteLine($"Remaining: ₹{(budget - total):F2}");
        Console.WriteLine($"Count: {expenses.Count}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Expense Tracking Module ===\n");

        try
        {
            ExpenseTracker tracker = new ExpenseTracker(1000);

            // Add expenses
            tracker.AddExpense("Groceries", 150, "Food");
            tracker.AddExpense("Gas", 50, "Transport");
            tracker.AddExpense("Movie", 20, "Entertainment");
            tracker.AddExpense("Electric Bill", 80, "Utilities");

            tracker.DisplayAll();
            tracker.DisplaySummary();

            // Test exceptions
            Console.WriteLine("\n--- Testing Exceptions ---");

            // Invalid amount
            try
            {
                tracker.AddExpense("Test", -50, "Test");
            }
            catch (InvalidExpenseException ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            // Budget exceeded
            try
            {
                tracker.AddExpense("Luxury", 900, "Shopping");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            // Remove expense
            tracker.RemoveExpense(1);
            tracker.DisplaySummary();

            Console.WriteLine("\n✓ Completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}