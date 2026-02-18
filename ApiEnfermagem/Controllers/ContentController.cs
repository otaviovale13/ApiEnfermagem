using ApiEnfermagem.Data;
using ApiEnfermagem.Models.Content;
using ApiEnfermagem.Models.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEnfermagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly DBContext _context;

        public ContentController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ObterConteudo()
        {
            var resultado = new ArtigosItem();

            resultado.Topicos = _context.Topics
                                .Include(t => t.Images)
                                .ToList();

            resultado.Artigos = _context.Articles.ToList();

            return Ok(resultado);
        }
    }
}
