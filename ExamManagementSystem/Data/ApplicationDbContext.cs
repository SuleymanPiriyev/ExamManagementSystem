using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
//using Models = ExamManagementSystem.Models;
namespace ExamManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.LazyLoadingEnabled = false;
    }
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var models = builder.Model.GetEntityTypes();

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}