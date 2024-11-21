using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context) : base(context)
    {
        
    }

   
}
