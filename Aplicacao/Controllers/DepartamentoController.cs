using Aplicacao.Data;
using Aplicacao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly Context _context;

        public DepartamentoController(Context context) => _context = context;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.OrderBy(D => D.Nome).ToListAsync());
        }


        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Nome")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException exc)
            {
                ModelState.AddModelError("", $"Não foi possível criar um novo departamento. Exceção: {exc.Message}.");
            }

            return View(departamento);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null) return NotFound();

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento is null) return NotFound();

            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Departamento departamentoAlterado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Departamentos.Update(departamentoAlterado);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException exc)
                {
                    bool existeDepartamento = await _context.Departamentos.AnyAsync(D => D.Id == departamentoAlterado.Id);
                    string mensagemErro = existeDepartamento ? "Não foi possível alterar os dados do departamento." : "O departamento informado não foi encontrado.";
                    mensagemErro += $" Exceção: {exc.Message}.";

                    ModelState.AddModelError("", mensagemErro);
                }
            }

            return View(departamentoAlterado);
        }


        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id is null) return NotFound();

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento is null) return NotFound();

            return View(departamento);
        }


        [HttpGet]
        public async Task<IActionResult> Apagar(int? id)
        {
            if (id is null) return NotFound();

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento is null) return NotFound();

            return View(departamento);
        }

        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApagarConfirmado(int? id)
        {
            if(id is null) return NotFound(); 
           
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento is null) return NotFound();

            try
            {                
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {                
                ModelState.AddModelError("", $"Não foi possível apagar a instituição informada. Exceção: {exc.Message}");
                return View(departamento);
            }
        }
    }
}
