using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Data.Domein.Data
{
    public class StateNumberOrder:IGlobalId
    {
        public int Id { get; set; }
        public int StateNumberId { get; set; }
        public StateNumber StateNumber { get; set; }
        public DateTime CreateTime { get; set; }
        
    }
}
