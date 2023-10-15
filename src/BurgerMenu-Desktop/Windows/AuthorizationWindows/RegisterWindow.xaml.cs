using BurgerMenu_Desktop.Entities.Users;
using BurgerMenu_Desktop.Interfaces.Users;
using BurgerMenu_Desktop.Repositories.Users;
using BurgerMenu_Desktop.Security;
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
using System.Windows.Shapes;

namespace BurgerMenu_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        private readonly IUserRepository _userRepository;
        private string passwordShower { get; set; }
        private string passwordShower2 { get; set; }


        public RegisterWindow()
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            // For Close Register Window
            LoginWindow loginWindow = new LoginWindow();    
            this.Close();
            loginWindow.ShowDialog();

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Block the space key input
            }
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
        private bool ContainsBigCharactersAndDigits(string text)
        {
            // Define the regular expression pattern to match uppercase letters and digits
            string pattern = @"[A-Z0-9]";
            Regex regex = new Regex(pattern);

            // Check if the text contains any uppercase letters or digits
            return regex.IsMatch(text);
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
                if (ContainsBigCharactersAndDigits(passwordShower) == true ) count++;
                else MessageBox.Show("Пожалуйста, сделайте свой пароль надежным\r\nПример: Qqww1122");                
                if (ContainsNonLatinCharacters(passwordShower) == true && ContainsNonLatinCharacters(tbUsername.Text) == true) count++;
                else MessageBox.Show("Только латинский алфавит!!");
                if (dbResultCheckUserName.Count == 0) count++;
                else MessageBox.Show("Пользователь уже существует");
                if (tbUsername.Text.Length >= 8) count++;
                else MessageBox.Show("Имя пользователя недействительно, пожалуйста, проверьте минимум : 8 символов");
                if (passwordShower.Length >= 8) count++;
                else MessageBox.Show("Пароль недействителен, пожалуйста, проверьте : минимум 8 символов");
                if (passwordShower==passwordShower2) count++;
                else MessageBox.Show("Пароль не соответствует старому паролю, проверьте");
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
                            MessageBox.Show("Успешная регистрация");
                            LoginWindow loginWindow = new LoginWindow();
                            loginWindow.setData(UserName, UserPassword);
                            loginWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не зарегистрирован");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка Что-то не так");
                    }                   
                }
            }
            else
            {
                MessageBox.Show("пожалуйста, заполните поле");
            }
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
