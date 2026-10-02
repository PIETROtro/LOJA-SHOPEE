using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopeeMVC.Models;
using System.Text.Json;

namespace ShopeeMVC.Controllers;

public class CarrinhoController : Controller
{
    private readonly DbShopeeContext _context;

    public CarrinhoController(DbShopeeContext context) => _context = context;

    private List<CarrinhoItem> ObterCarrinho()
    {
        var json = HttpContext.Session.GetString("Carrinho");
        if (string.IsNullOrEmpty(json))
            return new List<CarrinhoItem>();

        return JsonSerializer.Deserialize<List<CarrinhoItem>>(json)
               ?? new List<CarrinhoItem>();
    }

    private void SalvarCarrinho(List<CarrinhoItem> carrinho)
    {
        HttpContext.Session.SetString("Carrinho", JsonSerializer.Serialize(carrinho));
    }

    public IActionResult Index()
    {
        var carrinho = ObterCarrinho();
        return View(carrinho);
    }

    public async Task<IActionResult> Adicionar(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Codigo == id);
        if (produto == null) return NotFound();

        var carrinho = ObterCarrinho();
        var item = carrinho.FirstOrDefault(x => x.ProdutoId == produto.Codigo);

        if (item == null)
        {
            carrinho.Add(new CarrinhoItem
            {
                ProdutoId = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quantidade = 1
            });
        }
        else
        {
            item.Quantidade++;
        }

        SalvarCarrinho(carrinho);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remover(int id)
    {
        var carrinho = ObterCarrinho();
        var item = carrinho.FirstOrDefault(x => x.ProdutoId == id);
        if (item != null) carrinho.Remove(item);
        SalvarCarrinho(carrinho);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Limpar()
    {
        HttpContext.Session.Remove("Carrinho");
        return RedirectToAction(nameof(Index));
    }
}