using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        try
        {
            return _context.Categorias.Include(c => c.Produtos).Where(c => c.CategoriaId < 5).ToList();
            
        }
        catch (Exception)
        {
            throw new Exception("Não foi possivel carregar as categorias....");
        }
       
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            var lista = _context.Categorias.ToList();
            if (lista is null)
            {
                return NotFound("Nenhuma categoria cadastrada...");
            }
            return lista;
        }
        catch (Exception)
        {
            throw new Exception("Houve um problema em buscar categorias, tente novamente mais tarde");
        }

        

    }

    [HttpGet("{id:int}",Name ="ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada...");
            }
            return Ok(categoria);
        }
        catch (Exception)
        {
            throw new Exception("Categoria não encontrada, tente novamente mais tarde...");
        }
        

    }


    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
            {
                return BadRequest("Categoria vazia...");
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            throw new Exception("Categoria não registrada, tente novamente mais tarde...");
        }

       
    }


    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }
        catch (Exception)
        {
            throw new Exception("Categoria não alterada, tente novamente mais tarde...");
        }
       
    }


    [HttpDelete("{id:int}")]

    public ActionResult Delete(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada... ");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
        catch (Exception)
        {
            throw new Exception("Categoria não removida, tente novamente mais tarde...");
        }
       
    }
}
