using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ManagerLibrary.Models;

namespace ManagerLibrary.Views
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.Closing += Login_Closing;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            try
            {
                using (var context = new MnlibraryContext())
                {
                    var user = context.Accounts.SingleOrDefault(u => u.UserName == username && u.Password == password);

                    if (user != null)
                    {
                        MessageBox.Show("Đăng nhập thành công!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        Home homeWindow = new Home(user);
                        homeWindow.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Hide();
        }

        private void Login_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
