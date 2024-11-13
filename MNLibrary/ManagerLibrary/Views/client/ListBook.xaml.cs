using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;

namespace ManagerLibrary.Views.client
{
    public partial class ListBook : Window
    {
        private List<object> bookList;
        private int currentPage;
        private int itemsPerPage = 10;
        private Account _loggedInUser;

        public ListBook(List<object> bookList, Account loggedInUser)
        {
            InitializeComponent();
            this.bookList = bookList;
            currentPage = 0;
            DisplayCurrentPage();
            _loggedInUser = loggedInUser;
            this.Closing += ListBook_Closed;
        }

        private void ListBook_Closed(object sender, EventArgs e)
        {
            Home home = new Home(_loggedInUser);
            home.Show();
            this.Hide();
        }

        private void DisplayCurrentPage()
        {
            lvBook.ItemsSource = bookList.Skip(currentPage * itemsPerPage).Take(itemsPerPage).ToList();
            btnPrevious.IsEnabled = currentPage > 0;
            btnNext.IsEnabled = (currentPage + 1) * itemsPerPage < bookList.Count;
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if ((currentPage + 1) * itemsPerPage < bookList.Count)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Home homeWindow = new Home(_loggedInUser);
            homeWindow.Show();
            this.Hide();
        }

        private void RegisterBorrow_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = (sender as Button)?.DataContext as dynamic;

            if (selectedBook != null)
            {
                Book book = new Book
                {
                    BookId = selectedBook.BookId,
                    Title = selectedBook.Title,
                    Status = selectedBook.Status,
                    Isbn = selectedBook.Isbn
                };

                using (var context = new MnlibraryContext())
                {
                    bool hasBorrowed = context.BorrowRecords
                  .Any(b => b.Isbn == book.Isbn && b.UserId == _loggedInUser.UserId
                            && (b.Status == (int)OrderStatus.Submitted 
                             || b.Status == (int)OrderStatus.Confirmed 
                             || b.Status == (int)OrderStatus.Borrowed));
                    if (hasBorrowed)
                    {
                        MessageBox.Show("Bạn đang mượn cuốn sách này! Vui lòng chọn sách khác.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (book.Status == (int)BookStatus.Available)
                    {
                        RegisterBorrow rb = new RegisterBorrow(_loggedInUser, selectedBook);
                        rb.ShowDialog();
                    }
                }
            }
        }

    }
}

