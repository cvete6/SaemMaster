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
    public class PublishHouseIncreaseBooks
    {
        public PublishHouse publishHouse { get; set; }
        public List<Book> books { get; set; }

        [Required(ErrorMessage ="Мора да имате избрано книга")]
        [Display(Name ="Изберете книга")]
        public int BookId { get; set; }

        [Required(ErrorMessage ="Мора да внесите број на книги")]
        [Display(Name ="Внесете број на книги")]
        public int Units { get; set; }

        public int PublishHouseId { get; set; }

        public PublishHouseIncreaseBooks()
        {
            books = new List<Book>();
        }

        public Book getById(int id)
        {
            foreach (Book book in books)
                if (book.Id == id)
                    return book;
            return null;
        }
    }
}