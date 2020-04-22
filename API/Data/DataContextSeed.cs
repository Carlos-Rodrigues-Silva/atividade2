using API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContextSeed 
    {
        public static async Task SeedAsync(DataContext dataContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!dataContext.Estacionamentos.Any())
                {
                    var dadosEstacionamentos = File.ReadAllText("../API/Data/SeedData/Estacionamento.json");

                    var estacionamentos = JsonSerializer.Deserialize<List<Estacionamento>>(dadosEstacionamentos);

                    foreach(var item in estacionamentos)
                    {
                        dataContext.Estacionamentos.Add(item);
                    }

                    await dataContext.SaveChangesAsync();
                }

                if (!dataContext.Enderecos.Any())
                {
                    var dadosEndereco = File.ReadAllText("../API/Data/SeedData/Endereco.json");

                    var endereco = JsonSerializer.Deserialize<List<Endereco>>(dadosEndereco);

                    foreach (var item in endereco)
                    {
                        dataContext.Enderecos.Add(item);
                    }

                    await dataContext.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}
