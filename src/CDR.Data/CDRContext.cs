using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CDR.Data;
public class CDRContext : DbContext
{
    public CDRContext(DbContextOptions<CDRContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entities.CDR> CDRs { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Entities.CDR>(entity =>
    //    {
    //        entity.ToTable("CDRs");
    //    });
    //}
}