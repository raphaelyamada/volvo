using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volvo.Cadastro.Data;
using Volvo.Cadastro.Models;
using Volvo.Cadastro.Services;

namespace Volvo.Cadastro.Test
{
    public class BaseServiceTest
    {
        public CadastroService service;
        public Mock<CadastroContext> contexto;

        public BaseServiceTest()
        {
            contexto = ObterContexto();
            service = new CadastroService(contexto.Object);
        }

        private DbSet<T> ObterDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        private Mock<CadastroContext> ObterContexto()
        {
            var listaCaminhao = ObterListaCaminhao();
            var listaModelo = ObterListaModelo();

            var mockDbSetCaminhao = ObterDbSet<Caminhao>(listaCaminhao);
            var mockDbSetModelo = ObterDbSet<Modelo>(listaModelo);

            var mockContext = new Mock<CadastroContext>();
            mockContext.Setup(c => c.Caminhoes).Returns(mockDbSetCaminhao);
            mockContext.Setup(c => c.Modelos).Returns(mockDbSetModelo);

            return mockContext;
        }

        private List<Caminhao> ObterListaCaminhao()
        {
            return new List<Caminhao>
            {
                new Caminhao { IdCaminhao = 1, Modelo = new Modelo { IdModelo = 2, DescricaoModelo = "FM"}, AnoFabricacao = 2012, AnoModelo = 2012 , ModeloIdModelo = 2},
                new Caminhao { IdCaminhao = 2, Modelo = new Modelo { IdModelo = 1, DescricaoModelo = "FH"}, AnoFabricacao = 2012, AnoModelo = 2013 , ModeloIdModelo = 2},
                new Caminhao { IdCaminhao = 3, Modelo = new Modelo { IdModelo = 2, DescricaoModelo = "FM"}, AnoFabricacao = 2020, AnoModelo = 2021 , ModeloIdModelo = 2}
            };
        }

        private List<Modelo> ObterListaModelo()
        {
            return new List<Modelo>
            {
                new Modelo { IdModelo = 1, DescricaoModelo = "FH"},
                new Modelo { IdModelo = 2, DescricaoModelo = "FM"}
            };
        }

        public Caminhao ObterCaminhaoParaIncluir()
        {
            return new Caminhao { Modelo = new Modelo { IdModelo = 2, DescricaoModelo = "FM" }, AnoFabricacao = 2019, AnoModelo = 2019, ModeloIdModelo = 2 };
        }

        public Caminhao ObterCaminhaoParaAtualizar()
        {
            return new Caminhao { IdCaminhao = 2, Modelo = new Modelo { IdModelo = 1, DescricaoModelo = "FH" }, AnoFabricacao = 2013, AnoModelo = 2013, ModeloIdModelo = 2 };
        }
    }
}
