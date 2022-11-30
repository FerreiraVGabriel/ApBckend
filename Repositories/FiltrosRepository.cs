
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public class FiltrosRepository : IFiltrosRepository
    {
         private readonly DataContext _context;

        public FiltrosRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Filtro filtro)
        {
            _context.Add(filtro);
            _context.SaveChanges();
        }

        public List<Filtro> Read()
        {
            return _context.Filtro.ToList();
        }
    }
}

