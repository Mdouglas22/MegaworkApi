using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using ProjetoWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjetoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _context;

        public ValuesController(DataContext contextaux)
        {
            _context = contextaux;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultado = await _context.Bancos.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
            
        }

        [HttpGet("buscar")]
        public ActionResult GetBuscar()
        {
            var listaBancos = (from banco in _context.Bancos
                               select banco).ToList();
            return Ok(listaBancos);
        }

        [HttpGet("filtro/{nomeBanco}")]
        public ActionResult GetFiltro(string nomeBanco)
        {
            var listaBancos = (from banco in _context.Bancos
                               where banco.Nome.Contains(nomeBanco)
                               select banco).ToList();
            return Ok(listaBancos);
        }

        [HttpGet("{nomeBanco}")]
        public ActionResult GetInserir(string nomeBanco)
        {
            var banco = new Banco { Nome = nomeBanco};
                _context.Bancos.Add(banco);
                _context.SaveChanges();
        return Ok();
        }

        [HttpGet("atualizar/{nomeBanco}")]
        public ActionResult GetAtualizar(string nomeBanco)
        {
            var auxBanco = (from banco in _context.Bancos
                               where banco.Nome == nomeBanco
                               select banco).FirstOrDefault();
            auxBanco.Nome = "Santader";
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("deletar/{nomeBanco}")]
        public ActionResult GetDeletar(string nomeBanco)
        {
            var auxBanco = (from banco in _context.Bancos
                            where banco.Nome == nomeBanco
                            select banco).FirstOrDefault();

            _context.Bancos.Remove(auxBanco);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("addRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                new Banco { Nome = "Bradesco"},
                new Banco { Nome = "Caixa" }
                );
            _context.SaveChanges();
            return Ok();
        }
    }
}
