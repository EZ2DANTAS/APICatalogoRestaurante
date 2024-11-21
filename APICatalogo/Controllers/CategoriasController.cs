using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using APICatalogo.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;
    private readonly ILogger _logger;
    public CategoriasController(IRepository<Categoria> repository, ILogger<CategoriasController> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categorias = _repository.Get(c => c.CategoriaId == id);

        if (categorias is null)
        {
            _logger.LogWarning($"Categoria com id {id} não encontrado...");
            return NotFound($"Categoria com id {id} não encontrado...");
        }
        return Ok(categorias);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        var categorias = _repository.GetAll();
        return Ok(categorias);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning("Dados invalidos...");
            return BadRequest("Categoria vazia...");
        }

        var categoriaCriada = _repository.Create(categoria);
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {

        if (id != categoria.CategoriaId)
        {
            _logger.LogWarning("Dados invalidos...");
            return BadRequest();
        }

       _repository.Update(categoria);
        return Ok(categoria);
    }


    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada... ");
        }

        var categoriaExcluida = _repository.Delete(categoria);
        return Ok(categoriaExcluida);
    }

    /*
    [HttpGet]
    [ServiceFilter(typeof(ApiLogginFilter))]
    public  ActionResult<IEnumerable<Categoria>> Get()    {
        _logger.LogInformation("===================GET api/categotria/ ======================");
        var lista =  _repository.GetCategorias();
        return Ok(lista);
    }

    [HttpGet("UsandoFromServices/{nome}")]
    public ActionResult<string> GetSaudacaoFromService([FromServices] IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }

    [HttpGet("SemUsarFromServices/{nome}")]
    public ActionResult<string> GetSaudacaoSemService(IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
            return _context.Categorias.Include(c => c.Produtos).Where(c => c.CategoriaId < 5).ToList();
    }
    */
}
