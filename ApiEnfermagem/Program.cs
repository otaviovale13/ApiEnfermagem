using Microsoft.EntityFrameworkCore;
using ApiEnfermagem.Data;
using System.Text.Json.Serialization; // <--- Adicionei para tratar o JSON

var builder = WebApplication.CreateBuilder(args);

// 1. ADICIONADO: Proteção extra contra Loops Infinitos no JSON
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. CORREÇÃO CRÍTICA: Usar SEMPRE a 'strConnExterna'
// Removemos o "if production", pois a conexão interna estava falhando.
var strConn = builder.Configuration.GetConnectionString("strConnExterna");

// Trava de segurança: Se a string não vier, avisa no log (evita erro 500 mudo)
if (string.IsNullOrEmpty(strConn))
{
    throw new InvalidOperationException("A Connection String 'strConnExterna' não foi encontrada no appsettings.json!");
}

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(strConn));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// 3. CORREÇÃO DO SWAGGER:
// Agora ele roda SEMPRE (Prod e Dev) e abre na raiz
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
    c.RoutePrefix = string.Empty; // Abre direto no tiiotavio.runasp.net
});

// Removi o bloco "if (app.Environment.IsDevelopment())" duplicado que estava aqui.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("PermitirTudo");

app.MapControllers();

// Tratamento de erros globais (Mantive o seu, que é bom para segurança)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            Console.WriteLine($"[CRITICAL ERROR] {error.Error.Message}");
            // Em produção, o usuário vê apenas esta mensagem genérica:
            var response = new { message = "Ocorreu um erro interno no servidor. Tente novamente mais tarde." };
            await context.Response.WriteAsJsonAsync(response);
        }
    });
});

app.Run();