using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class DbProjectWriteAdapter : IDbProjectWriteAdapter
    {
        private readonly Context _context;
        private DbSet<Project> _project;
        public DbProjectWriteAdapter(Context context)
        {
            _context = context;
            _project = _context.Set<Project>();
        }

        public async Task DeleteAsync(Project project)
        {
            _project.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> SaveAsync(Project project)
        {
            _project.AddAsync(project);
            await _context.SaveChangesAsync();
            return project.Id;
        }

        public async Task UpdateAsync(Project project)
        {
            _project.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
