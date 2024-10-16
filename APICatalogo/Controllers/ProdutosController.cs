using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        try
        {
            var produtos = _context.Produtos.Take(10).ToList();
            if (produtos is null)
            {
                // NotFound() só é possivel devido o Retorno ActionResult que permite retornar uma lista ou um StatusCode
                return NotFound("Nenhum produto registrato...");
            }
            return produtos;
        }
        catch (Exception ex)
        {
            throw new Exception("houve um problema em obter Produtos, Aguarde uns instantes...");
        }

    }



    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        try
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não Encontrado...");
            }
            return produto;
        }
        catch (Exception)
        {
            throw new Exception("houve um problema em obter o produto, Aguarde uns instantes...");
        }



    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        try
        {
            if (produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {
            throw new Exception("houve um erro ao cadastrar produto, aguarde alguns instantes e tente novamente...");
        }


    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        try
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            // atualiza e persiste os dados no banco de dados
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {
            throw new Exception("houve um erro ao alterar produto, aguarde alguns instantes e tente novamente...");


        }

    }


    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {
            throw new Exception("houve um erro ao alterar produto, aguarde alguns instantes e tente novamente...");
        }
    }
}
