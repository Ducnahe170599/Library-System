using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using System.Windows.Controls;


namespace ManagerLibrary.Views.admin
{
    public partial class UpdateBookDialog : Window
    {
        private string imagePath;
        private string locationFile = "C:\\Users\\nguye\\OneDrive\\Desktop\\MNLibrary\\ManagerLibrary\\Images\\book_image\\";
        private Book _book;

        public UpdateBookDialog(dynamic book)
        {
            InitializeComponent();
            _book = new Book
            {
                BookId = book.BookId,
                Title = book.Title,
                Isbn = book.Isbn,
                CategoryId = book.CategoryId,
                AuthorId = book.AuthorId,
                Image = book.Image,
                Quantity = book.Quantity,
                CreateDate = book.CreateDate,
                UpdateDate = DateTime.Now,
                Status = book.Status
                
            };
            LoadAuthors();
            LoadCategories();
            this.DataContext = _book;
        }

        private void LoadAuthors()
        {
            using (var context = new MnlibraryContext())
            {
                var authors = context.Authors.ToList();
                cbAuthor.ItemsSource = authors;
            }
        }
   

        private void LoadCategories()
        {
            using (var context = new MnlibraryContext())
            {
                var categories = context.Categories.ToList();
                cbCategory.ItemsSource = categories;
            }
        }

        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                txtImageName.Text = System.IO.Path.GetFileName(imagePath);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_book.Title) || string.IsNullOrWhiteSpace(_book.Isbn) ||
                cbAuthor.SelectedItem == null || cbCategory.SelectedItem == null || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Quantity must be a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(imagePath) && ManagerBook.Instance.ImageExists(txtImageName.Text, _book.BookId))
            {
                MessageBox.Show("The Image already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _book.Quantity = quantity;
            _book.Image = txtImageName.Text;
            _book.AuthorId = (int)cbAuthor.SelectedValue;
            _book.CategoryId = (int)cbCategory.SelectedValue;       
            
            if (cbStatus.SelectedItem != null && cbStatus.SelectedItem is ComboBoxItem selectedStatusItem)
            {
                var status = selectedStatusItem.Tag.ToString();
                MessageBox.Show(status);
                int key;
                switch (status)
                {
                    case "Available":
                        key = (int)BookStatus.Available;
                      
                        break;             
                    case "Unavailable":
                        key = (int)BookStatus.Unavailable;
                     
                        break;
                    default:
                        key = int.MinValue;
                        break;
                }
                _book.Status = key;
                ManagerBook.Instance.UpdateBook(_book);
                MessageBox.Show("Book updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Please select a status.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
          

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, locationFile, txtImageName.Text);
                System.IO.File.Copy(imagePath, destinationPath, true);
            }

           
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthor addAuthor = new AddAuthor();
            addAuthor.ShowDialog();
            LoadAuthors();
            LoadCategories();
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.ShowDialog();
            LoadAuthors();
            LoadCategories();
        }
    }
}
