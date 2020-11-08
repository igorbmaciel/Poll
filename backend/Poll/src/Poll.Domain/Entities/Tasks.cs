using Poll.Domain.AppConst;
using System;
using System.Collections.Generic;
using Tnf.Notifications;

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

        public Tasks(string name, INotificationHandler notification)
        {
            if (name.IsNullOrEmpty())
            {
                InvalidTaskName(notification);
                return;
            }

            Id = Guid.NewGuid();
            Name = name;
        }

        internal Tasks AddTask(string name, INotificationHandler notification)
        {
            return new Tasks(name, notification);
        }

        private void InvalidTaskName(INotificationHandler notification)
        {
            notification.Raise(notification
                 .DefaultBuilder
                 .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidTaskName)
                 .Build());
        }

        public enum EntityError
        {
            InvalidTaskName
        }
    }
}
