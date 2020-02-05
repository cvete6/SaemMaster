using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaemNaKniga.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Publish House")]
        public int PublishHouseId { get; set; }

        [Required]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required(ErrorMessage = "The In Stock field is required")]
        [Display(Name = "In Stock")]
        public int InStock { get; set; }

    }
}