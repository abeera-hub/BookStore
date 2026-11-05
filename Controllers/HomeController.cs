using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "Ali", Country = "Pakistan" },
            new Author { Id = 2, Name = "Abeera", Country = "USA" }
        };

        public static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "C#", Price = 1500, AuthorId = 1 },
            new Book { Id = 2, Title = "Python", Price = 1700, AuthorId = 1 },
            new Book { Id = 3, Title = "Java ", Price = 1600, AuthorId = 2 }
        };

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Authors = Authors;

            model.Books = Books.Select(b => new BookCardViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                AuthorName = Authors.FirstOrDefault(a => a.Id == b.AuthorId).Name
            }).ToList();

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            Book newBook = new Book();
            newBook.Id = Books.Count == 0 ? 1 : Books.Max(b => b.Id) + 1;
            newBook.Title = title;
            newBook.Price = price;
            newBook.AuthorId = authorId;
            Books.Add(newBook);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            Author newAuthor = new Author();

            newAuthor.Id = Authors.Max(a => a.Id) + 1;
            newAuthor.Name = name;
            newAuthor.Country = country;

            Authors.Add(newAuthor);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book = Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Books.Remove(book);
            }
            return RedirectToAction("Index");
        }
    }
}