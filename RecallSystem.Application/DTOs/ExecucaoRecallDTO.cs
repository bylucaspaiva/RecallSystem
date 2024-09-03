using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecallSystem.Application.DTOs
{
    public class ExecucaoRecallDTO
    {
        public int Id { get; set; }
        public int RecallId { get; set; }
        public string Chassi { get; set; }
        public DateTime? DataExecucao { get; set; }
        public string Concessionaria { get; set; }
        public RecallDTO Recall { get; set; }
    }
}
