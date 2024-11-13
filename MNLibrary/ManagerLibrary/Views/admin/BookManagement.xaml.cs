using System;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using ManagerLibrary.Models;
using System.IO;
using Microsoft.Win32;
using ManagerLibrary.ModelsViews;
using System.Net;



namespace ManagerLibrary.Views.admin
{
    public partial class BookManagement : Window
    {
        private Home _homeWindow;
        private dynamic _selectedBook;
        private Account _loggedInUser;
        private string locationFile = "E:\\dotritrong\\C#\\MNLibrary\\ManagerLibrary\\Images\\book_image\\";
        public BookManagement(Home homeWindow, Account loggedInUser)
        {   
            InitializeComponent();
            _homeWindow = homeWindow;
            _loggedInUser = loggedInUser;
            LoadBooks();
            LoadCategories();
            this.Closing += Closed_BookManagement;
        }

        private void LoadBooks(string? search = null, int? categoryId = null, int? status = null)

        {
          
            using (var context = new MnlibraryContext())
            {
                var query = from book in context.Books
                            join author in context.Authors on book.AuthorId equals author.AuthorId
                            join category in context.Categories on book.CategoryId equals category.CategoryId
                            select new
                            {
                                book.BookId,
                                book.Title,
                                author.AuthorName,
                                category.CategoryName,
                                book.CategoryId,
                                book.AuthorId,
                                book.Image,
                                ImagePath = locationFile + book.Image,
                                book.Isbn,
                                book.Quantity,
                                book.CreateDate,
                                book.UpdateDate,
                                book.Status,
                                DisplayStatus = book.Status == (int)BookStatus.Available ? "Available" : "Unavailable"

                            };

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.BookId.ToString().Contains(search)
                                             || s.Title.ToLower().Contains(search.ToLower())
                                             || s.AuthorName.ToLower().Contains(search.ToLower())
                                             || s.CategoryName.ToLower().Contains(search.ToLower())
                                             || s.Isbn.ToLower().Contains(search.ToLower()));
                }

                if (categoryId.HasValue)
                {
                    query = query.Where(s => s.CategoryId == categoryId.Value);
                }
                if (status.HasValue)
                {
                    query = query.Where(s => s.Status == status.Value);
                }

                var bookList = query.ToList();
                listViewBooks.ItemsSource = bookList;
                txtBookCount.Text = $"Total Books: {bookList.Count}";
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadBooks(txtSearch.Text);
        }

        private void LoadCategories()
        {
            using (var context = new MnlibraryContext())
            {
                var categories = context.Categories.ToList();
                cbCategory.ItemsSource = categories;
            }
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStatus.SelectedItem is ComboBoxItem selectedStatusItem)
            {
                int status = selectedStatusItem.Tag.ToString() switch
                {
                    "Available" => (int)BookStatus.Available,
                    "Unavailable" => (int)BookStatus.Unavailable,
                    _ => int.MinValue,
                };
                LoadBooks(null, null, status);
            }
        }



        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategory.SelectedItem is Category selectedCategory)
            {
                LoadBooks(null, selectedCategory.CategoryId);
            }
        }
        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookDialog dialog = new AddBookDialog();
            dialog.ShowDialog();
            LoadBooks();
        }

        private void BtnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            _selectedBook = listViewBooks.SelectedItem;
            if (_selectedBook != null)
            {
                UpdateBookDialog dialog = new UpdateBookDialog(_selectedBook);
                dialog.ShowDialog();
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Please select a book to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            _selectedBook = listViewBooks.SelectedItem;
            if (_selectedBook != null)
            {
                try
                {
                    var result = MessageBox.Show("Are you sure you want to delete this book?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var bookToDelete = new Book
                        {
                            BookId = _selectedBook.BookId,
                        };
                        ManagerBook.Instance.DeleteBook(bookToDelete);
                        LoadBooks();
                        MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Closed_BookManagement(object sender, EventArgs e)
        {
            _homeWindow.Show();
        }
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Home homeWindow = new Home(_loggedInUser);
            homeWindow.Show();
            this.Hide();
        }
    }
}
