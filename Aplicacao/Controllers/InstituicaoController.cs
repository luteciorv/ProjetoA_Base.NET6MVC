using Aplicacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacao.Controllers
{
    public class InstituicaoController : Controller
    {
        private static IList<Instituicao> _instituicoes = new List<Instituicao>()
        {
            new Instituicao()
            {
                Id = 1,
                Nome = "UniParaná",
                Endereco = "Paraná"
            },
            new Instituicao()
            {
                Id = 2,
                Nome = "UniSanta",
                Endereco = "Santa Catarina"
            },
            new Instituicao()
            {
                Id = 3,
                Nome = "UniSãoPaulo",
                Endereco = "São Paulo"
            },
            new Instituicao()
            {
                Id = 4,
                Nome = "UniGrandeSul",
                Endereco = "Rio Grande do Sul"
            },
            new Instituicao()
            {
                Id = 5,
                Nome = "UniCarioca",
                Endereco = "Rio de Janeiro"
            },
        };


        public IActionResult Index()
        {
            return View(_instituicoes);
        }


        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Instituicao novaInstituicao)
        {
            novaInstituicao.Id = _instituicoes.Max(I => I.Id) + 1;
            _instituicoes.Add(novaInstituicao);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Editar(int id)
        {
            return View(_instituicoes.FirstOrDefault(I => I.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Instituicao instituicaoAlterada)
        {
            int posicao = _instituicoes.IndexOf(_instituicoes.First(I => I.Id == instituicaoAlterada.Id));
            _instituicoes[posicao] = instituicaoAlterada;            

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Detalhes(int id)
        {
            return View(_instituicoes.First(I => I.Id == id));
        }


        [HttpGet]
        public ActionResult Apagar(int id)
        {
            return View(_instituicoes.First(I => I.Id == id));
        }

        [HttpPost]
        public ActionResult Apagar(Instituicao instituicao)
        {
            _instituicoes.Remove(_instituicoes.First(I => I.Id == instituicao.Id));

            return RedirectToAction("Index");
        }
    }
}
