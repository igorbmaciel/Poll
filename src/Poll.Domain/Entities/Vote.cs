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

        public Vote(Guid employeeId, Guid taskId, string comment, DateTime date)
        {
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            TaskId = taskId;
            Comment = comment;
            Date = date;
        }       

        internal static Vote AddVote(Guid employeeId, Guid taskId, string comment, DateTime date)
        {
            return new Vote(employeeId, taskId, comment, date);
        }

    }
}
