using FoccoEmFrente.Kanban.Application.Context;
using FoccoEmFrente.Kanban.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        protected readonly KanbanContext DbContext;

        protected readonly DbSet<Activity> DbSet;

        public ActivityRepository(KanbanContext context) {

            DbContext = context;
            DbSet = DbContext.Set<Activity>();
        
        
        }

        public IUnitOfWork UnitOfWork => DbContext;

        public Activity Add(Activity activity)
        {
            var entry = DbSet.Add(activity);
            
            return entry.Entity;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public async Task<bool> ExistsAsync(Guid id,Guid userId)
        {
            return await DbSet.Where(f => f.Id == id && f.UserId == userId).AnyAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(Guid userId)
        {
            return await DbSet
                .Where(a=> a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Activity> GetByIdAsync(Guid Id, Guid userId)
        {
            return await DbSet
                 .Where(a => a.UserId == userId && a.Id ==Id)
                .FirstOrDefaultAsync();
        }


        public Activity Remove(Activity activity)
        {
        var entry =  DbSet.Remove(activity);
            return entry.Entity;
           
        }

        public Activity Update(Activity activity)
        {
            var entry = DbSet.Update(activity);
            return entry.Entity;
        }
    }
}
