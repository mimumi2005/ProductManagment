using FluentValidation;
using ProductManagment.Application.Audits.Queries;
using System;

namespace ProductManagment.Application.Audits.Validation
{
    public class GetAuditsQueryValidator : AbstractValidator<GetAuditsQuery>
    {
        public GetAuditsQueryValidator()
        {
            RuleFor(x => x)
                .Must(x => !x.From.HasValue || !x.To.HasValue || x.From <= x.To)
                .WithMessage("From date must be earlier than or equal to To date.");
        }
    }
}
