using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkeyBook.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }


        [Required(ErrorMessage = "Please Select The Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Display(Name = "Add Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
