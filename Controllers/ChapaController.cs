using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMamoreGranito.Data;
using SistemaMamoreGranito.Models;

namespace SistemaMamoreGranito.Controllers
{
    public class ChapaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChapaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chapa
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chapas
                .Include(c => c.BlocoOrigem)
                .ToListAsync());
        }

        // GET: Chapa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapa = await _context.Chapas
                .Include(c => c.BlocoOrigem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapa == null)
            {
                return NotFound();
            }

            return View(chapa);
        }

        // GET: Chapa/Create
        public IActionResult Create()
        {
            ViewBag.BlocosDisponiveis = _context.Blocos
                .Where(b => b.Disponivel)
                .ToList();
            return View();
        }

        // POST: Chapa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeMaterial,TipoMaterial,Espessura,Altura,Largura,Peso,Observacoes,DataEntrada,Disponivel,BlocoId,Valor")] Chapa chapa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verifica se o bloco existe e está disponível
                    var bloco = await _context.Blocos.FindAsync(chapa.BlocoId);
                    if (bloco == null || !bloco.Disponivel)
                    {
                        ModelState.AddModelError("BlocoId", "O bloco selecionado não está disponível.");
                        ViewBag.BlocosDisponiveis = _context.Blocos.Where(b => b.Disponivel).ToList();
                        return View(chapa);
                    }

                    // Se a data de entrada não foi fornecida, usa a data atual
                    if (chapa.DataEntrada == default)
                    {
                        chapa.DataEntrada = DateTime.Now;
                    }

                    _context.Add(chapa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao salvar a chapa: {ex.Message}. Por favor, tente novamente.");
            }

            ViewBag.BlocosDisponiveis = _context.Blocos.Where(b => b.Disponivel).ToList();
            return View(chapa);
        }

        // GET: Chapa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapa = await _context.Chapas.FindAsync(id);
            if (chapa == null)
            {
                return NotFound();
            }

            ViewBag.BlocosDisponiveis = _context.Blocos
                .Where(b => b.Disponivel || b.Id == chapa.BlocoId)
                .ToList();
            return View(chapa);
        }

        // POST: Chapa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeMaterial,TipoMaterial,Espessura,Altura,Largura,Peso,Observacoes,DataEntrada,Disponivel,BlocoId,Valor")] Chapa chapa)
        {
            if (id != chapa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verifica se o bloco existe e está disponível
                    var bloco = await _context.Blocos.FindAsync(chapa.BlocoId);
                    if (bloco == null || (!bloco.Disponivel && bloco.Id != chapa.BlocoId))
                    {
                        ModelState.AddModelError("BlocoId", "O bloco selecionado não está disponível.");
                        ViewBag.BlocosDisponiveis = _context.Blocos
                            .Where(b => b.Disponivel || b.Id == chapa.BlocoId)
                            .ToList();
                        return View(chapa);
                    }

                    _context.Update(chapa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapaExists(chapa.Id))
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

            ViewBag.BlocosDisponiveis = _context.Blocos
                .Where(b => b.Disponivel || b.Id == chapa.BlocoId)
                .ToList();
            return View(chapa);
        }

        // GET: Chapa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapa = await _context.Chapas
                .Include(c => c.BlocoOrigem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapa == null)
            {
                return NotFound();
            }

            return View(chapa);
        }

        // POST: Chapa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chapa = await _context.Chapas.FindAsync(id);
            if (chapa != null)
            {
                _context.Chapas.Remove(chapa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChapaExists(int id)
        {
            return _context.Chapas.Any(e => e.Id == id);
        }
    }
} 