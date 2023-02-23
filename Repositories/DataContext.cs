using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}

         #region MODELCREATING
                protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                    modelBuilder.Entity<Times>()
                    .HasOne(t => t.Pais)
                    .WithMany()
                    .HasForeignKey(t => t.Pais_id); 

                     modelBuilder.Entity<Apostas>()
                    .HasOne(a => a.Competicao)
                    .WithMany()
                    .HasForeignKey(a => a.Competicao_id);

                    modelBuilder.Entity<Apostas>()
                    .HasOne(a => a.Mercados )
                    .WithMany()
                    .HasForeignKey(a => a.Mercados_id);

                    modelBuilder.Entity<Apostas>()
                    .HasOne(a => a.TimeMandante)
                    .WithMany()
                    .HasForeignKey(a => a.Mandante_id);

                    modelBuilder.Entity<Apostas>()
                    .HasOne(a => a.TimeVisitante )
                    .WithMany()
                    .HasForeignKey(a => a.Visitante_id);
                }
        #endregion

        //DBSET
        public DbSet<Times> Times {get; set;}

        public DbSet<Pais> Pais {get; set;}
        public DbSet<Competicao> Competicao {get; set;}
        public DbSet<Mercados> Mercados {get; set;}
        public DbSet<Apostas> Apostas {get; set;}
        
        public DbSet<ApostasLive> ApostasLive {get; set;}
        public DbSet<TipoAposta> TipoAposta {get; set;}
        public DbSet<Filtro> Filtro {get; set;}
        public DbSet<Informacoes> Informacoes {get; set;}
    }
}