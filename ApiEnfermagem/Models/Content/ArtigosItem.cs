using ApiEnfermagem.Models.Security;

namespace ApiEnfermagem.Models.Content;

public class ArtigosItem
{
    public List<Topic> Topicos { get; set; }
    public List<Article> Artigos { get; set; }
}