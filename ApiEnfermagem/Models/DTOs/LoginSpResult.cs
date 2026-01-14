namespace ApiEnfermagem.Models.DTOs
{
    public class LoginSpResult
    {
        public int LoginStatus { get; set; } // 1 = Sucesso, 0 = Falha
        public int? AdminID { get; set; }
        public string? Username { get; set; }
    }
}
