using FluentValidation;
using MediatR;
using Poll.Domain.Base;
using Poll.Domain.Queries.Response;
using System;

namespace Poll.Domain.Queries.Request
{
    public class AddVoteCommand : BaseCommand, IRequest<AddVoteResponse>
    {
        public Guid EmployeeId { get; set; }
        public Guid TaskId { get; set; }
        public string Comment { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddVoteValidator().Validate(this);

            return ValidationResult.IsValid;
        }       
    }

    public class AddVoteValidator : AbstractValidator<AddVoteCommand>
    {
        public AddVoteValidator()
        {
            RuleFor(e => e.EmployeeId)
                .NotEmpty()
                .WithState(e => EntityError.InvalidEmployeeId);

            RuleFor(e => e.TaskId)
                .NotEmpty()
                .WithState(e => EntityError.InvalidTaskId);

            RuleFor(e => e.Comment)
                .NotEmpty()
                .WithState(e => EntityError.InvalidComment);
        }

        public enum EntityError
        {
            InvalidEmployeeId,
            InvalidTaskId,
            InvalidComment
        }
    }
}
