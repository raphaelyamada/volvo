using Volvo.Cadastro.Services;
using Volvo.Cadastro.Test.Repositories;

namespace Volvo.Cadastro.Test.Services
{
    public class BaseServiceTest : BaseRepositoryTest
    {
        public CadastroService service;

        public BaseServiceTest()
        {
            service = new CadastroService(repository);
        }
    }
}
