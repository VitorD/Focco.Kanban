using FoccoEmFrente.Kanban.Application.Entities;
using FoccoEmFrente.Kanban.Application.Enums;
using FoccoEmFrente.Kanban.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Services
{
    public class ActivityService : IActivityService
    {

        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<Activity> AddAsync(Activity activity)
        {
            var newactivity = _activityRepository.Add(activity);
            await _activityRepository.UnitOfWork.CommitAsync();
            return newactivity;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<bool> ExistsAsync(Guid id, Guid userId)
        {
            return await _activityRepository.ExistsAsync(id, userId);
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(Guid userId)
        {
          return  await _activityRepository.GetAllAsync(userId);
        }

        public  async Task<Activity> GetByIdAsync(Guid Id, Guid userId)
        {
            return await _activityRepository.GetByIdAsync(Id, userId);
        }

        public async Task<Activity> RemoveAsync(Guid Id, Guid userId)
        {
            var activityTobeRemoved = await GetByIdAsync(Id, userId);
            if (activityTobeRemoved == null)
                throw new Exception("Atividade nao pode ser encontrada");

            var oldactivity = _activityRepository.Remove(activityTobeRemoved);
            await _activityRepository.UnitOfWork.CommitAsync();
            return oldactivity;

        }

        public async Task<Activity> RemoveAsync(Activity activity)
        {
            var activityTobeRemoved = await GetByIdAsync(activity.Id, activity.UserId);
            if (activityTobeRemoved == null)
                throw new Exception("Atividade nao pode ser encontrada");

            var oldactivity = _activityRepository.Remove(activity);
            await _activityRepository.UnitOfWork.CommitAsync();
            return oldactivity;
        }

        public async Task<Activity> UpdateAsync(Activity activity)
        {
            var activityExists = await ExistsAsync(activity.Id, activity.UserId);
            if (!activityExists)
                throw new Exception("Atividade nao pode ser encontrada");

            var oldactivity = _activityRepository.Update(activity);
            await _activityRepository.UnitOfWork.CommitAsync();
            return oldactivity;
            
        }

        public async Task<Activity> UpdateToTodoAsync(Guid id,Guid userId)
        {

            return await UpdateStatusAsync(id, userId, ActivityStatus.Todo);
        }
        public async Task<Activity> UpdateToDoingAsync(Guid id, Guid userId)
        {

            return await UpdateStatusAsync(id, userId, ActivityStatus.Doing);
        }
        public async Task<Activity> UpdateToDoneAsync(Guid id, Guid userId)
        {

            return await UpdateStatusAsync(id, userId, ActivityStatus.Done);
        }

        public async Task<Activity> UpdateStatusAsync(Guid id, Guid userId, ActivityStatus status)
        {

            var activity = await GetByIdAsync(id, userId);
            activity.Status = status;
            return await UpdateAsync(activity);
        }

    }
}
