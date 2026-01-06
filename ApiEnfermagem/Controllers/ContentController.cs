using ApiEnfermagem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
