using ApiProduto.Context;
using ApiProduto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProduto.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoDbContext _context;

    public ProdutosController(ProdutoDbContext context)
    {
        _context = context;
    }
    
    // apiv1/prudutos <- get all
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultadoProdutos>>> GetProdutos()
    {
        IEnumerable<Produto> produtos = await _context.Produtos.ToListAsync();

        return Ok(
            new ResultadoProdutos
            {
                Totals = produtos.Count(),
                Items = produtos
            }
        );
    }
    
    // apiv1/produtos/{id} <- get one
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        return produto;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Produto>>> PostProdutos(List<Produto>? produtos)
    {
        if (produtos == null || !produtos.Any())
        {
            return BadRequest("A lista de produtos está vazia ou nula.");
        }
        _context.Produtos.AddRange(produtos);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return CreatedAtAction(nameof(GetProdutos), produtos);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, Produto produtoAtualizado)
    {
        if (id != produtoAtualizado.Id)
        {
            return BadRequest();
        }

        _context.Entry(produtoAtualizado).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Produto>> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        try
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok();
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }
}