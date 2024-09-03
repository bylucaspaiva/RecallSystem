using Microsoft.EntityFrameworkCore;
using RecallSystem.Domain.Entities;
using RecallSystem.Domain.Interfaces;
using RecallSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecallSystem.Infrastructure.Repositories
{
    public class RecallRepository : IRecallRepository
    {
        private readonly RecallDbContext _context;

        public RecallRepository(RecallDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recall>> GetAllRecallsAsync()
        {
            return await _context.Recalls.ToListAsync();
        }

        public async Task<IEnumerable<ExecucaoRecall>> GetRecallsByChassiAsync(string chassi)
        {
            return await _context.ExecucoesRecalls
                .Where(er => er.Chassi == chassi)
                .Include(er => er.Recall)
                .ToListAsync();
        }
    }
}
