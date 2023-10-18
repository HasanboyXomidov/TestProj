using BurgerMenu_Desktop.Entities.Users;
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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {

        private readonly IUserRepository _userRepository;
        private string passwordShower { get; set; }
        private string passwordShower2 { get; set; }
        public RegisterPage()
        {
            InitializeComponent();
            this._userRepository = new UserRepository();

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
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Block the space key input
            }
        }
        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            // For Close Register Window
            
            NavigationService?.Navigate(new LoginPage());
            
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
        public static bool ContainsBigCharactersAndDigits(string s)
        {
            bool hasUppercase = false;
            bool hasDigit = false;

            foreach (char c in s)
            {
                if (char.IsUpper(c))
                    hasUppercase = true;

                if (char.IsDigit(c))
                    hasDigit = true;

                if (hasUppercase && hasDigit)
                    return true;
            }

            return false;
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
            if (textboxParol2.Visibility == Visibility.Visible) passwordShower2 = textboxParol2.Password;
            else passwordShower2 = textboxParolText2.Text;

            if (tbUsername.Text.Length > 0 && passwordShower.Length > 0 && passwordShower2.Length > 0)
            {
                var dbResultCheckUserName = await _userRepository.GetUserByUserName(tbUsername.Text);

                int count = 0;
                if (ContainsBigCharactersAndDigits(passwordShower) == true) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Пожалуйста, сделайте свой пароль надежным\r\nПример: Qqww1122", NotificationType.Notification, "WindowArea");
                }
                if (ContainsNonLatinCharacters(passwordShower) == true && ContainsNonLatinCharacters(tbUsername.Text) == true) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Только латинский алфавит!!", NotificationType.Warning, "WindowArea");
                }
                if (dbResultCheckUserName.Count == 0) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Пользователь уже существует", NotificationType.Warning, "WindowArea");
                }
                if (tbUsername.Text.Length >= 8) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Совет!", "Имя пользователя недействительно, пожалуйста, проверьте минимум : 8 символов", NotificationType.Notification, "WindowArea");
                }
                if (passwordShower.Length >= 8) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Совет!", "Пароль недействителен, пожалуйста, проверьте : минимум 8 символов", NotificationType.Notification, "WindowArea");
                }
                if (passwordShower==passwordShower2) count++;
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Предупреждение!", "Пароль не соответствует старому паролю, проверьте", NotificationType.Notification, "WindowArea");
                }
                if (count  == 6)
                {
                    string UserName = tbUsername.Text;

                    string UserPassword;

                    UserPassword = passwordShower;
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.Save();
                    var hashResult = Hasher.Hash(UserPassword);

                    User user = new User();
                    user.UserName = UserName;
                    user.PasswordHash = hashResult.PasswordHash;
                    user.Salt = hashResult.Salt;

                    try
                    {
                        var dbResult = await _userRepository.CreateAsync(user);
                        if (dbResult > 0)
                        {
                            //MessageBox.Show("Успешная регистрация");
                            var notificationManager = new NotificationManager();
                            notificationManager.Show("Успешная!", "Успешная регистрация", NotificationType.Success, "WindowArea");
                            LoginPage loginPage = new LoginPage();
                            loginPage.setData(UserName, UserPassword);
                            NavigationService?.Navigate(loginPage);

                            //LoginWindow loginWindow = new LoginWindow();
                            //loginWindow.setData(UserName, UserPassword);
                            //loginWindow.Show();
                        }
                        else
                        {
                            var notificationManager = new NotificationManager();
                            notificationManager.Show("Предупреждение!", "Не зарегистрирован", NotificationType.Warning, "WindowArea");
                        }
                    }
                    catch
                    {
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Ошибка!", "Ошибка Что-то не так", NotificationType.Error, "WindowArea");
                    }
                }
            }
            else
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Предупреждение!", "пожалуйста, заполните поле", NotificationType.Notification, "WindowArea");
            }


        }

        private void showPassword_Click2(object sender, RoutedEventArgs e)
        {
            if (textboxParolText2.Visibility == Visibility.Collapsed)
            {
                //Second Passwordbox
                showPassword2.Style = (Style)FindResource("showPasswordCrosButton2");
                textboxParolText2.Text = textboxParol2.Password;
                textboxParol2.Visibility = Visibility.Collapsed;
                textboxParolText2.Visibility = Visibility.Visible;
            }
            else
            {
                //Second Passwordbox
                showPassword2.Style = (Style)FindResource("showPasswordButton2");
                textboxParol2.Password = textboxParolText2.Text;
                textboxParolText2.Visibility = Visibility.Collapsed;
                textboxParol2.Visibility = Visibility.Visible;
            }
        }
    }
}
