using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlite;

public class AppDbContext : DbContext
{
    public DbSet<GroupInfo> GroupInfo { get; set; }
    public DbSet<ShortcutInfo> ShortcutInfo { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}