using FluentValidation;
using Library.Infrastructure.PropertyValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Infrastructure.Dto.StateNumberDto
{
    public class AddStateNumberDto
    {
        public string StateNumber { get; set; }
    }
    public class AddStateNumberDtoValidator : AbstractValidator<AddStateNumberDto>
    {
        private readonly IPropertyValidators _validator;
        public AddStateNumberDtoValidator(IPropertyValidators validator)
        {
            _validator = validator;
            RuleFor(x => x.StateNumber)
                .NotEmpty()
                .Must(_validator.StateNumberValidator)
                .WithMessage(_validator.errNotCorretFormat)
                .MaximumLength(7);


            


        }
    }
}
