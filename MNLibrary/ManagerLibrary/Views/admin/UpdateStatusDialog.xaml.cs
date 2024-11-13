using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ManagerLibrary.Views.admin
{
    public partial class UpdateStatusDialog : Window
    {
        private BorrowRecord item;
        public UpdateStatusDialog(BorrowRecord borrow)
        {
            InitializeComponent();
            item = borrow;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = ((ComboBoxItem)cbNewStatus.SelectedItem)?.Tag.ToString();
            int newStatusKey;
            DateOnly dateReturn = default(DateOnly);

            // Map the selected status to the corresponding OrderStatus value
            switch (selectedStatus)
            {
                case "Borrowed":
                    newStatusKey = (int)OrderStatus.Borrowed;
                    break;
                case "Returned":
                    newStatusKey = (int)OrderStatus.Returned;
                    dateReturn = DateOnly.FromDateTime(DateTime.Now);
                    break;
                case "Overdue":
                    newStatusKey = (int)OrderStatus.Overdue;
                    break;
                case "Confirmed":
                    newStatusKey = (int)OrderStatus.Confirmed;
                    break;
                case "Reject":
                    newStatusKey = (int)OrderStatus.Reject;
                    break;
                default:
                    MessageBox.Show("Please select a valid status!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            // Check if the status update is valid
            bool isValidStatusUpdate = false;
            switch (item.Status)
            {
                case (int)OrderStatus.Submitted:
                    if (newStatusKey == (int)OrderStatus.Confirmed)
                        isValidStatusUpdate = true;
                    break;
                case (int)OrderStatus.Confirmed:
                    if (newStatusKey == (int)OrderStatus.Borrowed)
                        isValidStatusUpdate = true;
                    break;
                case (int)OrderStatus.Borrowed:
                    if (newStatusKey == (int)OrderStatus.Returned || newStatusKey == (int)OrderStatus.Overdue)
                        isValidStatusUpdate = true;
                    break;
                case (int)OrderStatus.Reject:
                    if (newStatusKey == (int)OrderStatus.Reject)
                        isValidStatusUpdate = true;
                    break;
            }

            // If the status update is valid, update the item and database
            if (isValidStatusUpdate)
            {
                item.Status = newStatusKey;
                item.ReturnDate = dateReturn;

                if (item.Status == (int)OrderStatus.Returned || item.Status == (int)OrderStatus.Reject)
                {
                    var bookId = item.Isbn;
                    var quantity = item.Quantity;
                    ManagerBook.Instance.IncreaseBookQuantity(bookId, quantity);
                }

                ManagerBorrow.Instance.UpdatBorrow(item);
                MessageBox.Show("Update status successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid status update!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
