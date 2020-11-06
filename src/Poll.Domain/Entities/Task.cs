using System;

namespace Poll.Domain.Entities
{
    public class Task
    {
        public Task()
        {

        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Task(string name)
        {
            Id = Guid.NewGuid();
            Name = name;           
        }      

        internal static Task AddTask(string name)
        {
            return new Task(name);
        }       
    }
}
