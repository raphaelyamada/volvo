using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.Cadastro.Controllers;
using Volvo.Cadastro.Data;
using Volvo.Cadastro.Models;
using Xunit;

namespace Volvo.Cadastro.Test.Repositories
{
    public class CadastroRepositoryTest: BaseRepositoryTest
    {
        [Fact]
        public async Task ObterCaminhoes_QuandoChamado_RetornaLista()
        {
            var caminhoes = await repository.ObterCaminhoes();

            Assert.Equal(3, caminhoes.ToList().Count);
            Assert.IsType<List<Caminhao>>(caminhoes);
        }

        [Fact]
        public async Task ObterCaminhaoPorId_QuandoEncontra_RetornaCaminhao()
        {
            int idPesquisado = 2;

            var caminhao = await repository.ObterCaminhaoPorId(idPesquisado);

            Assert.True(caminhao != null);
            Assert.Equal(idPesquisado, caminhao.IdCaminhao);
            Assert.IsType<Caminhao>(caminhao);
        }

        [Fact]
        public async Task ObterCaminhaoPorId_QuandoNaoEncontra_RetornaNull()
        {
            int idPesquisado = 5;

            var caminhao = await repository.ObterCaminhaoPorId(idPesquisado);

            Assert.True(caminhao == null);
        }

        [Fact]
        public void ObterModelos_QuandoChamado_RetornaLista()
        {
            var modelos = repository.ObterModelos();

            Assert.Equal(2, modelos.ToList().Count);
            Assert.IsType<List<Modelo>>(modelos);
        }

        [Fact]
        public async Task IncluirCaminhao_QuandoChamado_RetornaInt()
        {
            var caminhao = ObterCaminhaoParaIncluir();

            var retorno = await repository.IncluirCaminhao(caminhao);

            Assert.IsType<int>(retorno);
        }

        [Fact]
        public async Task AtualizarCaminhao_QuandoChamado_RetornaInt()
        {
            var caminhao = ObterCaminhaoParaAtualizar();

            var retorno = await repository.AtualizarCaminhao(caminhao);

            Assert.IsType<int>(retorno);
        }

        [Fact]
        public async Task DeletarCaminhao_QuandoChamado_RetornaInt()
        {
            int idCaminhaoParaDeletar = 2;

            var retorno = await repository.DeletarCaminhao(idCaminhaoParaDeletar);

            Assert.IsType<int>(retorno);
        }

        [Fact]
        public void CaminhaoExiste_QuandoExiste_RetornaTrue()
        {
            int idCaminhaoParaPesquisar = 1;

            var retorno = repository.CaminhaoExiste(idCaminhaoParaPesquisar);

            Assert.True(retorno);
        }

        [Fact]
        public void CaminhaoExiste_QuandoNaoExiste_RetornaFalse()
        {
            int idCaminhaoParaPesquisar = 10;

            var retorno = repository.CaminhaoExiste(idCaminhaoParaPesquisar);

            Assert.True(!retorno);
        }
    }
}
