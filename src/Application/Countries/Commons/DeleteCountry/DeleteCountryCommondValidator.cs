using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Countries.Commons.DeleteCountry
{
    public class DeleteCountryCommondValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommondValidator()
        {
            
        }
    }
}
