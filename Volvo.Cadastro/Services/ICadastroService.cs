using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Cadastro.Models;

namespace Volvo.Cadastro.Services
{
    public interface ICadastroService
    {
        Task<IEnumerable<Caminhao>> ObterCaminhoes();
        Task<Caminhao> ObterCaminhaoPorId(int id);
        IEnumerable<Modelo> ObterModelos();
        Task<int> IncluirCaminhao(Caminhao caminhao);
        Task<int> AtualizarCaminhao(Caminhao caminhao);
        Task<int> DeletarCaminhao(int id);
        bool CaminhaoExiste(int id);
    }
}
