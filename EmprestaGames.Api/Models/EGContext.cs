using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmprestaGames.Api.Models
{
    public class EGContext : DbContext
    {
        public IConfiguration Configuration { get; }

        private const string ConnectionString = "Data Source=BarDG.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration["ConexaoSql:SqlConnectionString"];
                options.UseSqlServer(connectionString);
            }
        }

        public EGContext(DbContextOptions options) : base(options)
        {

        }

        public EGContext() : base()
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<PessoaXJogo> PessoasXJogos { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
    }
}