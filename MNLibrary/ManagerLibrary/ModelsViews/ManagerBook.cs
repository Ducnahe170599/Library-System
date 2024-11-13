using Microsoft.EntityFrameworkCore;
using ManagerLibrary.Models;
using System;
using System.Linq;

namespace ManagerLibrary.ModelsViews
{
    internal class ManagerBook
    {
        private static ManagerBook _instance = null;
        private static readonly object _instanceLock = new object();

        private ManagerBook() { }

        public static ManagerBook Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ManagerBook();
                    }
                    return _instance;
                }
            }
        }

        public void AddBook(Book b)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        b.CreateDate = DateTime.Now; 
                        context.Books.Add(b);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void UpdateBook(Book b)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        context.Entry(b).State = EntityState.Modified;
                        context.SaveChanges();
                    }               
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void DeleteBook(Book book)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        var b = context.Books.SingleOrDefault(c => c.BookId == book.BookId);
                        if (b != null)
                        {
                            context.Books.Remove(b);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool IsbnExists(string isbn, int? bookId = null)
        {
            using (var context = new MnlibraryContext())
            {
                return bookId == null
                    ? context.Books.Any(b => b.Isbn == isbn)
                    : context.Books.Any(b => b.Isbn == isbn && b.BookId != bookId);
            }
        }

        public void DecreaseBookQuantity(int bookId, int quantity)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        var book = context.Books.SingleOrDefault(b => b.BookId == bookId);
                        if (book != null)
                        {
                            book.Quantity -= quantity;
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public void IncreaseBookQuantity(string BookIsbn, int quantity)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        var book = context.Books.SingleOrDefault(b => b.Isbn == BookIsbn);
                        if (book != null)
                        {
                            book.Quantity += quantity;
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool ImageExists(string imageName, int? bookId = null)
        {
            using (var context = new MnlibraryContext())
            {
                return bookId == null
                    ? context.Books.Any(b => b.Image == imageName)
                    : context.Books.Any(b => b.Image == imageName && b.BookId != bookId);
            }
        }
    }
}
