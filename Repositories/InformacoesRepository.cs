
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

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
    }
}

