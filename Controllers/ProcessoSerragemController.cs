using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMamoreGranito.Data;
using SistemaMamoreGranito.Models;
using System.Diagnostics;

namespace SistemaMamoreGranito.Controllers
{
    public class ProcessoSerragemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProcessoSerragemController> _logger;

        public ProcessoSerragemController(ApplicationDbContext context, ILogger<ProcessoSerragemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: ProcessoSerragem
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Listando processos de serragem");
                var processos = await _context.ProcessosSerragem
                    .Include(p => p.Bloco)
                    .OrderByDescending(p => p.DataProcesso)
                    .ToListAsync();
                return View(processos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar processos de serragem");
                TempData["Erro"] = "Erro ao carregar a lista de processos. Por favor, tente novamente.";
                return View(new List<ProcessoSerragem>());
            }
        }

        // GET: ProcessoSerragem/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                _logger.LogInformation("Carregando formulário de criação de processo de serragem");
                ViewBag.BlocosDisponiveis = await _context.Blocos
                    .Where(b => b.Disponivel)
                    .ToListAsync();
                _logger.LogInformation($"Blocos disponíveis carregados: {ViewBag.BlocosDisponiveis.Count}");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar formulário de criação");
                TempData["Erro"] = "Erro ao carregar o formulário. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProcessoSerragem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcessoSerragem processoSerragem)
        {
            try
            {
                _logger.LogInformation("Iniciando criação de processo de serragem");
                _logger.LogInformation($"Dados recebidos: BlocoId={processoSerragem.BlocoId}, Espessura={processoSerragem.EspessuraChapa}, Quantidade={processoSerragem.QuantidadeChapas}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState inválido");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        _logger.LogWarning($"Erro de validação: {error.ErrorMessage}");
                    }
                    ViewBag.BlocosDisponiveis = await _context.Blocos
                        .Where(b => b.Disponivel)
                        .ToListAsync();
                    return View(processoSerragem);
                }

                _logger.LogInformation("Buscando bloco no banco de dados");
                var bloco = await _context.Blocos
                    .FirstOrDefaultAsync(b => b.Id == processoSerragem.BlocoId);

                if (bloco == null)
                {
                    _logger.LogError($"Bloco não encontrado: ID={processoSerragem.BlocoId}");
                    TempData["Erro"] = "Bloco não encontrado.";
                    ViewBag.BlocosDisponiveis = await _context.Blocos
                        .Where(b => b.Disponivel)
                        .ToListAsync();
                    return View(processoSerragem);
                }

                _logger.LogInformation($"Bloco encontrado: ID={bloco.Id}, NomeMaterial={bloco.NomeMaterial}");

                if (!bloco.Disponivel)
                {
                    _logger.LogWarning($"Bloco não está disponível: ID={bloco.Id}");
                    TempData["Erro"] = "Este bloco não está disponível para serragem.";
                    ViewBag.BlocosDisponiveis = await _context.Blocos
                        .Where(b => b.Disponivel)
                        .ToListAsync();
                    return View(processoSerragem);
                }

                _logger.LogInformation("Atribuindo bloco ao processo");
                processoSerragem.Bloco = bloco;
                processoSerragem.DataProcesso = DateTime.Now;

                _logger.LogInformation("Calculando volume total");
                var volumeTotal = processoSerragem.VolumeTotalChapas;
                _logger.LogInformation($"Volume total calculado: {volumeTotal}");

                if (volumeTotal > processoSerragem.VolumeBloco)
                {
                    _logger.LogWarning($"Volume excede o disponível: {volumeTotal} > {processoSerragem.VolumeBloco}");
                    TempData["Erro"] = "O volume total das chapas excede o volume do bloco.";
                    ViewBag.BlocosDisponiveis = await _context.Blocos
                        .Where(b => b.Disponivel)
                        .ToListAsync();
                    return View(processoSerragem);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _logger.LogInformation("Iniciando salvamento no banco de dados");
                    _context.Add(processoSerragem);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Criando chapas");
                    for (int i = 0; i < processoSerragem.QuantidadeChapas; i++)
                    {
                        var chapa = new Chapa
                        {
                            NomeMaterial = bloco.NomeMaterial,
                            TipoMaterial = bloco.TipoMaterial,
                            Espessura = processoSerragem.EspessuraChapa,
                            Altura = bloco.Altura,
                            Largura = bloco.Largura,
                            Peso = processoSerragem.PesoTotalChapas / processoSerragem.QuantidadeChapas,
                            BlocoId = bloco.Id,
                            DataEntrada = DateTime.Now,
                            Disponivel = true,
                            Observacoes = $"Gerada do processo de serragem #{processoSerragem.Id} - {processoSerragem.Observacoes}"
                        };
                        _context.Chapas.Add(chapa);
                    }

                    _logger.LogInformation("Atualizando disponibilidade do bloco");
                    bloco.Disponivel = false;
                    _context.Update(bloco);

                    _logger.LogInformation("Salvando alterações no banco de dados");
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    _logger.LogInformation("Processo de serragem criado com sucesso");
                    TempData["Sucesso"] = "Processo de serragem criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro durante a transação");
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar processo de serragem");
                TempData["Erro"] = "Ocorreu um erro ao criar o processo de serragem. Por favor, tente novamente.";
                ViewBag.BlocosDisponiveis = await _context.Blocos
                    .Where(b => b.Disponivel)
                    .ToListAsync();
                return View(processoSerragem);
            }
        }

        // GET: ProcessoSerragem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var processo = await _context.ProcessosSerragem
                    .Include(p => p.Bloco)
                    .FirstOrDefaultAsync(m => m.Id == id);
                    
                if (processo == null)
                {
                    return NotFound();
                }

                return View(processo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar detalhes do processo {Id}", id);
                TempData["Erro"] = "Erro ao carregar os detalhes do processo. Por favor, tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 