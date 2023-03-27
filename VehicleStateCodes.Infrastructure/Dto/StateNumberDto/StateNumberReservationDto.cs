using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Infrastructure.Dto.StateNumberDto
{
    public class StateNumberReservationDto
    {   public int Id { get; set; }
        public int StateNumberId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndReservation { get; set; }
    }
}
