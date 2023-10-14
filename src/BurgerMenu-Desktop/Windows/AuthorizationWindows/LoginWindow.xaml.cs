
using System.Configuration;

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
using BurgerMenu_Desktop.Interfaces.Users;
using BurgerMenu_Desktop.Repositories.Users;
using BurgerMenu_Desktop.Security;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;

namespace BurgerMenu_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindo.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string registeredUsername { get; set; }
        private string registeredPassword { get; set; }
        private string passwordShower { get; set; }
        private readonly IUserRepository _userRepository;
        public LoginWindow()
        {            
            InitializeComponent();
            this._userRepository = new UserRepository();


            // Load the user's preference from application settings
            if (Properties.Settings.Default.RememberMe)
            {
                // Check Checkbox 
                textboxParol.Password = Properties.Settings.Default.Password;
                tbUsername.Text = Properties.Settings.Default.Username;
                rememberMeCheckBox.IsChecked = true;
            }
            else { rememberMeCheckBox.IsChecked = false; }

            
            
        }



        private void showPassword_Click(object sender, RoutedEventArgs e)
        {
            if (textboxParolText.Visibility == Visibility.Collapsed)
            {
                showPassword.Style = (Style)FindResource("showPasswordCrosButton");
                textboxParolText.Text = textboxParol.Password;
                textboxParol.Visibility = Visibility.Collapsed;
                textboxParolText.Visibility = Visibility.Visible;
            }
            else
            {
                showPassword.Style = (Style)FindResource("showPasswordButton");
                textboxParol.Password = textboxParolText.Text;
                textboxParolText.Visibility = Visibility.Collapsed;
                textboxParol.Visibility = Visibility.Visible;
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }
        public bool ContainsNonLatinCharacters(string input)
        {            
            string latinAlphaNumericPattern = @"^[a-zA-Z0-9]+$";
            // Check if the input contains any Latin alphabets
            return Regex.IsMatch(input, latinAlphaNumericPattern);
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (textboxParol.Visibility == Visibility.Visible) passwordShower = textboxParol.Password;
            else passwordShower = textboxParolText.Text;
            // For validation 
            int count = 0;
            string name = tbUsername.Text;
            var dbResult = await _userRepository.GetUserByUserName(name);
            if (dbResult.Count == 1)
            {
                string passWordHash = dbResult[0].PasswordHash;
                string salt = dbResult[0].Salt;
                var checkPassword = Hasher.Verify(passwordShower, passWordHash, salt);
                if (checkPassword == true) count++;
            }
            else MessageBox.Show("User not found");
            if (ContainsNonLatinCharacters(tbUsername.Text) == true && ContainsNonLatinCharacters(passwordShower) == true ) count++;            
            else MessageBox.Show("Only latin alphabit !!");
            if ( tbUsername.Text.Length > 8 ) count++;
            else MessageBox.Show("Username is invalid  ,  please check ");
            if (passwordShower.Length > 8) count++;
            else MessageBox.Show("Password is invalid , please check ");
            if ( count  == 4 )
            {
                if (rememberMeCheckBox.IsChecked == true)
                {
                    // Store the user's preference in application settings
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Password = passwordShower;
                    Properties.Settings.Default.Username = tbUsername.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Success");
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                }
                else
                {
                    // Clear the user's preference in application settings
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Success");
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                }
            }
        }
        public void setData(string userName,string userPassword)
        {
            registeredUsername = userName;
            registeredPassword = userPassword;
        }
        
        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            // For Open Register Window
            RegisterWindow regiterWindow = new RegisterWindow();

            // For Login Window Hide
            this.Close();
            regiterWindow.ShowDialog();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = tbUsername.Text + e.Text;

            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    e.Handled = true; // Block input if it is not a letter or digit, or if it is whitespace
                    break;
                }
            }
            
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Block the space key input
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (registeredUsername != null && registeredPassword != null)
            {
                tbUsername.Text = registeredUsername;
                textboxParol.Password = registeredPassword;
            }
        }
    }
}
