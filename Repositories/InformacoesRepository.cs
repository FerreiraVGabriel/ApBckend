
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;

namespace ProjetoGabrielAPI.Repositories
{
    public class InformacoesRepository : IInformacoesRepository
    {
         private readonly DataContext _context;

        public InformacoesRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Informacoes informacoes)
        {
            _context.Add(informacoes);
            _context.SaveChanges();
        }

        public List<Informacoes> Read()
        {
            return _context.Informacoes.ToList();
        }

        public Informacoes Read(int mes, int ano)
        {
            return _context.Informacoes.Where(x=> x.mes == mes && x.ano == ano).First();
        }

        public void Update(Informacoes informacoes)
        {
            _context.Entry(informacoes).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Informacoes GetInformacoesCompletaMesAno(Filtro filtro, List<Apostas> listApostas){
            Informacoes informacoes = Read(filtro.Mes, filtro.Ano);
            informacoes.pl = listApostas.Sum(x => x.PL);
            informacoes.roi = UtilsProject.retornaRoiStake(informacoes.inicio, informacoes.pl);
            informacoes.final = informacoes.inicio + informacoes.pl;
            Update(informacoes);
            return informacoes;
        }
    }
}

