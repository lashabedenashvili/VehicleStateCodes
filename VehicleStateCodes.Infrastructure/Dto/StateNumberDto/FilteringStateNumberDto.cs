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
}
