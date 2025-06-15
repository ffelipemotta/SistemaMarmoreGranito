using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMamoreGranito.Data;
using SistemaMamoreGranito.Models;

namespace SistemaMamoreGranito.Controllers
{
    public class BlocoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BlocoController> _logger;

        public BlocoController(ApplicationDbContext context, ILogger<BlocoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Bloco
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blocos.ToListAsync());
        }

        // GET: Bloco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Blocos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloco == null)
            {
                return NotFound();
            }

            return View(bloco);
        }

        // GET: Bloco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bloco/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeMaterial,TipoMaterial,Altura,Largura,Comprimento,Peso,PedreiraOrigem,NumeroNotaFiscal,ValorCompra,Observacoes")] Bloco bloco)
        {
            try
            {
                _logger.LogInformation("Iniciando criação de novo bloco");
                
                if (ModelState.IsValid)
                {
                    bloco.DataEntrada = DateTime.Now;
                    bloco.Disponivel = true;
                    
                    _logger.LogInformation("Adicionando bloco ao contexto");
                    _context.Add(bloco);
                    
                    _logger.LogInformation("Salvando alterações no banco de dados");
                    await _context.SaveChangesAsync();
                    
                    _logger.LogInformation("Bloco criado com sucesso");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("ModelState inválido: {Errors}", 
                        string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar bloco");
                ModelState.AddModelError("", "Ocorreu um erro ao salvar o bloco. Por favor, tente novamente.");
            }
            
            return View(bloco);
        }

        // GET: Bloco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Blocos.FindAsync(id);
            if (bloco == null)
            {
                return NotFound();
            }
            return View(bloco);
        }

        // POST: Bloco/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeMaterial,TipoMaterial,Quantidade,DataEntrada")] Bloco bloco)
        {
            if (id != bloco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlocoExists(bloco.Id))
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
            return View(bloco);
        }

        // GET: Bloco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Blocos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloco == null)
            {
                return NotFound();
            }

            return View(bloco);
        }

        // POST: Bloco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloco = await _context.Blocos.FindAsync(id);
            if (bloco != null)
            {
                _context.Blocos.Remove(bloco);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlocoExists(int id)
        {
            return _context.Blocos.Any(e => e.Id == id);
        }
    }
} 