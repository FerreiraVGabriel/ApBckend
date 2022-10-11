
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public interface ITipoApostaRepository
    {
        List<TipoAposta> Read();

        void Create (TipoAposta tipoAposta);
    }
    public class TipoApostaRepository : ITipoApostaRepository
    {
         private readonly DataContext _context;

        public TipoApostaRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(TipoAposta tipoAposta)
        {
            _context.Add(tipoAposta);
            _context.SaveChanges();
        }

        public List<TipoAposta> Read()
        {
            return _context.TipoAposta.ToList();
        }
    }
}

