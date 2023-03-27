﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VehicleStateCodes.Data.Domein.Data.Enum.Enum;

namespace VehicleStateCodes.Infrastructure.Dto.UserDto
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string PrivateNumber { get; set; }
        public Cityzen Cityzen { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
