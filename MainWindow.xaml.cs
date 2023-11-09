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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            if (username == "admin" && password == "admin")
            {
                errorTextBlock.Visibility = Visibility.Hidden;
                MessageBox.Show("Welcome!");
            }
            else if (username.Length == 0 && password.Length == 0)
            {
                errorTextBlock.Text = "Error! Username and password is empty";
                errorTextBlock.Visibility = Visibility.Visible;
                usernameTextBox.Background = System.Windows.Media.Brushes.IndianRed;
                passwordBox.Background = System.Windows.Media.Brushes.IndianRed;
            }
            else if (username.Length == 0)
            {
                errorTextBlock.Text = "Error! Username is empty";
                errorTextBlock.Visibility = Visibility.Visible;
                usernameTextBox.Background = System.Windows.Media.Brushes.IndianRed;
                passwordBox.Background = System.Windows.Media.Brushes.White;
            }
            else if (password.Length == 0)
            {
                errorTextBlock.Text = "Error! Password is empty";
                errorTextBlock.Visibility = Visibility.Visible;
                usernameTextBox.Background = System.Windows.Media.Brushes.White;
                passwordBox.Background = System.Windows.Media.Brushes.IndianRed;
            }
            else if (username.Length < 5)
            {
                errorTextBlock.Text = "Error! Username need have 5 symbols";
                errorTextBlock.Visibility = Visibility.Visible;
                usernameTextBox.Background = System.Windows.Media.Brushes.IndianRed;
                passwordBox.Background = System.Windows.Media.Brushes.White;
            }
            else
            {
                errorTextBlock.Text = "Error! Uncorrect username or password";
                errorTextBlock.Visibility = Visibility.Visible;
                usernameTextBox.Background = System.Windows.Media.Brushes.IndianRed;
                passwordBox.Background = System.Windows.Media.Brushes.IndianRed;
            }
        }

        private void CancelButton_Click(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
