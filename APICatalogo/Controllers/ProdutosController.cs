using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IRepository<Produto> _repository;
    private readonly IConfiguration _configuration;
    public ProdutosController(IProdutoRepository produtoRepository,IRepository<Produto> repository, IConfiguration configuration)
    {
        _produtoRepository = produtoRepository;
         _repository = repository;
        _configuration = configuration;
    }


    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);
        if(produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.Get(c => c.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não Encontrado...");
        }
        return Ok(produto);
    }

    [HttpGet("todos")]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetAll();
        if (produtos is null)
        {
            // NotFound() só é possivel devido o Retorno ActionResult que permite retornar uma lista ou um StatusCode
            return NotFound("Nenhum produto registrato...");
        }
        return Ok(produtos);
    }


    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
        {
            return BadRequest();
        }
        var prod = _repository.Create(produto);
        return new CreatedAtRouteResult("ObterProduto", new { id = prod.ProdutoId }, prod);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest("IDENTIFICADORES NÃO CORRESPONDEM");
        }
        // atualiza e persiste os dados no banco de dados
        Produto resp = _repository.Update(produto);
        if (resp != null) return Ok(produto);

        return StatusCode(500, $"Falha ao atualizar o produto de Id = {id}");

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var prod = _repository.Get(p => p.ProdutoId == id);
        if (prod is null)
        {
            return NotFound("Produto não encontrado");
        }

        var prodDeletado = _repository.Delete(prod);
        return Ok(prodDeletado);
    }

}
/*
[HttpGet("LerArquivoConfiguracao")]
public string GetValores()
{
    var valor1 = _configuration["chave1"];
    var valor2 = _configuration["chave2"];
    var secao1 = _configuration["secao1:chave2"];
    return $"Chave 1: {valor1}, Chave 2: {valor2}, Secao 1 => chave2  {secao1}";
}    

// min => indica o valor minimo da requisição

*/

