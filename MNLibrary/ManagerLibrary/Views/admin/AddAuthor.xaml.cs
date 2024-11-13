using System.Windows;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;

namespace ManagerLibrary.Views.admin
{
    /// <summary>
    /// Interaction logic for AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : Window
    {
        public AddAuthor()
        {
            InitializeComponent();
            //this.Closing += AddAuthor_Closed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string authorName = txtAuthorName.Text.Trim();

            if (string.IsNullOrEmpty(authorName))
            {
                MessageBox.Show("Author name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ManagerAuthor.Instance.AuthorExists(authorName))
            {
                MessageBox.Show("Author name already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Author newAuthor = new Author
            {
                AuthorName = authorName
            };

            ManagerAuthor.Instance.AddAuthor(newAuthor);
            MessageBox.Show("Author added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }
        private void AddAuthor_Closed(object sender, EventArgs e)
        {

            AddBookDialog addBook = new AddBookDialog();
            addBook.ShowDialog();
            
        }
    }
}
