using CleanArchitecture.Application.Countries.Commons.CreateCountry;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Validations;

public class CreateCountryCommondValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommondValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(200)
            .WithMessage("maximum length must be 200")
            .NotEmpty()
            .WithMessage("Country name required");
    }
}
