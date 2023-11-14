using ApiProduto.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProduto.Context;

public class ProdutoDbContext : DbContext
{
    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Produto> Produtos { get; set; }
}