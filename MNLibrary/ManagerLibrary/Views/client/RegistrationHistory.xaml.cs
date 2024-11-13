using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using ManagerLibrary.Views.admin;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ManagerLibrary.Views.client
{
    /// <summary>
    /// Interaction logic for RegistrationHistory.xaml
    /// </summary>
    public partial class RegistrationHistory : Window
    {
        private Account _loggedInUser;

        private string locationFile = "E:\\dotritrong\\C#\\MNLibrary\\ManagerLibrary\\Images\\book_image\\";
        public RegistrationHistory(Account loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            LoadHistory();
        }

        private void LoadHistory(string? search = null, string? status = null)
        {
            using (var context = new MnlibraryContext())
            {
                var query = from br in context.BorrowRecords
                            join book in context.Books on br.Isbn equals book.Isbn
                            where br.UserId == _loggedInUser.UserId
                            select new
                            {
                                Title = book.Title,
                                Image = locationFile + book.Image,
                                Isbn = book.Isbn,
                                Quantity = br.Quantity,
                                BorrowDate = br.BorrowDate,
                                br.BorrowId,
                                DueDate = br.DueDate,
                                UserId = br.UserId,
                                ReturnDate = br.ReturnDate,
                                StatusDisplay = br.StatusDisplay,
                                Status = br.Status,
                                Enable = br.Status == (int)OrderStatus.Submitted ? true : false,
                            };

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.Title.ToLower().Contains(search.ToLower())
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

                var historyList = query.ToList();
                lvRegistrationHistory.ItemsSource = historyList;
            }
        }


        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadHistory(txtSearch.Text);
        }


        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadHistory(null, ((ComboBoxItem)cbStatus.SelectedItem)?.Tag.ToString());

        }
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Home homeWindow = new Home(_loggedInUser);
            homeWindow.Show();
            this.Hide();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Home home = new Home(_loggedInUser);
            home.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var borrowId = (int)button.Tag;
            BorrowRecord borrowRecord;

            try
            {
                using (var context = new MnlibraryContext())
                {
                    
                    var borrow = (from bd in context.BorrowRecords                               
                                  where bd.BorrowId == borrowId
                                  select new { 
                                      BorrowRecord = bd                                
                                                              
                                      }).FirstOrDefault();

                    if (borrow == null)
                    {
                        MessageBox.Show("Borrow record not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    borrowRecord = borrow.BorrowRecord;
                    var result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        ManagerBorrow.Instance.DeleteBorrow(borrowRecord);
                        ManagerBook.Instance.IncreaseBookQuantity(borrowRecord.Isbn, borrowRecord.Quantity);                    
                        MessageBox.Show("Borrow record deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadHistory();
                    }
  
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

    
