using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEnfermagem.Models.Content
{
    [Table("ForumPosts", Schema = "Content")]
    public class ForumPost
    {
        [Key]
        public int PostID { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string ContentBody { get; set; } = string.Empty;

        [StringLength(50)]
        public string AuthorName { get; set; } = "Anônimo"; // Se não passar nada, fica Anônimo

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relação: Um post pode ter várias respostas
        public virtual ICollection<ForumReply> Replies { get; set; } = new List<ForumReply>();
    }
}