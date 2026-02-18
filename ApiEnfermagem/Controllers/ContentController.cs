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

        // POST: api/Content
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Article artigo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                artigo.CreatedAt = DateTime.UtcNow;
                _context.Articles.Add(artigo);
                await _context.SaveChangesAsync();
                return Ok(artigo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar: {ex.Message}");
            }
        }

        // PUT: api/Content/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Article artigo)
        {
            if (id != artigo.ArticleID) return BadRequest("ID divergente");

            var existente = await _context.Articles.FindAsync(id);
            if (existente == null) return NotFound();

            try
            {
                existente.Title = artigo.Title;
                existente.ContentBody = artigo.ContentBody;
                existente.TopicID = artigo.TopicID;
                existente.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar: {ex.Message}");
            }
        }

        // DELETE: api/Content/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var artigo = await _context.Articles.FindAsync(id);
            if (artigo == null) return NotFound();

            try
            {
                _context.Articles.Remove(artigo);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar: {ex.Message}");
            }
        }
    }
}
