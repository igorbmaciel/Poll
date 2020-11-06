using System;

namespace Poll.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {

        }

        public Guid Id { get; private set; }
        public string Name  {get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Employee(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }       

        internal static Employee AddEmployee(string name, string email, string password)
        {
            return new Employee(name, email, password);
        }       
    }
}
