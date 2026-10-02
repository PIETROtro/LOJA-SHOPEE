using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeMVC.Models;

[Table("Produto")]
public partial class Produto
{
    [Key]
    public int Codigo { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    public int Estoque { get; set; }
}