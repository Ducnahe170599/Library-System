using ManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.ModelsViews
{
    internal class ManagerCategory
    {
        private static ManagerCategory _instance = null;
        private static readonly object _instanceLock = new object();

        private ManagerCategory() { }

        public static ManagerCategory Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ManagerCategory();
                    }
                    return _instance;
                }
            }
        }


        public void AddCategory(Category c)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        context.Categories.Add(c);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void UpdatCategory(Category c)
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

        public void DeleteCategory(Category category)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        var b = context.Books.SingleOrDefault(c => c.CategoryId == category.CategoryId);
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

        public bool CategoryExists(string categoryName)
        {
            using (var context = new MnlibraryContext())
            {
                return context.Categories.Any(c => c.CategoryName == categoryName);
            }
        }
    }

}

