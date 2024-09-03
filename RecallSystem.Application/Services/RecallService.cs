using RecallSystem.Application.Interfaces;
using RecallSystem.Domain.Entities;
using RecallSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecallSystem.Application.Services
{
    public class RecallService : IRecallService
    {
        private readonly IRecallRepository _recallRepository;

        public RecallService(IRecallRepository recallRepository)
        {
            _recallRepository = recallRepository;
        }

        public async Task<IEnumerable<Recall>> GetAllRecallsAsync()
        {
            return await _recallRepository.GetAllRecallsAsync();
        }

        public async Task<IEnumerable<ExecucaoRecall>> GetRecallsByChassiAsync(string chassi)
        {
            return await _recallRepository.GetRecallsByChassiAsync(chassi);
        }
    }
}
