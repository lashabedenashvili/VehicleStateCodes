using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;
using VehicleStateCodes.Data.Domein.Domein;
using VehicleStateCodes.DataBase.GenericRepository;
using VehicleStateCodes.DataBase.Repositories.Interface;

namespace VehicleStateCodes.DataBase.Repositories.Implementation
{
    public class StateNumberRepository : GeneralRepository<StateNumber>, IStateNumberRepository
    {
        public StateNumberRepository(Context context) : base(context)
        {

        }
    }
}
