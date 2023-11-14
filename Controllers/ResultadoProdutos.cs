using ApiProduto.Model;

namespace ApiProduto.Controllers;

public class ResultadoProdutos
{
    public int Totals { get; set; }
    public IEnumerable<Produto>? Items { get; set; } = null;
}