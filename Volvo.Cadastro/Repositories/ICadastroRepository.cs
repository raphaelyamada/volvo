﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Volvo.Cadastro.Models;

namespace Volvo.Cadastro.Repositories
{
    public interface ICadastroRepository
    {
        IEnumerable<Caminhao> ObterCaminhoes();
        Caminhao ObterCaminhaoPorId(int id);
        IEnumerable<Modelo> ObterModelos();
        Task<int> IncluirCaminhao(Caminhao caminhao);
        Task<int> AtualizarCaminhao(Caminhao caminhao);
        Task<int> DeletarCaminhao(int id);
        bool CaminhaoExiste(int id);
    }
}
