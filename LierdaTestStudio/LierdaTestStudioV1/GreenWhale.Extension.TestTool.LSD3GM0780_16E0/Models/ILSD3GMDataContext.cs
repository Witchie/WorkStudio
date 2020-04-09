using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    public interface ILSD3GMDataContext
    {
        DbSet<TestCategory> TestCategorys { get; set; }
        int SaveChanges();
        void RemoveRange(params object[] models);
        EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity) where TEntity : class;
        void RemoveRange([NotNull] IEnumerable<object> entities);
    }
}