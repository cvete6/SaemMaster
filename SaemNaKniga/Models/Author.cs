using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaemNaKniga.Models
{
    public class Author
    {
        [Key]
        public int Id { set; get; }
        [Required (ErrorMessage ="Мора да внесите име во полето")]
        [Display(Name ="Име")]
        public string Name { set; get; }
        [Required (ErrorMessage ="Мора да внесите пол во полето")]
        [Display(Name ="Пол")]
        public string Gender { set; get; }
        [Required(ErrorMessage ="Мора да внесите линк од сликата")]
        [Display(Name="Линк од Слика")]
        public string ImgUrl { set; get; }
        [Display(Name ="Кратка Биографија")]
        public string Biography { set; get; }
        public virtual ICollection<Book> Books { set; get; }

        public Author() {
            Books = new List<Book>();
        }
    }
}