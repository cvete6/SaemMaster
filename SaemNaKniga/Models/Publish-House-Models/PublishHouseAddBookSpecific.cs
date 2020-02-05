using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SaemNaKniga.Models;

namespace SaemNaKniga.Models.Publish_House_Models
{
    public class PublishHouseAddBookSpecific
    {
        public PublishHouse publishHouse { get; set; }
        public Book Book { get; set; }
        public List<Author> Authors { set; get; }
        public int PublishHouseId { get; set; }
        [Display(Name ="Автор")]
        public int AuthorId { set; get; }

        [Required (ErrorMessage ="Мора да внесете цена во полето")]
        [Display(Name ="Цена")]
        public int Price { get; set; }

        [Required (ErrorMessage ="Мора да имате внесено број на книги")]
        [Display(Name ="Број на книги")]
        public int InStock { get; set; }
    }
}