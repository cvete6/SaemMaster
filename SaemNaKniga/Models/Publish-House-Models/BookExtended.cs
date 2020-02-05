using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SaemNaKniga.Models;

namespace SaemNaKniga.Models.Publish_House_Models
{
    [NotMapped]
    public class BookExtended : Book
    {
        [Required(ErrorMessage ="Мора да внесите цена во полето")]
        [Display(Name ="Цена")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Мора да внесите број на книги во полето")]
        [Display(Name = "Број на книги")]
        public int Units { get; set; }
    }
}