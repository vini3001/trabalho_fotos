using BuscaCEP.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trabalho_fotos.Data;
using trabalho_fotos.Models;

namespace trabalho_fotos.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CorreiosService _correiosService;

        public ClienteController(ApplicationDbContext context, CorreiosService correiosService)
        {
            _context = context;
            _correiosService = correiosService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente client, IList<IFormFile> Img)
        {
            IFormFile uploadedImg = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            System.Diagnostics.Debug.WriteLine("Suba");
            System.Diagnostics.Debug.WriteLine(client.Photo);
            if(Img.Count > 0)
            {
                uploadedImg.OpenReadStream().CopyTo(ms);
                client.Photo = ms.ToArray();
            }

            _context.Clientes.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var client = await _context.Clientes.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Cliente client, IList<IFormFile> Img)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            IFormFile uploadedImg = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();

            if (Img.Count > 0)
            {
                uploadedImg.OpenReadStream().CopyTo(ms);
                client.Photo = ms.ToArray();
            }

            _context.Clientes.Update(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var client = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entidade'ApplicationDbContext.Clientes' não encontrada.");
            }
            var client = await _context.Clientes.FindAsync(id);
            if (client != null)
            {
                _context.Clientes.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool clientExists(int? id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> BuscarEnderecoPorCep(string cep)
        {
            Endereco? endereco = await _correiosService.BuscarEnderecoPorCep(cep);
            if (endereco == null)
            {
                TempData["Mensagem"] = "CEP <strong> não </strong> encontrado";
            }

            ViewBag.endereco = endereco;
            return View("Create");
        }
    }
}
