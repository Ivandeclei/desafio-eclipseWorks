using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class DbProjectReadAdapter : IDbProjectReadAdapter
    {
        private readonly Context _context;
        private DbSet<Project> _project;
        public DbProjectReadAdapter(Context context)
        {
            _context = context;
            _project = _context.Set<Project>();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
                var result = await _project.Include(x => x.Tasks).ToListAsync();

                await _context.SaveChangesAsync();
                return result;
        }


        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _project
                .Include(s => s.Tasks)
                               .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
