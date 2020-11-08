using MediatR;
using Poll.Domain.Base;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tnf.Notifications;
using Tnf.Repositories.Uow;

namespace Poll.Domain.Handlers
{
    public class AddEmployeeHandler : BaseRequestHandler, IRequestHandler<AddEmployeeCommand, AddEmployeeResponse>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmployeeRepository _employeeRepository;

        public AddEmployeeHandler(
          INotificationHandler notification,
          IUnitOfWorkManager unitOfWorkManager,
          IEmployeeRepository employeeRepository
          ) : base(notification)
        {
            _employeeRepository = employeeRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<AddEmployeeResponse> Handle(AddEmployeeCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command))
                return null;

            var employee = new Employee();

            var password = SetPassword(command.Password);

            employee = employee.AddEmployee(command.Name, command.Email, password, _notification);

            if (_notification.HasNotification())
                return null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _employeeRepository.AddEmployee(employee);
                uow.Complete();
            }          

            return EmployeeResponse(employee);
        }

        private AddEmployeeResponse EmployeeResponse(Employee employee)
        {
            return new AddEmployeeResponse()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email                
            };
        }

        private string SetPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(data);
            var encoding = new ASCIIEncoding();
            return encoding.GetString(sha1data);
        }
    }
}
