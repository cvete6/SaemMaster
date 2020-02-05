using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaemNaKniga.Models;

namespace SaemNaKniga.Models.Publish_House_Models
{
    public class PublishHouseIndex
    {
        public PublishHouse publishHouse { get; set; }
        public List<Book> books { get; set; }

        public PublishHouseIndex()
        {
            books = new List<Book>();
        }

        public int NumberBooks()
        {
            return books.Capacity;
        }
    }
}