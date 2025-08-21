using ExamManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace ExamManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.LazyLoadingEnabled = false;
    }
    

    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<SchoolClass> SchoolClass { get; set; }
    public DbSet<Lesson> Lesson { get; set; }
    public DbSet<Exam> Exam { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Teacher>().HasKey(x => x.Id).IsClustered(false);
        builder.Entity<Student>().HasKey(x => x.Id).IsClustered(false);
        builder.Entity<SchoolClass>().HasKey(x => x.Id).IsClustered(false);
        builder.Entity<Lesson>().HasKey(x => x.Id).IsClustered(false);
        builder.Entity<Exam>().HasKey(x => x.Id).IsClustered(false);


        builder.Entity<Student>().HasOne(s => s.SchoolClass).WithMany().HasForeignKey(s => s.SchoolClassId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Lesson>().HasOne(l => l.SchoolClass).WithMany().HasForeignKey(l => l.SchoolClassId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Lesson>().HasOne(l => l.Teacher).WithMany().HasForeignKey(l => l.TeacherId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Exam>().HasOne(e => e.Lesson).WithMany().HasForeignKey(e => e.LessonId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Exam>().HasOne(e => e.Student).WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);

        var models = builder.Model.GetEntityTypes();

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}