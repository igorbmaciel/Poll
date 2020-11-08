using Poll.Domain.AppConst;
using System;
using Tnf.Notifications;

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

        public Vote(Guid employeeId, Guid taskId, string comment, INotificationHandler notification)
        {
            if (employeeId == default)
            {
                InvalidEmployeeId(notification);
                return;
            }

            if (taskId == default)
            {
                InvalidTaskId(notification);
                return;
            }

            if (comment.IsNullOrEmpty())
            {
                InvalidComment(notification);
                return;
            }

            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            TaskId = taskId;
            Comment = comment;
            Date = DateTime.UtcNow;
        }
        internal Vote AddVote(Guid employeeId, Guid taskId, string comment, INotificationHandler notification)
        {
            return new Vote(employeeId, taskId, comment, notification);
        }

        private void InvalidComment(INotificationHandler notification)
        {
            notification.Raise(notification
                .DefaultBuilder
                .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidComment)
                .Build());
        }

        private void InvalidTaskId(INotificationHandler notification)
        {
            notification.Raise(notification
                 .DefaultBuilder
                 .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidTaskId)
                 .Build());
        }

        private void InvalidEmployeeId(INotificationHandler notification)
        {
            notification.Raise(notification
                .DefaultBuilder
                .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidEmployeeId)
                .Build());
        }        

        public enum EntityError
        {
            InvalidEmployeeId,
            InvalidTaskId,
            InvalidComment
        }
    }
}
