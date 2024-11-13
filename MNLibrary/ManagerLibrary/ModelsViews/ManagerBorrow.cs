using ManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.ModelsViews
{
    internal class ManagerBorrow
    {
        private static ManagerBorrow _instance = null;
        private static readonly object _instanceLock = new object();

        private ManagerBorrow() { }

        public static ManagerBorrow Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ManagerBorrow();
                    }
                    return _instance;
                }
            }
        }


        public void AddBorrowDetail(BorrowRecord bd)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        bd.BorrowDate = DateOnly.FromDateTime(DateTime.Now);
                        context.BorrowRecords.Add(bd);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void AddBorrowRecord(BorrowRecord br)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        br.BorrowDate = DateOnly.FromDateTime(DateTime.Now);
                        context.BorrowRecords.Add(br);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public void UpdatBorrow(BorrowRecord bd)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        context.Entry(bd).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void DeleteBorrow(BorrowRecord borrow)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        var b = context.BorrowRecords.SingleOrDefault(c => c.BorrowId == borrow.BorrowId);
                        if (b != null)
                        {
                            context.BorrowRecords.Remove(b);
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

      
    }
}

