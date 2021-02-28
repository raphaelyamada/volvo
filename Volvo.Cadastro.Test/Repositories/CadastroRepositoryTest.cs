using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Cadastro.Models;
using Xunit;

namespace Volvo.Cadastro.Test.Repositories
{
    public class CadastroRepositoryTest: BaseRepositoryTest
    {
        [Fact]
        public void ObterCaminhoes_QuandoChamado_RetornaLista()
        {
            var caminhoes = repository.ObterCaminhoes();

            Assert.Equal(3, caminhoes.ToList().Count);
            Assert.IsType<List<Caminhao>>(caminhoes);
        }

        [Fact]
        public void ObterCaminhaoPorId_QuandoEncontra_RetornaCaminhao()
        {
            int idPesquisado = 2;

            var caminhao = repository.ObterCaminhaoPorId(idPesquisado);

            Assert.True(caminhao != null);
            Assert.Equal(idPesquisado, caminhao.IdCaminhao);
            Assert.IsType<Caminhao>(caminhao);
        }

        [Fact]
        public void ObterCaminhaoPorId_QuandoNaoEncontra_RetornaNull()
        {
            int idPesquisado = 5;

            var caminhao = repository.ObterCaminhaoPorId(idPesquisado);

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
