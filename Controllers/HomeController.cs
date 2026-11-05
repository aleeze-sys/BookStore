using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.K. Rowling", Country = "UK" },
            new Author { Id = 2, Name = "Paulo Coelho", Country = "Brazil" }
        };

        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "Harry Potter", Price = 1500, AuthorId = 1 },
            new Book { Id = 2, Title = "The Alchemist", Price = 1200, AuthorId = 2 },
            new Book { Id = 3, Title = "Fantastic Beasts", Price = 1800, AuthorId = 1 }
        };

        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();

            vm.Authors = Authors;

            vm.Books = Books.Select(book => new BookCardViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                AuthorName = Authors.FirstOrDefault(a => a.Id == book.AuthorId)?.Name
            }).ToList();

            return View(vm);
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
            if (string.IsNullOrWhiteSpace(title))
            {
                return RedirectToAction("Index");
            }

            int newId = Books.Count == 0 ? 1 : Books.Max(b => b.Id) + 1;

            Book book = new Book
            {
                Id = newId,
                Title = title,
                Price = price,
                AuthorId = authorId
            };

            Books.Add(book);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            int newId = Authors.Count == 0 ? 1 : Authors.Max(a => a.Id) + 1;

            Author author = new Author
            {
                Id = newId,
                Name = name,
                Country = country
            };

            Authors.Add(author);

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