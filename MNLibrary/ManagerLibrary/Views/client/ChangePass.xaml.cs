using ManagerLibrary.Models;
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

namespace ManagerLibrary.Views.client
{
    /// <summary>
    /// Interaction logic for ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        private Account _loggedInUser;
        public ChangePass(Account loggedInUser)
        {
            InitializeComponent();
            _loggedInUser=loggedInUser;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MnlibraryContext())
            {
                var user = context.Accounts.SingleOrDefault(u => u.UserId == _loggedInUser.UserId);
                if (user != null)
                {
                    if (user.Password == OldPasswordBox.Password)
                    {
                        if (NewPasswordBox.Password == ConfirmPasswordBox.Password)
                        {
                            user.Password = NewPasswordBox.Password;
                            context.SaveChanges();
                            MessageBox.Show("Password changed successfully!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("New passwords do not match!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Old password is incorrect!");
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

