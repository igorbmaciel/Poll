using System;

namespace Poll.Domain.Entities
{
    public class Tasks
    {
        public Tasks()
        {

        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Tasks(string name)
        {
            Id = Guid.NewGuid();
            Name = name;           
        }      

        internal static Tasks AddTask(string name)
        {
            return new Tasks(name);
        }       
    }
}
