using DevExpress.Data.Browsing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    public class LSD3GMDataContext : DbContext, ILSD3GMDataContext
    {
        public LSD3GMDataContext([NotNull] DbContextOptions options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected LSD3GMDataContext()
        {
        }
        public DbSet<TestCategory> TestCategorys { get; set; }
    }
}
