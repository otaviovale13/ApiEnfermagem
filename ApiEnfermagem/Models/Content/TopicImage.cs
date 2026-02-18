using ApiEnfermagem.Models.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiEnfermagem.Models.Content
{
    [Table("TopicImages", Schema = "Content")]
    public class TopicImage
    {
        [Key]
        public int ImageID { get; set; }

        [Required]
        public int TopicID { get; set; }

        [Required]
        [StringLength(500)]
        [Url]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Caption { get; set; }

        public int DisplayOrder { get; set; } = 0;

        [JsonIgnore]
        [ForeignKey("TopicID")]
        public virtual Topic? Topic { get; set; }
    }
}