using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volvo.Cadastro.Models;

namespace Volvo.Cadastro.Services
{
    public interface ICadastroService
    {
        IEnumerable<Caminhao> ObterCaminhoes();
        Caminhao ObterCaminhaoPorId(int? id);
        SelectList ObterModelosSelectList();
        SelectList ObterModelosSelectList(int id);
        Task<int> IncluirCaminhao(Caminhao caminhao);
        Task<int> AtualizarCaminhao(Caminhao caminhao);
        Task<int> DeletarCaminhao(int id);
        bool CaminhaoExiste(int id);
        bool ValidaAno(int anoFabricacao, int anoModelo);
    }
}
