using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Infrastructure.Dto.StateNumberDto
{
    public class NumberDetailInfoDto:GetStateNumberDto    {
        
        public StateNumberOrderDto StateNumberOrder { get; set; }
        public StateNumberReservationDto StateNumberReservation { get; set; }
    }
}
