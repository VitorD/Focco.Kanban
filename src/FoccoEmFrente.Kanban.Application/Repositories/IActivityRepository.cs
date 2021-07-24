using FoccoEmFrente.Kanban.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Repositories
{
   public interface IActivityRepository : IRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetAllAsync();

        Task<Activity> GetByIdAsync(Guid Id);
        Activity Add(Activity activity);
        Activity Update(Activity activity);

        Activity Remove(Activity IdActivity);

    }
}
