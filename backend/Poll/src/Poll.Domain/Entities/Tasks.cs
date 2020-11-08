using System;
using System.Collections.Generic;

namespace Poll.Domain.Entities
{
    public class Tasks
    {
        public Tasks()
        {

        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public virtual List<Vote> VoteList { get; internal set; }

        public Tasks(string name)
        {
            Id = Guid.NewGuid();
            Name = name;           
        }      

        internal Tasks AddTask(string name)
        {
            return new Tasks(name);
        }       
    }
}
