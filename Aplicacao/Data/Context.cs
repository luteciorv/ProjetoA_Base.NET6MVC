using Aplicacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }              

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
    }
}
