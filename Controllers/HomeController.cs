using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaMamoreGranito.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaMamoreGranito.Data;

namespace SistemaMamoreGranito.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction(nameof(Dashboard));
        }
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Dashboard()
    {
        try
        {
            ViewBag.BlocosDisponiveis = await _context.Blocos.CountAsync(b => b.Disponivel);
            ViewBag.ChapasEmEstoque = await _context.Chapas.CountAsync(c => c.Disponivel);
            ViewBag.ProcessosSerragem = await _context.ProcessosSerragem.CountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar estatísticas do dashboard");
            ViewBag.BlocosDisponiveis = 0;
            ViewBag.ChapasEmEstoque = 0;
            ViewBag.ProcessosSerragem = 0;
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
