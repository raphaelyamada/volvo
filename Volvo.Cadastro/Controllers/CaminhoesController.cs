using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volvo.Cadastro.Models;
using Volvo.Cadastro.Services;

namespace Volvo.Cadastro.Controllers
{
    public class CaminhoesController : Controller
    {
        private readonly ICadastroService _cadastroService;

        public CaminhoesController(ICadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        // GET: Caminhoes
        public IActionResult Index()
        {
            return View(_cadastroService.ObterCaminhoes());
        }

        // GET: Caminhoes/Details/5
        public IActionResult Details(int? id)
        {
            var caminhao = _cadastroService.ObterCaminhaoPorId(id);

            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // GET: Caminhoes/Create
        public IActionResult Create()
        {
            ViewData["ModeloIdModelo"] = _cadastroService.ObterModelosSelectList();
            return View();
        }

        // POST: Caminhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCaminhao,ModeloIdModelo,AnoFabricacao,AnoModelo")] Caminhao caminhao)
        {
            if (ModelState.IsValid)
            {
                await _cadastroService.IncluirCaminhao(caminhao);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloIdModelo"] = _cadastroService.ObterModelosSelectList(caminhao.ModeloIdModelo);
            return View(caminhao);
        }

        // GET: Caminhoes/Edit/5
        public IActionResult Edit(int? id)
        {
            var caminhao = _cadastroService.ObterCaminhaoPorId(id);
            
            if (caminhao == null)
            {
                return NotFound();
            }

            ViewData["ModeloIdModelo"] = _cadastroService.ObterModelosSelectList(caminhao.ModeloIdModelo);
            return View(caminhao);
        }

        // POST: Caminhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCaminhao,ModeloIdModelo,AnoFabricacao,AnoModelo")] Caminhao caminhao)
        {
            if (id != caminhao.IdCaminhao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cadastroService.AtualizarCaminhao(caminhao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminhaoExists(caminhao.IdCaminhao))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloIdModelo"] = _cadastroService.ObterModelosSelectList(caminhao.ModeloIdModelo);
            return View(caminhao);
        }

        // GET: Caminhoes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = _cadastroService.ObterCaminhaoPorId(id);

            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // POST: Caminhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cadastroService.DeletarCaminhao((int)id);
            return RedirectToAction(nameof(Index));
        }

        private bool CaminhaoExists(int id)
        {
            return _cadastroService.CaminhaoExiste(id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidaAno(int anoFabricacao, int anoModelo)
        {
            if(!_cadastroService.ValidaAno(anoFabricacao, anoModelo))
            {
                return Json($"Ano Modelo deve ser igual ou subsequente ao Ano de Fabricação.");
            }

            return Json(true);
        }
    }
}
