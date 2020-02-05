using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaemNaKniga.Models;
using SaemNaKniga.Models.Authors_Models;
using SaemNaKniga.Models.Publish_House_Models;

namespace SaemNaKniga.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Authors
        public ActionResult Index()
        {
            return View(db.Authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Author author = db.Authors.Find(id);
            //if (author == null)
            //{
            //  return HttpNotFound();
            //}
            //return View(author);

            AuthorDetails authorDetails = new AuthorDetails();
            authorDetails.author = db.Authors.FirstOrDefault(z => z.Id == id);
            foreach (Link link in db.Links.ToList())
            {
                if (link.AuthorId == authorDetails.author.Id)
                {
                    BookExtendenAuthor book = new BookExtendenAuthor();
                    Book bookBasic = db.Books.FirstOrDefault(z => z.Id == link.BookId);
                    book.Id = bookBasic.Id;
                    book.Name = bookBasic.Name;
                    book.ImgUrl = bookBasic.ImgUrl;
                    book.Year = bookBasic.Year;
                    book.Description = bookBasic.Description;
                    book.Price = link.Price;
                    book.Units = link.InStock;

                    authorDetails.Books.Add(book);
                }
            }
            return View(authorDetails);
        }

        /*public ActionResult AddBook()
        {
            AuthorAddBook authorAddBook = new AuthorAddBook();
            authorAddBook.books = db.Books.ToList();
            authorAddBook.publishHouses = db.PublishHouses.ToList();
            authorAddBook.authors = db.Authors.ToList();

            return View(authorAddBook);
        }

        [HttpPost]
        public ActionResult AddBook(AuthorAddBook authorAddBook)
        {
            Link link = new Link();
            link.BookId = authorAddBook.BookId;
            link.PublishHouseId = authorAddBook.PublishHouseId;
            link.AuthorId = authorAddBook.AuthorId;
            link.Price = authorAddBook.Price;
            link.InStock = authorAddBook.InStock;
            db.Links.Add(link);
            db.SaveChanges();
            return Redirect("/Authors");
        }
        */
        // GET: Authors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,ImgUrl,Biography")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }
        [Authorize(Roles = "User")]
        public ActionResult BuyBook(string AuthorId, string BookId)
        {
            int authorId = Int32.Parse(AuthorId);
            int bookId = Int32.Parse(BookId);
            AuthorBuyBook authorBuyBook = new AuthorBuyBook();
            authorBuyBook.Author = db.Authors.FirstOrDefault(z => z.Id == authorId);
            Book book = db.Books.FirstOrDefault(z => z.Id == bookId);
            authorBuyBook.bookExtended = new BookExtended();
            authorBuyBook.bookExtended.Id = book.Id;
            authorBuyBook.bookExtended.Name = book.Name;
            authorBuyBook.bookExtended.ImgUrl = book.ImgUrl;
            authorBuyBook.bookExtended.Year = book.Year;
            authorBuyBook.bookExtended.Description = book.Description;
            Link link = db.Links.FirstOrDefault(z => z.BookId == bookId && z.AuthorId == authorId);
            authorBuyBook.bookExtended.Price = link.Price;
            authorBuyBook.bookExtended.Units = link.InStock;
            authorBuyBook.BookId = link.BookId;
            authorBuyBook.AuthorId = link.AuthorId;
            return View(authorBuyBook);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult BuyBook(AuthorBuyBook authorBuyBook)
        {
            Link link = db.Links.FirstOrDefault(z => z.BookId == authorBuyBook.BookId && z.AuthorId == authorBuyBook.AuthorId);
            link.InStock = link.InStock - authorBuyBook.UnitsBuy;
            db.SaveChanges();
            return Redirect("/Authors");
        }



        // GET: Authors/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,ImgUrl,Biography")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
