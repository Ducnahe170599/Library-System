using ManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.ModelsViews
{
    internal class ManagerAuthor
    {
        private static ManagerAuthor _instance = null;
        private static readonly object _instanceLock = new object();

        private ManagerAuthor() { }

        public static ManagerAuthor Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ManagerAuthor();
                    }
                    return _instance;
                }
            }
        }


        public void AddAuthor(Author c)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        context.Authors.Add(c);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void UpdatAuthor(Author c)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        context.Entry(c).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void DeleteAuthor(Author Author)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        var b = context.Books.SingleOrDefault(c => c.AuthorId == Author.AuthorId);
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

        public bool AuthorExists(string AuthorName)
        {
            using (var context = new MnlibraryContext())
            {
                return context.Authors.Any(c => c.AuthorName == AuthorName);
            }
        }
    }
}

