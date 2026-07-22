using System;

// Interface
interface IPayroll
{
    void CalculateSalary();
}

// Base Class
class Employee
{
    public string Name;
    public int Id;

    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

// Derived Class
class FullTimeEmployee : Employee, IPayroll
{
    public double Salary;

    public FullTimeEmployee(string name, int id, double salary) : base(name, id)
    {
        Salary = salary;
    }

    public void CalculateSalary()
    {
        Console.WriteLine("Employee: " + Name);
        Console.WriteLine("Salary: " + Salary);
    }
}

class Program
{
    static void Main()
    {
        IPayroll emp = new FullTimeEmployee("NAMOR", 101, 30000);
        emp.CalculateSalary();   // Polymorphism
    }
}