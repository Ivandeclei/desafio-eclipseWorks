using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;
using System.Text.Json;


namespace Gerenciamento.Application
{
    public class TaskService : ITaskService
    {
        private readonly IDbTaskReadAdapter _dbTaskReadAdapter;
        private readonly IDbTaskWriteAdapter _dbTaskWriteAdapter;
        private readonly ILogUpdateAdapter _logUpdateAdapter;
        public TaskService(IDbTaskReadAdapter dbTaskReadAdapter, IDbTaskWriteAdapter dbTaskWriteAdapter, ILogUpdateAdapter logUpdateAdapter)
        {
            this._dbTaskReadAdapter = dbTaskReadAdapter ??
                 throw new ArgumentNullException(nameof(dbTaskReadAdapter));
            this._dbTaskWriteAdapter = dbTaskWriteAdapter ??
                 throw new ArgumentNullException(nameof(dbTaskWriteAdapter));
            this._logUpdateAdapter = logUpdateAdapter ??
                 throw new ArgumentNullException(nameof(logUpdateAdapter));
        }

        public async Task DeleteTaskAsync(Guid id, User user)
        {
            var task = await _dbTaskReadAdapter.GetByIdAsync(id);
            ValidateTask(task);

            await _dbTaskWriteAdapter.DeleteAsync(task);
            await SaveLogAsync(task, user.Name, CommonContants.DELETE, id);

        }

        public Task<IEnumerable<TaskProject>> GetTaskAsync(Guid idProject)
        {
            return _dbTaskReadAdapter.GetAllTaskByProjectAsync(idProject);
        }

        public async Task SaveTaskAsync(TaskProject task, User user)
        {
            ValidateTask(task);
            AddUserToTask(task, user.Name);
            var countTaskProject = await _dbTaskReadAdapter.GetCountTaskByIdProjectAsync(task.ProjectId);
            if (countTaskProject > 20)
            {
                throw new CustomException(ExceptionMessages.TASK_LIMIT_EXCEEDED);
            }
            var id = await _dbTaskWriteAdapter.SaveAsync(task);
            task.Id = id;
            await SaveLogAsync(task, user.Name, CommonContants.CREATE, id);

        }

        public async Task UpdateTaskAsync(TaskProject task, User user)
        {
            ValidateTask(task);
            AddUserToTask(task, user.Name);

            task.UpdatedAt = DateTime.Now;

            await _dbTaskWriteAdapter.UpdateAsync(task);
            await SaveLogAsync(task, user.Name, CommonContants.UPDATE, task.Id);
        }

        public async Task SaveCommentAsync(Comments comment, User user)
        {
            await SaveLogAsync(comment, user.Name, CommonContants.COMMENT, comment.TaskProjectId);
        }

        private void ValidateTask(TaskProject task)
        {
            if (task is null)
            {
                throw new CustomException(ExceptionMessages.REGISTER_IS_EMPTY);
            }
        }

        private async Task SaveLogAsync(object objectToSave, string user, string action, Guid id)
        {
            await _logUpdateAdapter.SaveAsync(new HistoryUpdate
            {
                Content = JsonSerializer.Serialize(objectToSave),
                User = user,
                Action = action,
                TaskProjectId = id
            });
        }

        private TaskProject AddUserToTask(TaskProject task,  string nameUser)
        {
            task.NameUser = nameUser;
            return task;
        }

    }
}
