using Microsoft.EntityFrameworkCore;

namespace clarityVenturesTest.Models;

public class RecipientsContext : DbContext
{
    public RecipientsContext (DbContextOptions<RecipientsContext> options)
        : base(options)
    {
    }

    public DbSet<Recipients> Recipients { get; set; } = null!;
}
