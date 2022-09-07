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
    public List<Pessoa> Get()
    {
       /*  Pessoa pessoa = new Pessoa("João", 30, "1234578");
        Contrato novoContrato = new Contrato("abc123", 50.60);
        pessoa.contratos.Add(novoContrato); */
       return _context.Pessoas.Include(p => p.contratos).ToList();
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
    public string Delete([FromRoute]int id)
    {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

        _context.Pessoas.Remove(result);
        _context.SaveChanges();
        return $"Deletado pessoa de Id {id}";
    }
}