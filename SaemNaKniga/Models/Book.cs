using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaemNaKniga.Models
{
    public class Book
    {
        [Key]
        public int Id { set; get; }
        [Required(ErrorMessage = "Мора да внесете име во полето")]
        [Display(Name = "Име")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Мора да внесите година на издавање")]
        [Range(0, 2018, ErrorMessage = "Внесете број помеѓу 0 и 2018")]
        [Display(Name = "Година на Издавање")]
        public int Year { set; get; }
        [Required(ErrorMessage = "Мора да внесите линк од сликата")]
        [Display(Name = "Линк од Слика")]
        public string ImgUrl { set; get; }
        [Display(Name = "Краток Опис")]
        public string Description { set; get; }
        public virtual Author Author { set; get; }
        public virtual ICollection<PublishHouse> PublishHouses { set; get; }

        public Book()
        {
            PublishHouses = new List<PublishHouse>();
        }

    }
}