using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Persistence;
using Microsoft.EntityFrameworkCore;


namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private DataBaseContext _context { get; set; }

    public PessoaController(DataBaseContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public ActionResult<List<Pessoa>> Get()
    {
       var result = _context.Pessoas.Include(p => p.contratos).ToList();

       if(!result.Any())
       {
            return NoContent();
       }
       return Ok(result);

    }

    [HttpPost]
    public Pessoa Post([FromBody]Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        _context.SaveChanges();
        return pessoa;
    }

    [HttpPut("{id}")]
    public string Update([FromRoute]int id, [FromBody]Pessoa pessoa)
    {
        _context.Pessoas.Update(pessoa);
        _context.SaveChanges();
        return $"Dados do id {id} atualizados";
    }

    [HttpDelete("{id}")]
    public ActionResult<> Delete([FromRoute]int id)
    {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

        if(result is null)
        {
            return BadRequest();
        }

        _context.Pessoas.Remove(result);
        _context.SaveChanges();
        return $"Deletado pessoa de Id {id}";
    }


}