using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using ManagerLibrary.Models;
using ManagerLibrary.ModelsViews;
using Microsoft.Win32;

namespace ManagerLibrary.Views.admin
{
    public partial class AccountManagement : Window
    {
        private Home _homeWindow;
        private dynamic _selectedAccount;
        private Account _loggedInUser;

        public AccountManagement(Home homeWindow, Account loggedInUser)
        {
            InitializeComponent();
            _homeWindow = homeWindow;
            LoadAccounts();
            _loggedInUser = loggedInUser;
            this.Closing += Closed_AccountManagement;
        }

        private void LoadAccounts(string search = null)
        {
            using (var context = new MnlibraryContext())
            {
                var query = from account in context.Accounts
                            join role in context.Roles on account.RoleId equals role.RoleId
                            select new
                            {
                                account.UserId,
                                account.UserName,
                                account.Email,
                                account.RoleId,
                                role.RoleName,
                                account.Password
                            };

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.UserId.ToString().Contains(search)
                                             || s.UserName.ToLower().Contains(search.ToLower())
                                             || s.Email.ToString().Contains(search)
                                             || s.RoleName.ToLower().Contains(search.ToLower()));
                }
                

                var accountList = query.ToList();
                listViewAccounts.ItemsSource = accountList;
                txtAccountCount.Text = $"Total Accounts: {accountList.Count}";
            }
        }
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadAccounts(txtSearch.Text);
        }
    

        private void BtnAddAccount_Click(object sender, RoutedEventArgs e)
        {
           AddAccountDialog dialog = new AddAccountDialog();
            dialog.ShowDialog();
            LoadAccounts();
        }

        private void BtnUpdateAccount_Click(object sender, RoutedEventArgs e)
        {
            _selectedAccount = listViewAccounts.SelectedItem;
            if (_selectedAccount != null)
            {
                UpdateAccountDialog dialog = new UpdateAccountDialog(_selectedAccount);
                dialog.ShowDialog();
                LoadAccounts();
            }
            else
            {
                MessageBox.Show("Please select an account to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDeleteAccount_Click(object sender, RoutedEventArgs e)

        {
            _selectedAccount = listViewAccounts.SelectedItem ;
            if (_selectedAccount != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this account?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        var accountToDelete = new Account
                        {
                            UserId = _selectedAccount.UserId,

                        };
                        ManagerAccount.Instance.DeleteStudent(accountToDelete);
                        LoadAccounts();
                        MessageBox.Show("Account deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } 
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"            
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string jsonData = File.ReadAllText(openFileDialog.FileName);
                List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonData);
                try
                {
                    using (var context = new MnlibraryContext())
                    {
                        context.Accounts.AddRange(accounts);
                        context.SaveChanges();
                        MessageBox.Show("Accounts imported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadAccounts();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to import employees: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Closed_AccountManagement(object sender, EventArgs e)
        {
            _homeWindow.Show();
            
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Home homeWindow = new Home(_loggedInUser);
            homeWindow.Show();
            this.Hide();
        }
    }
}
