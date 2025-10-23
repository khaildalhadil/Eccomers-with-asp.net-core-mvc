using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models.Models
{
    public class Category
    {
        // data Annotation to decler it
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [Length(2, 10)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        [Range(0, 100)]
        public int DisplayOrder { get; set; }

    }
}
