using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    public class dbContext : DbContext
    {
        public DbSet<MachineShift> MachineShift { get; set; }
        public DbSet<myuser> myuser { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<ProdShift> ProdShift { get; set; }
        public DbSet<SettingBaseProcess> SettingBaseProcess { get; set; }
        public DbSet<SettingAvailableSymbolic> SettingAvailableSymbolic { get; set; }
        public DbSet<GetShift5S> GetShift5S { get; set; }
        public DbSet<_5SDone> _5SDone { get; set; }
        public DbSet<SettingSymbolic> SettingSymbolic { get; set; }
        public DbSet<SettingDetailProcess> SettingDetailProcess { get; set; }
        public DbSet<SettingFrequens> SettingFrequens { get; set; }
        public DbSet<SettingValue> SettingValue { get; set; }
        public DbSet<SettingConnections> SettingConnections { get; set; }
        public DbSet<SettingExecutedReadings> SettingExecutedReadings { get; set; }
    }
}