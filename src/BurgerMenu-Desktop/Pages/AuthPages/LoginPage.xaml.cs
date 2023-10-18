using BurgerMenu_Desktop.Interfaces.Users;
using BurgerMenu_Desktop.Repositories.Users;
using BurgerMenu_Desktop.Security;
using BurgerMenu_Desktop.Windows;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BurgerMenu_Desktop.Pages.AuthPages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string registeredUsername { get; set; }
        private string registeredPassword { get; set; }
        private string passwordShower { get; set; }
        private long UserId { get; set; }
        private readonly IUserRepository _userRepository;
        public LoginPage()
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
        public void setData(string userName, string userPassword)
        {
            registeredUsername = userName;
            registeredPassword = userPassword;
        }

        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            // For Open Register page
            NavigationService?.Navigate(new RegisterPage());
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

            if (passwordShower.Length > 0 && tbUsername.Text.Length > 0)
            {
                // For validation 
                int count = 0;
                string name = tbUsername.Text;
                var dbResult = await _userRepository.GetUserByUserName(name);
                if (dbResult.Count == 1)
                {
                    string passWordHash = dbResult[0].PasswordHash;
                    string salt = dbResult[0].Salt;
                    UserId = dbResult[0].Id;
                    var checkPassword = Hasher.Verify(passwordShower, passWordHash, salt);
                    if (checkPassword == true) count++;
                    else
                    {
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Предупреждение!", "Неверный пароль", NotificationType.Warning, "WindowArea");
                    }
                }
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("внимание!", "Пользователь не найден", NotificationType.Warning, "WindowArea");
                }
                if (ContainsNonLatinCharacters(tbUsername.Text) == true && ContainsNonLatinCharacters(passwordShower) == true) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Только латинский алфавит!!", NotificationType.Warning, "WindowArea");
                }
                if (passwordShower.Length >= 8) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Имя пользователя недействительно, пожалуйста, проверьте", NotificationType.Warning, "WindowArea");
                }
                if (passwordShower.Length >= 8) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Пароль недействителен, пожалуйста, проверьте", NotificationType.Warning, "WindowArea");
                }
                if (count  == 4)
                {
                    if (rememberMeCheckBox.IsChecked == true)
                    {
                        // Store the user's preference in application settings
                        Properties.Settings.Default.RememberMe = true;
                        Properties.Settings.Default.Password = passwordShower;
                        Properties.Settings.Default.Username = tbUsername.Text;
                        Properties.Settings.Default.UserId = UserId;
                        Properties.Settings.Default.Save();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                    }
                    else
                    {
                        // Clear the user's preference in application settings
                        Properties.Settings.Default.RememberMe = false;
                        Properties.Settings.Default.UserId = UserId;
                        Properties.Settings.Default.Save();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                    }
                }
            }
            else
            {
                //MessageBox.Show("пожалуйста, заполните поле");
                var notificationManager = new NotificationManager();
                notificationManager.Show("Предупреждение!", "пожалуйста, заполните поле", NotificationType.Notification, "WindowArea");
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (registeredUsername != null && registeredPassword != null)
            {
                tbUsername.Text = registeredUsername;
                textboxParol.Password = registeredPassword;
            }
        }
    }
}
