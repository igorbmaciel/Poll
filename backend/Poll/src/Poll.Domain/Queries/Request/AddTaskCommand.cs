using FluentValidation;
using MediatR;
using Poll.Domain.Base;
using Poll.Domain.Queries.Response;

namespace Poll.Domain.Queries.Request
{
    public class AddTaskCommand : BaseCommand, IRequest<AddTaskResponse>
    {
        public string Name { get; set; }   

        public override bool IsValid()
        {
            ValidationResult = new AddTaskValidator().Validate(this);

            return ValidationResult.IsValid;
        }       
    }

    public class AddTaskValidator : AbstractValidator<AddTaskCommand>
    {
        public AddTaskValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithState(e => EntityError.InvalidTaskName);

        }

        public enum EntityError
        {
            InvalidTaskName
        }
    }
}
