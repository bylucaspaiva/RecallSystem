using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecallSystem.Domain.Entities
{
    public class ExecucaoRecall
    {
        public int Id { get; set; }
        public int RecallId { get; set; }
        public string Chassi { get; set; }
        public DateTime? DataExecucao { get; set; }
        public string Concessionaria { get; set; }
        public Recall Recall { get; set; }
    }
}
