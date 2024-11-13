using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ManagerLibrary.Views.client
{
    public partial class RegisterBorrow : Window
    {
        private Account _loggedInUser;
        private Book _book;
        private BorrowRecord _borrow = new BorrowRecord();

        public RegisterBorrow(Account loggedInUser, dynamic book)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _book = new Book
            {   Isbn = book.Isbn,
                Title = book.Title,
                BookId = book.BookId
            };

            LoadTitle();

        }
        public void LoadTitle()
        {
            txtTitle.Text = _book.Title;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!dpDueDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày hẹn trả.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime dueDate = dpDueDate.SelectedDate.Value;
                DateTime maxDueDate = DateTime.Now.AddDays(14);
                DateTime minDueDate = DateTime.Now.AddDays(0);
                if (dueDate < minDueDate)
                {
                    MessageBox.Show("Ngày hẹn ít nhất 1 ngày kể từ ngày mượn !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dueDate > maxDueDate)
                {
                    MessageBox.Show("Ngày hẹn trả không được vượt quá 14 ngày từ ngày mượn !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                _borrow.DueDate = DateOnly.Parse(dpDueDate.Text);
                _borrow.Quantity = 1;
                _borrow.UserId = _loggedInUser.UserId;
                _borrow.Status = (int)OrderStatus.Submitted;
                _borrow.Isbn = _book.Isbn;
                _borrow.Notes = tbNotes.Text;


                ManagerBorrow.Instance.AddBorrowRecord(_borrow);
                ManagerBook.Instance.DecreaseBookQuantity(_book.BookId, _borrow.Quantity);


                MessageBox.Show("Đăng kí mượn sách thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;
            this.Close();
        }

       
    }
}
