using Microsoft.EntityFrameworkCore;

namespace MGR.CommandLineParser.Command.OracleProcedure
{
    internal sealed class OracleSystemDataContext : DbContext
    {
        public DbSet<Procedure> Procedures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProcedureMap())
                .ApplyConfiguration(new ParameterMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
