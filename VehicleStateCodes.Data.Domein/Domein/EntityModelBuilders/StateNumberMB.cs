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
    public class StateNumberMB : IEntityTypeConfiguration<StateNumber>
    {      

        public void Configure(EntityTypeBuilder<StateNumber> modelBuilder)
        {            
                modelBuilder.Property(x => x.Id).IsRequired();
                modelBuilder.Property(x => x.Number).HasMaxLength(7).IsRequired();
                modelBuilder.Property(x => x.CreateTime).IsRequired().HasColumnType("date");    
            
           modelBuilder
                .HasMany(stateNumber => stateNumber.StateNumberOrder)
                .WithOne(stateNumberOrder => stateNumberOrder.StateNumber)
                .HasForeignKey(stateNumber => stateNumber.StateNumberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(stateNumber => stateNumber.StateNumberReservation)
                .WithOne(stateNumberReservation => stateNumberReservation.StateNumber)
                .HasForeignKey(stateNumber => stateNumber.StateNumberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
