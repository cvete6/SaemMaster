using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SaemNaKniga.Models.Publish_House_Models;

namespace SaemNaKniga.Models.Authors_Models
{
    public class AuthorBuyBook
    {
        public Author Author { get; set; }
        public BookExtended bookExtended { get; set; }

        [Required(ErrorMessage = "Мора да внесите број на книги")]
        [Display(Name = "Внесете број на книги")]
        public int UnitsBuy { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }
    }
}