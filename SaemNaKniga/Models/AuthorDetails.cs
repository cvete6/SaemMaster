using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaemNaKniga.Models;

namespace SaemNaKniga.Models
{
    public class AuthorDetails
    {
        public Author author { get; set; }
        public List<BookExtendenAuthor> Books { get; set; }

        public AuthorDetails()
        {
            Books = new List<BookExtendenAuthor>();
        }
    }
}