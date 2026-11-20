using Marketplace.Core;

namespace App.Lab4.Users.Models;

public class Admin : User
{
    private List<Employee> employees;
    public List<Employee> Employees
    {
        get => employees; 
        init => employees = value;
    }

    public override User Clone()
    {
        var clone = MemberwiseClone() as Admin;
        if (employees != null)
        {
            clone.employees = [.. Employees];
        }
    
        return clone;
    }
}
