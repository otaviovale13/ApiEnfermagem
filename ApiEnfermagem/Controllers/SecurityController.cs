using ApiEnfermagem.Data;
using ApiEnfermagem.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("logar")]
        public async Task<IActionResult> Logar([FromBody] LoginRequestApi request)
        {
            // 1. Validação básica
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Usuário e senha são obrigatórios.");
            }

            try
            {
                // 2. Prepara os parâmetros para o SQL
                var pUsername = new SqlParameter("@Username", request.Username);
                var pPassword = new SqlParameter("@PasswordInput", request.Password);

                // 3. Executa a Procedure "sp_AdminLogin"
                var resultadoLista = await _context.Database
                    .SqlQuery<LoginSpResult>($"EXEC Security.sp_AdminLogin @Username={pUsername}, @PasswordInput={pPassword}")
                    .ToListAsync();

                var resultado = resultadoLista.FirstOrDefault();

                // 4. Verifica o LoginStatus
                if (resultado != null && resultado.LoginStatus == 1)
                {
                    return Ok(new
                    {
                        UserId = resultado.AdminID,
                        FullName = resultado.Username,
                        Message = "Login realizado com sucesso"
                    });
                }
                else
                {
                    return Unauthorized("Usuário ou senha inválidos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no login: {ex.Message}");
                return StatusCode(500, "Erro interno ao processar login.");
            }
        }
    }
}
