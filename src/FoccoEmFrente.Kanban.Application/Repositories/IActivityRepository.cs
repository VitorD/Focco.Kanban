using FoccoEmFrente.Kanban.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Repositories
{
   public interface IActivityRepository : IRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetAllAsync(Guid userId);

        Task<Activity> GetByIdAsync(Guid Id, Guid userId);
        Activity Add(Activity activity);
        Activity Update(Activity activity);

        Activity Remove(Activity IdActivity);

        Task<bool> ExistsAsync(Guid id, Guid userId);


    }
}
