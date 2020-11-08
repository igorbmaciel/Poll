using System;

namespace Poll.Domain.Entities
{
    public class Vote
    {
        public Vote()
        {

        }

        public Guid Id { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid TaskId { get; private set; }
        public string Comment { get; private set; }
        public DateTime Date { get; private set; }

        public virtual Employee Employee { get; internal set; }
        public virtual Tasks Tasks { get; internal set; }

        public Vote(Guid employeeId, Guid taskId, string comment)
        {
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            TaskId = taskId;
            Comment = comment;
            Date = DateTime.UtcNow;
        }       

        internal Vote AddVote(Guid employeeId, Guid taskId, string comment)
        {
            return new Vote(employeeId, taskId, comment);
        }

    }
}
