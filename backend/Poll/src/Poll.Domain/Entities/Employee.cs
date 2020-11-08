using Poll.Domain.AppConst;
using System;
using System.Collections.Generic;
using Tnf.Notifications;

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

        public virtual List<Vote> VoteList { get; internal set; }

        public Employee(string name, string email, string password, INotificationHandler notification)
        {
            if (name.IsNullOrEmpty())
            {
                InvalidEmployeeName(notification);
                return;
            }

            if (email.IsNullOrEmpty())
            {
                InvalidEmployeeEmail(notification);
                return;
            }

            if (password.IsNullOrEmpty())
            {
                InvalidEmployeePassword(notification);
                return;
            }

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }

        internal Employee AddEmployee(string name, string email, string password, INotificationHandler notification)
        {
            return new Employee(name, email, password, notification);
        }

        private void InvalidEmployeeName(INotificationHandler notification)
        {

            notification.Raise(notification
                .DefaultBuilder
                .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidEmployeeName)
                .Build());
        }

        private void InvalidEmployeeEmail(INotificationHandler notification)
        {

            notification.Raise(notification
                .DefaultBuilder
                .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidEmployeeEmail)
                .Build());
        }

        private void InvalidEmployeePassword(INotificationHandler notification)
        {

            notification.Raise(notification
                .DefaultBuilder
                .WithMessage(AppConsts.LocalizationSourceName, EntityError.InvalidEmployeePassword)
                .Build());
        }

        public enum EntityError
        {
            InvalidEmployeeName,
            InvalidEmployeeEmail,
            InvalidEmployeePassword,
            InvalidEmployee
        }
    }
}
