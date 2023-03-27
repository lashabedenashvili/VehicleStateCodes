using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.DataBase.Repositories.Interface;

namespace VehicleStateCodes.DataBase.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IUserPasswordHistoryRepository UserPasswordHistory { get; }
        void SaveChanges();
        void Rollback();
        Task SaveChangesAsync();
        Task RollbackAsync();
    }
}
