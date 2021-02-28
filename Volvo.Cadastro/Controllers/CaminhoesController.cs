using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Volvo.Cadastro.Data;
using Volvo.Cadastro.Models;
using Volvo.Cadastro.Repositories;

namespace Volvo.Cadastro.Controllers
{
    public class CaminhoesController : Controller
    {
        private readonly ICadastroRepository _cadastroRepository;

        public CaminhoesController(ICadastroRepository cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        // GET: Caminhoes
        public async Task<IActionResult> Index()
        {
            return View(await _cadastroRepository.ObterCaminhoes());
        }

        // GET: Caminhoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _cadastroRepository.ObterCaminhaoPorId((int)id);

            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // GET: Caminhoes/Create
        public IActionResult Create()
        {
            ViewData["ModeloIdModelo"] = new SelectList(_cadastroRepository.ObterModelos(), "IdModelo", "DescricaoModelo");
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
                await _cadastroRepository.IncluirCaminhao(caminhao);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloIdModelo"] = new SelectList(_cadastroRepository.ObterModelos(), "IdModelo", "IdModelo", caminhao.ModeloIdModelo);
            return View(caminhao);
        }

        // GET: Caminhoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _cadastroRepository.ObterCaminhaoPorId((int)id);
            if (caminhao == null)
            {
                return NotFound();
            }
            ViewData["ModeloIdModelo"] = new SelectList(_cadastroRepository.ObterModelos(), "IdModelo", "DescricaoModelo", caminhao.ModeloIdModelo);
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
                    await _cadastroRepository.AtualizarCaminhao(caminhao);
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
            ViewData["ModeloIdModelo"] = new SelectList(_cadastroRepository.ObterModelos(), "IdModelo", "IdModelo", caminhao.ModeloIdModelo);
            return View(caminhao);
        }

        // GET: Caminhoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _cadastroRepository.ObterCaminhaoPorId((int)id);

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
            await _cadastroRepository.DeletarCaminhao((int)id);
            return RedirectToAction(nameof(Index));
        }

        private bool CaminhaoExists(int id)
        {
            return _cadastroRepository.CaminhaoExiste(id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidaAno(int anoFabricacao, int anoModelo)
        {
            var anoMaximo = anoFabricacao + 1;

            if (anoModelo < anoFabricacao ||
                anoModelo > anoMaximo )
            {
                return Json($"Ano Modelo deve ser igual ou subsequente ao Ano de Fabricação.");
            }

            return Json(true);
        }
    }
}
