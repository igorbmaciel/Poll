using FluentValidation;
using MediatR;
using Poll.Domain.Base;
using Poll.Domain.Queries.Response;

namespace Poll.Domain.Queries.Request
{
    public class AddEmployeeCommand : BaseCommand, IRequest<AddEmployeeResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddEmployeeValidator().Validate(this);

            return ValidationResult.IsValid;
        }

        public class AddEmployeeValidator : AbstractValidator<AddEmployeeCommand>
        {
            public AddEmployeeValidator()
            {
                RuleFor(e => e.Name)
                    .NotEmpty()
                    .WithState(e => EntityError.InvalidEmployeeName);


                RuleFor(e => e.Email)
                    .NotEmpty()
                    .WithState(e => EntityError.InvalidEmployeeEmail);


                RuleFor(e => e.Password)
                    .NotEmpty()
                    .WithState(e => EntityError.InvalidEmployeePassword);
            }

            public enum EntityError
            {
                InvalidEmployeeName,
                InvalidEmployeeEmail,
                InvalidEmployeePassword
            }
        }
    }
}
