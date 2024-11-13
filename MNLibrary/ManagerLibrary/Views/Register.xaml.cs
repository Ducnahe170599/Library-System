using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;

namespace ManagerLibrary.Views
{
    public partial class Register : Window
    {
        private Account _account = new Account();
        private bool _isRegistrationSuccessful = false;

        public Register()
        {
            InitializeComponent();
            this.DataContext = _account;
            this.Closing += Register_Closed;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
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

                var newAccount = new Account
                {
                    UserName = _account.UserName,
                    Email = _account.Email,
                    Password = password,
                    RoleId = 2
                };

                ManagerAccount.Instance.AddAccount(newAccount);

                // Get the new account's UserId
                var createdAccount = context.Accounts.SingleOrDefault(u => u.UserName == newAccount.UserName);

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

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    _isRegistrationSuccessful = true;
                    this.Close();
                }
            }
        }

        private void Register_Closed(object sender, EventArgs e)
        {
          
            Login loginWindow = new Login();
            loginWindow.Show();
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
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
