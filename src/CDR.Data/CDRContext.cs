using Microsoft.EntityFrameworkCore;

namespace CDR.Data;
public class CDRContext : DbContext
{
    public CDRContext()
    {
    }

    public CDRContext(DbContextOptions<CDRContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entities.CDR> CDRs { get; set; }
}