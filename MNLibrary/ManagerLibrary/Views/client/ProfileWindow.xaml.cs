using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace ManagerLibrary.Views.client
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private Account _loggedInUser;
        private Profile _userProfile;

        public ProfileWindow(Account loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            LoadProfile();
            this.Closing += Closed_ProfileWindow;
        }

        private void Closed_ProfileWindow(object sender, EventArgs e)
        {
            Home homeWindow = new Home(_loggedInUser);
            homeWindow.Show();
            this.Hide();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Home homeWindow = new Home(_loggedInUser);
            homeWindow.Show();
            this.Hide();
        }

        private void LoadProfile()
        {
            try
            {
                using (var context = new MnlibraryContext()) {
                    _userProfile = context.Profiles.SingleOrDefault(p => p.UserId == _loggedInUser.UserId);
                   if(_userProfile != null)
                    {
                        this.DataContext = _userProfile;
                      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new MnlibraryContext())
                {
                  
                   if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
                        {
                            MessageBox.Show("You should update required field information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                  
                       _userProfile.DateOfBirth = DateOnly.Parse(dpDob.Text);                   
                        context.Entry(_userProfile).State = EntityState.Modified;
                        context.SaveChanges();
                        MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadProfile();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePass cp = new ChangePass(_loggedInUser);
            cp.ShowDialog();
        }
    }
}
