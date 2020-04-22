using API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Estacionamento> Estacionamentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DataContext(DbContextOptions<DataContext> options ) : base (options)
        {

        }
    }
}
