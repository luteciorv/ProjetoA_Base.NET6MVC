using Aplicacao.Data;
using Aplicacao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly Context _context;
        public InstituicaoController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicoes.ToListAsync());
        }


        [HttpGet]
        public ActionResult Criar() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Instituicao novaInstituicao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Instituicoes.AddAsync(novaInstituicao);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError("", $"Não foi possível criar uma nova instituição. Exceção: {exc.Message}");                    
                }
            }

            return View(novaInstituicao);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null) return NotFound();

            var instituicao = await _context.Instituicoes.FindAsync(id);
            if (instituicao is null) return NotFound();

            return View(instituicao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Instituicao instituicaoAlterada)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Instituicoes.Update(instituicaoAlterada);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception exc)
                {
                    ModelState.AddModelError("", $"Não foi possível editar a instituição selecionada. Exceção: {exc.Message}");
                }
            }

            return View(instituicaoAlterada);
        }


        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            if(id is null) return NotFound();

            var instituicao = await _context.Instituicoes.FindAsync(id);
            if (instituicao is null) return NotFound();
            
            return View(instituicao);
        }


        [HttpGet]
        public async Task<IActionResult> Apagar(int? id)
        {
            if (id is null) return NotFound();

            var instituicao = await _context.Instituicoes.FindAsync(id);
            if (instituicao is null) return NotFound();

            return View(instituicao);
        }       

        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApagarConfirmado(int? id)
        {
            if (id is null) return NotFound();

            var instituicao = await _context.Instituicoes.FindAsync(id);
            if (instituicao is null) return NotFound();

            try
            {
                _context.Instituicoes.Remove(instituicao);
                await _context.SaveChangesAsync();

                TempData["Mensagem"] = $"A Instituição {instituicao.Nome} foi removida com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", $"Não foi possível apagar o departamento informado. Exceção: {exc.Message}");
                return View(instituicao);
            }
        }
    }
}
