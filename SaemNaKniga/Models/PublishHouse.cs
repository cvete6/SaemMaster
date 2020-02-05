using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaemNaKniga.Models
{
    public class PublishHouse
    {
        [Key]
        public int Id { set; get; }
        [Required(ErrorMessage = "Мора да внесите има во полето")]
        [Display(Name ="Име")]
        public string Name { set; get; }
        [Required(ErrorMessage ="Мора да внесите линк од сликата")]
        [Display(Name="Линк од слика")]
        public string ImgUrl { set; get; }
        [Display(Name="Краток опис")]
        public string Description { set; get; }
        public virtual ICollection<Book> Books { set; get; }

        public PublishHouse() {
            Books = new List<Book>();
        }
    }
}