using ApiEnfermagem.Data;
using ApiEnfermagem.Models.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEnfermagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly DBContext _context;

        public ForumController(DBContext context)
        {
            _context = context;
        }

        // 1. Pega todas as perguntas (com as respostas embutidas)
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.ForumPosts
                .Include(p => p.Replies)
                .OrderByDescending(p => p.CreatedAt) // Mais recentes primeiro
                .ToListAsync();

            return Ok(posts);
        }

        // 2. Cria uma nova pergunta
        [HttpPost("post")]
        public async Task<IActionResult> CreatePost([FromBody] ForumPost post)
        {
            post.CreatedAt = DateTime.UtcNow;
            _context.ForumPosts.Add(post);
            await _context.SaveChangesAsync();
            return Ok(post);
        }

        // 3. Adiciona uma resposta a uma pergunta
        [HttpPost("reply")]
        public async Task<IActionResult> CreateReply([FromBody] ForumReply reply)
        {
            reply.CreatedAt = DateTime.UtcNow;
            _context.ForumReplies.Add(reply);
            await _context.SaveChangesAsync();
            return Ok(reply);
        }

        // 4. (Para os Admins) Deletar uma pergunta inadequada
        [HttpDelete("post/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.ForumPosts.FindAsync(id);
            if (post == null) return NotFound();

            _context.ForumPosts.Remove(post);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}