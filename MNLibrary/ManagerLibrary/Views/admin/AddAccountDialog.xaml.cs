using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;

namespace ManagerLibrary.Views.admin
{
    public partial class AddAccountDialog : Window
    {
        private Account _account = new Account();
     
        public AddAccountDialog()
        {
            InitializeComponent();
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
       

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Password;
            string confirmPassword = txtPasswordC.Password;

            if (string.IsNullOrWhiteSpace(_account.UserName) || string.IsNullOrWhiteSpace(_account.Email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    var existingUserByUsername = context.Accounts.SingleOrDefault(u => u.UserName == _account.UserName);
                    if (existingUserByUsername != null)
                    {
                        MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var existingUser = context.Accounts.SingleOrDefault(u => u.Email == _account.Email);
                    if (existingUser != null)
                    {
                        MessageBox.Show("Email already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    _account.Password = password;
                    _account.RoleId = (int)cbRoleId.SelectedValue;

                    ManagerAccount.Instance.AddAccount(_account);


                    // Get the new account's UserId
                    var createdAccount = context.Accounts.SingleOrDefault(u => u.UserName == _account.UserName);

                    if (createdAccount != null)
                    {
                        string readerCode = GenerateReaderCode(context);
                        var newProfile = new Profile
                        {
                            UserId = createdAccount.UserId,
                            FullName = "",
                            ReaderCode = readerCode
                        };

                        context.Profiles.Add(newProfile);
                        context.SaveChanges();

                        MessageBox.Show("Account added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private string GenerateReaderCode(MnlibraryContext context)
        {
            var lastProfile = context.Profiles.OrderByDescending(p => p.ProfileId).FirstOrDefault();
            if (lastProfile != null && !string.IsNullOrEmpty(lastProfile.ReaderCode))
            {
                var lastCode = lastProfile.ReaderCode.Substring(2); // Remove "HE"
                if (int.TryParse(lastCode, out int lastNumber))
                {
                    return "HE" + (lastNumber + 1).ToString("D4");
                }
            }
            return "HE0001"; // Default if no profiles exist
        }
    }
}
