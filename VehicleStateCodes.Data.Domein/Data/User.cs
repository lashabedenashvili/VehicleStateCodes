using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VehicleStateCodes.Data.Domein.Data.Enum.Enum;

namespace VehicleStateCodes.Data.Domein.Data
{
    public class User : IGlobalId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Cityzen Cityzen { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserPasswordHistory> UserPasswordHistory { get; set; }
    }
}