using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 应用数据库
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Works> Works { get; set; }
    }
}
