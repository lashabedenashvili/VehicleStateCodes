using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;
using VehicleStateCodes.Data.Domein.Domein;
using VehicleStateCodes.DataBase.Repositories.Implementation;
using VehicleStateCodes.DataBase.Repositories.Interface;

namespace VehicleStateCodes.DataBase.UnitOfWork
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly Context _context;
        public IUserRepository User { get; private set; }
        public IUserPasswordHistoryRepository UserPasswordHistory { get; private set; }
        public UnitOFWork(Context context)
        {
            _context = context;
            User = new UserRepository(_context);
            UserPasswordHistory = new UserPasswordHistoryRepository(_context);
        }

        public void Rollback()
        {
            _context.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

