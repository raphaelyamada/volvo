using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Cadastro.Data;
using Volvo.Cadastro.Models;

namespace Volvo.Cadastro.Repositories
{
    public class CadastroRepository: ICadastroRepository
    {
        CadastroContext _context;

        public CadastroRepository(CadastroContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Caminhao>> ObterCaminhoes()
        {
            return _context.Caminhoes.Include(c => c.Modelo).ToList();
        }

        public async Task<Caminhao> ObterCaminhaoPorId(int id)
        {
            return _context.Caminhoes
                .Include(c => c.Modelo)
                .FirstOrDefault(m => m.IdCaminhao == id);
        }

        public IEnumerable<Modelo> ObterModelos()
        {
            return _context.Modelos.ToList();
        }

        public async Task<int> IncluirCaminhao(Caminhao caminhao)
        {
            _context.Add(caminhao);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AtualizarCaminhao(Caminhao caminhao)
        {
            _context.Update(caminhao);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletarCaminhao(int id)
        {
            var caminhao = await _context.Caminhoes.FindAsync(id);
            _context.Caminhoes.Remove(caminhao);
            return await _context.SaveChangesAsync();
        }

        public bool CaminhaoExiste(int id)
        {
            return _context.Caminhoes.Any(e => e.IdCaminhao == id);
        }
    }
}
