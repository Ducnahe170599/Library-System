using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ManagerLibrary.Views.admin
{
    /// <summary>
    /// Interaction logic for UpdateAccountDialog.xaml
    /// </summary>
    public partial class UpdateAccountDialog : Window
    {
        private Account _account;

        public UpdateAccountDialog(dynamic account)
        {
            InitializeComponent();
            _account = new Account()
            {
                UserId = account.UserId,
                UserName = account.UserName,
                Email = account.Email,
                RoleId = account.RoleId,
                Password = account.Password,
            };
            this.DataContext = _account;
            LoadRole();

        }
        public void LoadRole()
        {
            using (var context = new MnlibraryContext())
            {
                var roles = context.Roles.Select(r => new {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                }).ToList();

                cbRoleId.ItemsSource = roles;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_account.Email))
            {
                MessageBox.Show("Email is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(_account.Email))
            {
                MessageBox.Show("Invalid email format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbRoleId.SelectedValue == null)
            {
                MessageBox.Show("Please select a role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new MnlibraryContext())
                {
                    var existingUser = context.Accounts.SingleOrDefault(u => u.Email == _account.Email && u.UserId != _account.UserId);
                    if (existingUser != null)
                    {
                        MessageBox.Show("Email already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                   
                    _account.RoleId = (int)cbRoleId.SelectedValue;

                    ManagerAccount.Instance.UpdateAccount(_account);
                    MessageBox.Show("Account updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

