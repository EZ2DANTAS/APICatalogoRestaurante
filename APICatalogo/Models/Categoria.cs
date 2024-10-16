using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria

{
    //Instanciando o Colletion
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    // O entity framework entende que se tiver id no final é uma chave primaria em um banco de dados
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(80)]
    public string? ImagemUrl { get; set; }

    // Categoria pode ter uma coleção de objetos de produto
    public ICollection<Produto>? Produtos { get; set; }

    
   
}
