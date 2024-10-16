using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

public class AppDbContext : DbContext
{

    // Configurar as opções do contexto
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {    
    }

    //Mapeamento das tabelas do banco de dados
    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Produto>? Produtos { get; set; }

}
