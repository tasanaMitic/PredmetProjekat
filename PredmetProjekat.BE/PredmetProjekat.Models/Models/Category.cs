using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
