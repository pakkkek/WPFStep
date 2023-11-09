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
using WpfApp1.Domain;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        UsersList list;
        public MainWindow()
        {
            list = new UsersList();
            list.AddUser(new User("Alexander", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Aleksey", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Egor", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Oleg", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Alexander", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Aleksey", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Egor", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Oleg", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Alexander", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Aleksey", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Egor", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            list.AddUser(new User("Oleg", @"C:\Users\Kpojl\Source\Repos\WpfApp1\Assets\user-icon.jpg", "+380930000000"));
            InitializeComponent();
            LVUsers.ItemsSource = list.users;
        }
    }
}
