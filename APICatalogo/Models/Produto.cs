using APICatalogo.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatorio")]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 10000, ErrorMessage = "O preço tem que ser maior q um e menor que 10000")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }


    // Definindo que há uma coluna com categoriaId
    // e que tem como relacionamento q cada produto tem uma Categoria 
    public int CategoriaId { get; set; }

    [JsonIgnore]//ignora na hora da serealização e não mostra no POST ou PUT
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = Nome.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do produto deve ser maiuscula", new[]
                {
                    nameof(this.Nome),
                });
            }
        }

        if (this.Estoque <= 0)
        {
            yield return new ValidationResult("O estoque tem que ser maior que 0 ", new[]
               {
                    nameof(this.Estoque),
                });
        }
    }
}
