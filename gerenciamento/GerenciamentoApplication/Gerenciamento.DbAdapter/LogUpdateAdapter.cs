﻿using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class LogUpdateAdapter : ILogUpdateAdapter
    {
        private readonly Context _context;
        private DbSet<HistoryUpdate> _historyUpdate;
        public LogUpdateAdapter(Context context)
        {
            _context = context;
            _historyUpdate = _context.Set<HistoryUpdate>();
        }

        public async Task SaveAsync(HistoryUpdate historicoAtualizacao)
        {
            await _historyUpdate.AddAsync(historicoAtualizacao);
            await _context.SaveChangesAsync();

        }
    }
}
