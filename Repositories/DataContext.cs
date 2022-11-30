using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}

        //DBSET
        public DbSet<Times> Times {get; set;}
        public DbSet<Pais> Pais {get; set;}
        public DbSet<Competicao> Competicao {get; set;}
        public DbSet<Mercados> Mercados {get; set;}
        public DbSet<Apostas> Apostas {get; set;}
        public DbSet<ApostasLive> ApostasLive {get; set;}
        public DbSet<TipoAposta> TipoAposta {get; set;}
        public DbSet<Filtro> Filtro {get; set;}
    }
}