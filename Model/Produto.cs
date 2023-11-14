using System.ComponentModel.DataAnnotations;

namespace ApiProduto.Model;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatório")]
    public string? Nome { get; set; } = null;
    
    [Required(ErrorMessage = "Preço é um campo obrigatório")]
    public decimal? Preco { get; set; } = null;

}