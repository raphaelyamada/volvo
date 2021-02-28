using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Cadastro.Models;
using Volvo.Cadastro.Repositories;

namespace Volvo.Cadastro.Services
{
    public class CadastroService : ICadastroService
    {
        private readonly ICadastroRepository _cadastroRepository;

        public CadastroService(ICadastroRepository cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        public async Task<IEnumerable<Caminhao>> ObterCaminhoes()
        {
            return await _cadastroRepository.ObterCaminhoes();
        }

        public async Task<Caminhao> ObterCaminhaoPorId(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _cadastroRepository.ObterCaminhaoPorId((int)id);
        }

        public SelectList ObterModelosSelectList()
        {
            return new SelectList(ObterModelos(), "IdModelo", "DescricaoModelo");
        }

        public SelectList ObterModelosSelectList(int id)
        {
            return new SelectList(ObterModelos(), "IdModelo", "DescricaoModelo", id);
        }

        public async Task<int> IncluirCaminhao(Caminhao caminhao)
        {
            return await _cadastroRepository.IncluirCaminhao(caminhao);
        }

        public async Task<int> AtualizarCaminhao(Caminhao caminhao)
        {
            return await _cadastroRepository.AtualizarCaminhao(caminhao);
        }

        public async Task<int> DeletarCaminhao(int id)
        {
            return await _cadastroRepository.DeletarCaminhao(id);
        }

        public bool CaminhaoExiste(int id)
        {
            return _cadastroRepository.CaminhaoExiste(id);
        }

        public bool ValidaAno(int anoFabricacao, int anoModelo)
        {
            var anoMaximo = anoFabricacao + 1;

            if (anoModelo < anoFabricacao ||
                anoModelo > anoMaximo)
            {
                return false;
            }

            return true;
        }

        private IEnumerable<Modelo> ObterModelos()
        {
            return _cadastroRepository.ObterModelos();
        }
    }
}
