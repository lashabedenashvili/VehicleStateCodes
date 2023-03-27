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
}
