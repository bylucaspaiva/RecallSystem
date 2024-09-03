using RecallSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecallSystem.Application.Interfaces
{
    public interface IRecallService
    {
        Task<IEnumerable<Recall>> GetAllRecallsAsync();
        Task<IEnumerable<ExecucaoRecall>> GetRecallsByChassiAsync(string chassi);
    }
}
