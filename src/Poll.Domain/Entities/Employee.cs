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

        public Employee(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        internal static Employee AddEmployee(string name, string email, string password)
        {
            return new Employee(name, email, password);
        }

        internal static Employee ChangeEmployee(Guid id, string name, string email)
        {
            return new Employee(id, name, email);
        }
    }
}
