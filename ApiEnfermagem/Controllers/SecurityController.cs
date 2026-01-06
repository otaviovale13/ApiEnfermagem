using ApiEnfermagem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEnfermagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly DBContext _context;

        public SecurityController(DBContext context)
        {
            _context = context;
        }
    }
}
