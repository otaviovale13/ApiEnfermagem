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

            // 2. Busca os Tópicos (que são tabela de verdade)
            resultado.Topicos = _context.Topics.ToList();

            // 3. Busca os Artigos (que são tabela de verdade)
            resultado.Artigos = _context.Articles.ToList();

            // 4. Retorna o pacote completo
            return Ok(resultado);
        }
    }
}
