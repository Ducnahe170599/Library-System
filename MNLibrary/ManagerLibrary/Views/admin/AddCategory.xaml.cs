using System.Windows;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;

namespace ManagerLibrary.Views.admin
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory()
        {
            InitializeComponent();
            //this.Closing += AddCategory_Closed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("Category name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ManagerCategory.Instance.CategoryExists(categoryName))
            {
                MessageBox.Show("Category name already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Category newCategory = new Category
            {
                CategoryName = categoryName
            };

            ManagerCategory.Instance.AddCategory(newCategory);
            MessageBox.Show("Category added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void AddCategory_Closed(object sender, EventArgs e)
        {

            AddBookDialog addBook = new AddBookDialog();
            addBook.ShowDialog();
            this.Hide();
        }
    }

}
