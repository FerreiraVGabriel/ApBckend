
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public interface IPaisRepository
    {
        List<Pais> Read();

        void Create (Pais pais);
    }
    public class PaisRepository : IPaisRepository
    {
         private readonly DataContext _context;

        public PaisRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Pais pais)
        {
            _context.Add(pais);
            _context.SaveChanges();
        }

        public List<Pais> Read()
        {
            return _context.Pais.ToList();
        }
    }
}

