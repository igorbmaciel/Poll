using MediatR;
using Poll.Application.Interfaces;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace Poll.Application.Services
{
    public class EmployeeAppService : ApplicationServiceBase, IEmployeeAppService
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeAppService(
           INotificationHandler notification,
           IEmployeeRepository employeeRepository,
           IMediator mediator)
           : base(notification)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;

        }

        public async Task<AddEmployeeResponse> AddEmployee(AddEmployeeCommand command)
        {
            var response = await _mediator.Send(command);

            if (Notification.HasNotification())
                return null;

            return response;
        }

        public async Task<List<GetAllEmployeeResponse>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return EmployeeResponse(employees);
        }

        private List<GetAllEmployeeResponse> EmployeeResponse(List<Employee> employees)
        {
            var employeeResponseList = new List<GetAllEmployeeResponse>();
            employees.ForEach(e => employeeResponseList.Add(new GetAllEmployeeResponse()
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email
            }));

            return employeeResponseList;
        }
    }
}
