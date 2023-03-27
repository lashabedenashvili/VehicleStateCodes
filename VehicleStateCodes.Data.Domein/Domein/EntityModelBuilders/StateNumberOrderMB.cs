using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;

namespace VehicleStateCodes.Data.Domein.Domein.EntityModelBuilders
{
    public class StateNumberOrderMB : IEntityTypeConfiguration<StateNumberOrder>
    {
        public void Configure(EntityTypeBuilder<StateNumberOrder> modelBuilder)
        {
            modelBuilder.Property(x => x.Id).IsRequired();
            modelBuilder.Property(x => x.StateNumberId).IsRequired();
            modelBuilder.Property(x => x.CreateTime).IsRequired().HasColumnType("date");
        }
    }
}
