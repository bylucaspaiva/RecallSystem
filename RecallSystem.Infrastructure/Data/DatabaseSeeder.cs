using Microsoft.EntityFrameworkCore;
using RecallSystem.Domain.Entities;
using System;
using System.Linq;

namespace RecallSystem.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(RecallDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
           
            if (!dbContext.Recalls.Any())
            {
                var recalls = new[]
                {
                    new Recall { Titulo = "TROCA DISCO FREIO", Descricao = "Poderá resultar em ruídos anormais e deficiência na frenagem.", DataPublicacao = new DateTime(2023, 10, 29) },
                    new Recall { Titulo = "ATUALIZAÇÃO DE SOFTWARE", Descricao = "Poderá resultar em funcionamento incorreto do monitoramento dos sensores.", DataPublicacao = new DateTime(2019, 4, 1) },
                    new Recall { Titulo = "INSPEÇÃO SISTEMA DE PARTIDA", Descricao = "Poderá resultar em funcionamento incorreto na partida do veículo.", DataPublicacao = new DateTime(2021, 1, 30) },
                    new Recall { Titulo = "CORROSÃO CABO DO ACELERADOR", Descricao = "Poderá resultar em quebra no cabo, impedindo a aceleração do veículo.", DataPublicacao = new DateTime(2020, 3, 15) },
                    new Recall { Titulo = "SUBSTITUIÇÃO SENSOR DO VELOCÍMETRO", Descricao = "Poderá resultar em registro incorreto da quilometragem.", DataPublicacao = new DateTime(2014, 7, 10) }
                };

                dbContext.Recalls.AddRange(recalls);
                dbContext.SaveChanges();

                var execucoes = new[]
                {
                    new ExecucaoRecall { RecallId = recalls[3].Id, Chassi = "CHASSI123456789012", DataExecucao = new DateTime(2024, 5, 1), Concessionaria = "Horizon Motors" },
                    new ExecucaoRecall { RecallId = recalls[1].Id, Chassi = "CHASSI234567890123", DataExecucao = new DateTime(2020, 3, 25), Concessionaria = "Velocity Auto Group" },
                    new ExecucaoRecall { RecallId = recalls[4].Id, Chassi = "CHASSI234567890123", DataExecucao = new DateTime(2016, 10, 7), Concessionaria = "Velocity Auto Group" }
                };

                dbContext.ExecucoesRecalls.AddRange(execucoes);
                dbContext.SaveChanges();
            }
        }
    }
}