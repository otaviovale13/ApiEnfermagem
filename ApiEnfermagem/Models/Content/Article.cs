using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiEnfermagem.Models.Security
{
    [Table("Articles", Schema = "Content")]
    public class Article
    {
        [Key]
        public long ArticleID { get; set; } // long = BIGINT

        [Required]
        public int TopicID { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O conteúdo do artigo é obrigatório.")]
        [Column(TypeName = "nvarchar(max)")] // Garante que o EF entenda que é texto longo
        public string ContentBody { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        [ForeignKey("TopicID")]
        public virtual Topic? Topic { get; set; }
    }
}