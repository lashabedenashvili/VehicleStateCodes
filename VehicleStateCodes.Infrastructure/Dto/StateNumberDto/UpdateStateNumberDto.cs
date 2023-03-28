using FluentValidation;
using Library.Infrastructure.PropertyValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Infrastructure.Dto
{
    public class UpdateStateNumberDto
    {        
        public string StateNumber { get; set; }
        public DateTime? CreateTime { get; set; }
    }
    public class UpdateStateNumberDtoValidator : AbstractValidator<UpdateStateNumberDto>
    {
        private readonly IPropertyValidators _validator;
        public UpdateStateNumberDtoValidator(IPropertyValidators validator)
        {
            _validator = validator;
            RuleFor(x => x.StateNumber)
                .NotEmpty()
                .Must(_validator.StateNumberValidator)
                .WithMessage(_validator.errNotCorretFormat)
                .MaximumLength(7);


            RuleFor(x => x.CreateTime)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(_validator.errNotCorretFormat);


        }
    }
}
