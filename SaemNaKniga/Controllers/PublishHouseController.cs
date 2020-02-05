using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaemNaKniga.Models;
using SaemNaKniga.Models.Publish_House_Models;

namespace SaemNaKniga.Controllers
{
    [Authorize]
    public class PublishHouseController : Controller
    {
        // GET: PublishHouse
        private ApplicationDbContext database;

        public PublishHouseController()
        {
            database = ApplicationDbContext.Create();
        }

        public ActionResult Index()
        {
            List<PublishHouseIndex> publishHouseIndices = new List<PublishHouseIndex>();
            foreach(PublishHouse publishHouse in database.PublishHouses.ToList())
            {
                PublishHouseIndex publishHouseIndex = new PublishHouseIndex();
                publishHouseIndex.publishHouse = publishHouse;
                foreach(Link link in database.Links.ToList())
                {
                    if(link.PublishHouseId==publishHouseIndex.publishHouse.Id)
                    {
                        publishHouseIndex.books.Add(database.Books.FirstOrDefault(z => z.Id == link.BookId));
                    }
                }
                publishHouseIndices.Add(publishHouseIndex);
            }
            return View(publishHouseIndices);
        }

        /*public ActionResult Edit(int id)
        {
            PublishHouse publishHouse = database.PublishHouses.FirstOrDefault(z => z.Id == id);
            return View(publishHouse);
        }

        [HttpPost]
        public ActionResult Edit(PublishHouse publishHouse)
        {
            PublishHouse nova = database.PublishHouses.FirstOrDefault(z => z.Id == publishHouse.Id);
            nova.Name = publishHouse.Name;
            nova.ImgUrl = publishHouse.ImgUrl;
            nova.Description = publishHouse.Description;
            database.SaveChanges();
            return Redirect("/PublishHouse");
        }
        */
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View(new PublishHouse());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(PublishHouse publishHouse)
        {
            database.PublishHouses.Add(publishHouse);
            database.SaveChanges();
            return Redirect("/PublishHouse");
        }

        public ActionResult Details(int id)
        {
            PublishHouseDetails publishHouseDetails = new PublishHouseDetails();
            publishHouseDetails.publishHouse = database.PublishHouses.FirstOrDefault(z => z.Id == id);
            foreach(Link link in database.Links.ToList())
            {
                if (link.PublishHouseId == publishHouseDetails.publishHouse.Id)
                {
                    BookExtended book = new BookExtended();
                    Book bookBasic = database.Books.FirstOrDefault(z => z.Id == link.BookId);
                    book.Id = bookBasic.Id;
                    book.Name = bookBasic.Name;
                    book.ImgUrl = bookBasic.ImgUrl;
                    book.Year = bookBasic.Year;
                    book.Description = bookBasic.Description;
                    book.Price = link.Price;
                    book.Units = link.InStock;
                    publishHouseDetails.books.Add(book);
                }
            }
            return View(publishHouseDetails);
        }

        /*
        public ActionResult AddBook()
        {
            PublishHouseAddBook publishHouseAddBook = new PublishHouseAddBook();
            publishHouseAddBook.books = database.Books.ToList();
            publishHouseAddBook.publishHouses = database.PublishHouses.ToList();
            return View(publishHouseAddBook);
        }

        [HttpPost]
        public ActionResult AddBook(PublishHouseAddBook publishHouseAddBook)
        {
            Link link = new Link();
            link.BookId = publishHouseAddBook.BookId;
            link.PublishHouseId = publishHouseAddBook.PublishHouseId;
            link.Price = publishHouseAddBook.Price;
            link.InStock = publishHouseAddBook.InStock;
            database.Links.Add(link);
            database.SaveChanges();
            return Redirect("/PublishHouse");
        }
        */
        [Authorize(Roles = "Admin")]
        public ActionResult AddBookSpecific(int id)
        {
            PublishHouseAddBookSpecific publishHouseAddBookSpecific = new PublishHouseAddBookSpecific();
            publishHouseAddBookSpecific.publishHouse = database.PublishHouses.FirstOrDefault(z => z.Id == id);
            publishHouseAddBookSpecific.Authors = database.Authors.ToList();
            publishHouseAddBookSpecific.PublishHouseId = id;
            return View(publishHouseAddBookSpecific);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddBookSpecific(PublishHouseAddBookSpecific publishHouseAddBookSpecific)
        {
            Book newBook = publishHouseAddBookSpecific.Book;
            Author bookAuthor = database.Authors.Find(publishHouseAddBookSpecific.AuthorId);
            newBook.Author = bookAuthor;
            database.Books.Add(newBook);
            bookAuthor.Books.Add(newBook);
            database.SaveChanges();
            Link link = new Link();
            Book addedBook = database.Authors.Find(publishHouseAddBookSpecific.AuthorId).Books.Last();
            link.BookId = addedBook.Id;
            link.PublishHouseId = publishHouseAddBookSpecific.PublishHouseId;
            link.AuthorId = publishHouseAddBookSpecific.AuthorId;
            link.Price = publishHouseAddBookSpecific.Price;
            link.InStock = publishHouseAddBookSpecific.InStock;
            database.Links.Add(link);
            database.SaveChanges();
            return Redirect("/PublishHouse");
        }


        [Authorize(Roles = "User")]
        public ActionResult BuyBook(string PublishHouseId, string BookId)
        {
            int publishHouseId = Int32.Parse(PublishHouseId);
            int bookId = Int32.Parse(BookId);
            PublisHouseBuyBook publisHouseBuyBook = new PublisHouseBuyBook();
            publisHouseBuyBook.publishHouse = database.PublishHouses.FirstOrDefault(z => z.Id == publishHouseId);
            Book book = database.Books.FirstOrDefault(z => z.Id == bookId);
            publisHouseBuyBook.bookExtended = new BookExtended();
            publisHouseBuyBook.bookExtended.Id = book.Id;
            publisHouseBuyBook.bookExtended.Name = book.Name;
            publisHouseBuyBook.bookExtended.ImgUrl = book.ImgUrl;
            publisHouseBuyBook.bookExtended.Year = book.Year;
            publisHouseBuyBook.bookExtended.Description = book.Description;
            Link link = database.Links.FirstOrDefault(z => z.BookId == bookId && z.PublishHouseId == publishHouseId);
            publisHouseBuyBook.bookExtended.Price = link.Price;
            publisHouseBuyBook.bookExtended.Units = link.InStock;
            publisHouseBuyBook.BookId = link.BookId;
            publisHouseBuyBook.PublishHouseId = link.PublishHouseId;
            return View(publisHouseBuyBook);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult BuyBook(PublisHouseBuyBook publisHouseBuyBook)
        {
            Link link = database.Links.FirstOrDefault(z => z.BookId == publisHouseBuyBook.BookId && z.PublishHouseId == publisHouseBuyBook.PublishHouseId);
            link.InStock = link.InStock - publisHouseBuyBook.UnitsBuy;
            database.SaveChanges();
            return Redirect("/PublishHouse");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IncreaseBooks(int id)
        {
            PublishHouseIncreaseBooks publishHouseIncreaseBooks = new PublishHouseIncreaseBooks();
            publishHouseIncreaseBooks.publishHouse = database.PublishHouses.FirstOrDefault(z => z.Id == id);
            foreach(Link link in database.Links.ToList())
            {
                if (link.PublishHouseId == id) publishHouseIncreaseBooks.books.Add(database.Books.FirstOrDefault(z => z.Id == link.BookId));
            }
            return View(publishHouseIncreaseBooks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult IncreaseBooks(PublishHouseIncreaseBooks publishHouseIncreaseBooks)
        {
            Link link = database.Links.FirstOrDefault(z => z.BookId == publishHouseIncreaseBooks.BookId && z.PublishHouseId == publishHouseIncreaseBooks.PublishHouseId);
            link.InStock = link.InStock + publishHouseIncreaseBooks.Units;
            database.SaveChanges();
            return Redirect("PublishHouse/");
        }
    }
}