using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStateCodes.Data.Domein.Data
{
    public class StateNumber : IGlobalId
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime? CreateTime { get; set; }
        public IList<StateNumberOrder> StateNumberOrder { get; set; }
        public IList<StateNumberReservation> StateNumberReservation { get; set; }
    }
}
