﻿using FluentValidation;
using Library.Infrastructure.PropertyValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Infrastructure.Dto.StateNumberDto
{
    public class FilteringStateNumberDto
    {
        public int? Id { get; set; }
        public string StateNumber { get; set; }
        public DateTime? CreateFrom { get; set; }
        public DateTime? CreatoTo { get; set; }
        public int PageNumb { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }

    public class FilteringStateNumberDtoValidator : AbstractValidator<FilteringStateNumberDto>
    {
        private readonly IPropertyValidators _validator;
        public FilteringStateNumberDtoValidator(IPropertyValidators validator)
        {
            _validator = validator;
            RuleFor(x => x.StateNumber)
                .NotEmpty()
                .Must(_validator.StateNumberValidator)
                .WithMessage(_validator.errNotCorretFormat)
                .MaximumLength(7);

            RuleFor(x => x.PageNumb)
                .LessThan(50);


            RuleFor(x => x.PageSize)
                .LessThan(50);


        }

    }
}
