using System.Linq.Expressions;
using ExamManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamManagementSystem.Data;

public class Repo
{
    private bool track = false;
    public string ConnectionString { get; set; }
    public ApplicationDbContext DB { get; }

    public Repo(ApplicationDbContext dB, IConfiguration configuration)
    {
        DB = dB;
        ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public void EnableTracking(bool enable = true)
    {
        track = enable;
        DB.ChangeTracker.AutoDetectChangesEnabled = enable;
        DB.ChangeTracker.QueryTrackingBehavior = (enable) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
    }

    public IQueryable<T> Get<T>() where T : class, IEntity
    {
        if (track)
        {
            return DB.Set<T>();
        }
        return DB.Set<T>().AsNoTracking();
    }

    public IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
    {
        return Get<T>().Where(predicate);
    }

    public async Task<T> Add<T>(T entity) where T : class, IEntity
    {
        return await Save(DB.Add(entity));
    }

    public async Task<T> Update<T>(T entity) where T : class, IEntity
    {
        return await Save(DB.Update(entity));
    }

    public async Task<T> Remove<T>(T entity) where T : class, IEntity
    {
        return await Save(DB.Remove(entity));
    }

    private async Task<T> Save<T>(EntityEntry<T> entity) where T : class, IEntity
    {
        await DB.SaveChangesAsync();

        if (!track)
        {
            entity.State = EntityState.Detached;
        }

        return entity.Entity;
    }
}