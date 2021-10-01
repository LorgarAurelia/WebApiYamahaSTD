using Microsoft.EntityFrameworkCore;

namespace WebApiyamaha.Models
{
    public class Context : DbContext 
    {
        public Context (DbContextOptions<Context> options) 
            : base(options)
        { }
        public DbSet<ModelsInfo> YamahaData { get; set; }
    }
}
