using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;

namespace Gerenciamento.Application
{
    public class ProjectService : IProjectService
    {
        private readonly IDbProjectReadAdapter _dbProjectReadAdapter;
        private readonly IDbProjectWriteAdapter _dbProjectWriteAdapter;
        public ProjectService(IDbProjectReadAdapter dbProjectReadAdapter, IDbProjectWriteAdapter dbProjectWriteAdapter)
        {
            this._dbProjectReadAdapter = dbProjectReadAdapter ??
                 throw new ArgumentNullException(nameof(dbProjectReadAdapter));
            this._dbProjectWriteAdapter = dbProjectWriteAdapter ??
                 throw new ArgumentNullException(nameof(dbProjectWriteAdapter));
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _dbProjectReadAdapter.GetByIdAsync(id);

            if (project == null)
            {
                throw new CustomException(ExceptionMessages.REGISTER_NOT_FOUND);
            }

            bool hasPendingTasks = project.Tasks.Any(t => t.Status == StatusBase.Pending);

            if (hasPendingTasks)
            {
                throw new CustomException(ExceptionMessages.PROJECT_WITH_PENDING_TASK);
            }

            await _dbProjectWriteAdapter.DeleteAsync(project);
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _dbProjectReadAdapter.GetAllAsync();
        }

        public async Task SaveProjectAsync(Project project)
        {
            if (project is null)
                throw new CustomException(ExceptionMessages.REGISTER_IS_EMPTY);

            await _dbProjectWriteAdapter.SaveAsync(project);
        }
    }
}
