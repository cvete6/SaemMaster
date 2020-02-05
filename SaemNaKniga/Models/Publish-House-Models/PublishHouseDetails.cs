using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaemNaKniga.Models;

namespace SaemNaKniga.Models.Publish_House_Models
{
    public class PublishHouseDetails
    {
        public PublishHouse publishHouse { get; set; }
        public List<BookExtended> books { get; set; }

        public PublishHouseDetails()
        {
            books = new List<BookExtended>();
        }
    }
}