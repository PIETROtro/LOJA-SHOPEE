using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopeeMVC.Models;

namespace ShopeeMVC.Controllers;

public class ProdutoController : Controller
{
    private readonly DbShopeeContext _context;

    public ProdutoController(DbShopeeContext context) => _context = context;

    public async Task<IActionResult> Index()
        => View(await _context.Produtos.AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Codigo == id);
        return produto == null ? NotFound() : View(produto);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (!ModelState.IsValid) return View(produto);
        _context.Add(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var produto = await _context.Produtos.FindAsync(id);
        return produto == null ? NotFound() : View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Produto produto)
    {
        if (id != produto.Codigo) return NotFound();
        if (!ModelState.IsValid) return View(produto);
        _context.Update(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Codigo == id);
        return produto == null ? NotFound() : View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}