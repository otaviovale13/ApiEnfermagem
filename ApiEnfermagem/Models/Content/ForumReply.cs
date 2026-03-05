using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiEnfermagem.Models.Content
{
    [Table("ForumReplies", Schema = "Content")]
    public class ForumReply
    {
        [Key]
        public int ReplyID { get; set; }

        [Required]
        public int PostID { get; set; }

        [Required(ErrorMessage = "O conteúdo da resposta é obrigatório.")]
        public string ContentBody { get; set; } = string.Empty;

        [StringLength(50)]
        public string AuthorName { get; set; } = string.Empty;

        // Isso vai ajudar o app a pintar a resposta de azul caso seja do suporte/admin
        public bool IsAdminReply { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        [ForeignKey("PostID")]
        public virtual ForumPost? Post { get; set; }
    }
}