using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SaemNaKniga.Models;


namespace SaemNaKniga.Models
{
    public class AuthorAddBook
    {
        public List<Book> books { get; set; }
        public List<PublishHouse> publishHouses { get; set; }
        public List<Author> authors { get; set; }




        [Required(ErrorMessage = "Мора да внесите цена во полето")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Мора да внесите број на книги")]
        [Display(Name = "Број на книги")]
        public int InStock { get; set; }

        [Required(ErrorMessage = "Мора да изберете автор")]
        [Display(Name = "Изберете автор")]
        public int AuthorId { get; set; }


        [Required(ErrorMessage = "Мора да изберете издавачка куќа")]
        [Display(Name = "Изберете издавачка куќа")]
        public int PublishHouseId { get; set; }

        [Required(ErrorMessage = "Мора да изберете книга")]
        [Display(Name = "Изберете книга")]
        public int BookId { get; set; }
    }
}