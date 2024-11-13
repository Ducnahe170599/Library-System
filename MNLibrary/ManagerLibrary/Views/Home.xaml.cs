using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using ManagerLibrary.Views.admin;
using ManagerLibrary.Views.client;

namespace ManagerLibrary.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private Account _loggedInUser;
        private DispatcherTimer autoScrollTimer;
        private string locationFile = "C:\\Users\\nguye\\OneDrive\\Desktop\\MNLibrary\\ManagerLibrary\\Images\\book_image\\";
        public Home(Account loggedInUser )
        {

            InitializeComponent();
            _loggedInUser = loggedInUser;
            UpdateLoginState();       
            LoadCategories();
            GetNewBook();
            StartAutoScroll();
            this.Closing += Home_Closing;
        }
        private void UpdateLoginState()
        {
            if (_loggedInUser != null)
            {
                txtUserName.Text = $"{_loggedInUser.UserName}";
                txtUserName.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Collapsed;           
                imgUser.Visibility = Visibility.Visible;
               

                if (_loggedInUser.RoleId == 1) //  admin
                {
                    txtManagement.Visibility = Visibility.Visible;
                    btnManageUsers.Visibility = Visibility.Visible;
                    btnManageBooks.Visibility = Visibility.Visible;
                    btnManageBR.Visibility = Visibility.Visible;
                }
                
                if(_loggedInUser.RoleId == 2)
                {
                    txtUser.Visibility = Visibility.Visible;
                    btnRegistrationHistory.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txtUserName.Visibility = Visibility.Collapsed;
                btnLogout.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Visible;

                // Hide 
                txtManagement.Visibility = Visibility.Collapsed;
                txtUser.Visibility = Visibility.Collapsed;
                btnRegistrationHistory.Visibility = Visibility.Collapsed;
                btnManageUsers.Visibility = Visibility.Collapsed;
                btnManageBooks.Visibility = Visibility.Collapsed;
                btnManageBR.Visibility = Visibility.Collapsed;
                imgUser.Visibility = Visibility.Collapsed;          
              
            }
        }

      
        private void BtnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            AccountManagement accountManagement = new AccountManagement(this, _loggedInUser);
            accountManagement.Show();
            this.Hide();
        }

        private void BtnManageBooks_Click(object sender, RoutedEventArgs e)
        {
            BookManagement bookManagement = new BookManagement(this, _loggedInUser);
            bookManagement.Show();
            this.Hide();
        }
      
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _loggedInUser = null;
                UpdateLoginState();
            }
        }

        private void BtnRegistrationHistory_Click(object sender, RoutedEventArgs e)
        {

            RegistrationHistory history = new RegistrationHistory(_loggedInUser);
            history.Show();
            this.Hide();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void UserImage_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            ProfileWindow profile = new ProfileWindow(_loggedInUser);
            profile.Show();
            this.Hide();
        }

        private void btnManageBR_Click(object sender, RoutedEventArgs e)
        {
            BorrowReManagement borrowRe = new BorrowReManagement(this, _loggedInUser);
            borrowRe.Show();
            this.Hide();
        }
        private void LoadBooks(string? search = null, int? categoryId = null)
        {
            if (string.IsNullOrEmpty(search) && !categoryId.HasValue)
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm hoặc chọn thể loại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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
                                book.Status,
                                IsCheck = book.Status == (int)BookStatus.Available ? true : false,
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

                var bookList = query.ToList<object>();
                if (bookList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var searchResultsWindow = new ListBook(bookList, _loggedInUser);
                    searchResultsWindow.Show();
                    this.Hide();
                }
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

        public void GetNewBook()
        {
            using (var context = new MnlibraryContext())
            {
                DateTime sevenDaysAgo = DateTime.Now.AddDays(-200);
                var query = from book in context.Books
                            where book.CreateDate >= sevenDaysAgo
                            orderby book.CreateDate descending
                            select new
                            {
                                Title = book.Title,
                                ImagePath = locationFile + book.Image,
                            };

                var listBook = query.Take(10).ToList();
                RecentBooksListView.ItemsSource = listBook;
            }
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks(txtSearch.Text);
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategory.SelectedItem is Category selectedCategory)
            {
                LoadBooks(null, selectedCategory.CategoryId);
            }
        }
        private void StartAutoScroll()
        {
            autoScrollTimer = new DispatcherTimer();
            autoScrollTimer.Interval = TimeSpan.FromSeconds(0);
            autoScrollTimer.Tick += AutoScrollTimer_Tick;
            autoScrollTimer.Start();
        }

        private void AutoScrollTimer_Tick(object sender, EventArgs e)
        {
            if (scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth >= scrollViewer.ExtentWidth)
            {
                scrollViewer.ScrollToHorizontalOffset(0);
            }
            else
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 0.02);
            }
        }

        private void Home_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đóng ứng dụng không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

       
    }
}
