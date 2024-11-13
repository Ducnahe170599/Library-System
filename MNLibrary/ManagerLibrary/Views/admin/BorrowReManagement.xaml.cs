using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace ManagerLibrary.Views.admin
{
    public partial class BorrowReManagement : Window
    {
        private Account _loggedInUser;
        private List<object> listBorrow;
        private Home _homeWindow;
        private string locationFile = "C:\\Users\\nguye\\OneDrive\\Desktop\\MNLibrary\\ManagerLibrary\\Images\\book_image\\";

        public BorrowReManagement(Home homeWindow, Account loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _homeWindow = homeWindow;
            LoadData();
            DeleteOldRecords();
            UpdateOverdueRecords();
        }

        private void LoadData(string? search = null, string? status = null)
        {
            using (var context = new MnlibraryContext())
            {
                var query = from bd in context.BorrowRecords
                            join book in context.Books on bd.Isbn equals book.Isbn
                            join profile in context.Profiles on bd.UserId equals profile.UserId
                            select new
                            {
                                ReaderCode = profile.ReaderCode,
                                Image = locationFile + book.Image,
                                Isbn = book.Isbn,
                                bd.BorrowId,
                                Quantity = bd.Quantity,
                                BorrowDate = bd.BorrowDate,
                                DueDate = bd.DueDate,
                                UserId = bd.UserId,
                                ReturnDate = bd.ReturnDate,
                                StatusDisplay = bd.StatusDisplay,
                                Status = bd.Status,
                                Enable = bd.Status == (int)OrderStatus.Returned || bd.Status == (int)OrderStatus.Reject ? false : true,
                            };

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.ReaderCode.ToLower().Contains(search.ToLower())
                                             || s.Isbn.ToLower().Contains(search.ToLower()));
                }

                if (!string.IsNullOrEmpty(status) && status != "All")
                {
                    int key;
                    switch (status)
                    {
                        case "Borrowed":
                            key = (int)OrderStatus.Borrowed;
                            break;
                        case "Returned":
                            key = (int)OrderStatus.Returned;
                            break;
                        case "Overdue":
                            key = (int)OrderStatus.Overdue;
                            break;
                        case "Submitted":
                            key = (int)OrderStatus.Submitted;
                            break;
                        case "Confirmed":
                            key = (int)OrderStatus.Confirmed;
                            break;
                        case "Cancelled":
                            key = (int)OrderStatus.Reject;
                            break;
                        default:
                            key = int.MinValue;
                            break;
                    }

                    query = query.Where(s => s.Status == key);
                }

                var dataList = query.ToList();
                lvData.ItemsSource = dataList;
            }
        }

          

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(txtSearch.Text, null);
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData(null, ((ComboBoxItem)cbStatus.SelectedItem)?.Tag.ToString());
        }



        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var borrowId = (int)button.Tag;

            using (var context = new MnlibraryContext())
            {
                var borrow = (from bd in context.BorrowRecords                            
                              where bd.BorrowId == borrowId
                              select bd).FirstOrDefault();

                if (borrow != null)
                {
                    UpdateStatusDialog dialog = new UpdateStatusDialog(borrow);
                    if (dialog.ShowDialog() == true)
                    {
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("No borrow details found for the selected item.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteOldRecords()
        {
            using (var context = new MnlibraryContext())
            {
                var thresholdDate = DateOnly.FromDateTime(DateTime.Now);
                var oldRecords = context.BorrowRecords
                    .Where(br => (br.ReturnDate != null && br.ReturnDate.Value.AddDays(14) <= thresholdDate)
                                 || br.Status == (int)OrderStatus.Reject)
                    .ToList();

                context.BorrowRecords.RemoveRange(oldRecords);
                context.SaveChanges();
            }
        }



        private void UpdateOverdueRecords()
        {
            using (var context = new MnlibraryContext())
            {
                var thresholdDate = DateOnly.FromDateTime(DateTime.Now);
                var overdueRecords = context.BorrowRecords
                    .Where(br => br.ReturnDate == null && br.BorrowDate.AddDays(14) <= thresholdDate && br.Status != (int)OrderStatus.Overdue)
                    .ToList();

                foreach (var record in overdueRecords)
                {
                    record.Status = (int)OrderStatus.Overdue;
                }

                context.SaveChanges();
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _homeWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _homeWindow.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddBorrowDialog dialog = new AddBorrowDialog();
            //if (dialog.ShowDialog() == true)
            //{
            //    LoadData();
            //}
        }
    }
}
