using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecallSystem.Infrastructure.Data
{
    public class RecallDbContext :DbContext
    {
        public RecallDbContext(DbContextOptions<RecallDbContext> options) : base(options) { }
    }
}
