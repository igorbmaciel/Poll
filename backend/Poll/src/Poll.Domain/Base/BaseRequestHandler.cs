using Poll.Domain.AppConst;
using Poll.Domain.Enum;
using Tnf.Notifications;

namespace Poll.Domain.Base
{
    public class BaseRequestHandler
    {
        protected readonly INotificationHandler _notification;      

        protected BaseRequestHandler(INotificationHandler notification)
        {
            _notification = notification;
        }

        protected virtual void NotifyNullOrEmptyObject()
        {
            _notification.RaiseError(AppConsts.LocalizationSourceName, CommonsEnum.Error.NullOrEmptyObject);
        }

        protected bool IsValid(BaseCommand command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
                _notification.RaiseError(AppConsts.LocalizationSourceName, (System.Enum)error.CustomState);

            return false;
        }
    }
}
