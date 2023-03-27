using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Data.Domein.Data
{
  
        public class UserPasswordHistory : IGlobalId
        {
            public int Id { get; set; }            
            public int UserId { get; set; }
            public User User { get; set; }            
            public byte[] PasswordHash { get; set; }            
            public byte[] PasswordSalt { get; set; }            
            public DateTime CreateTime { get; set; }        
            public bool IsActive { get; set; }
            public DateTime? UpdateTime { get; set; }
        }
    
}
