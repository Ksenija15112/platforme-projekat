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

namespace ObavezeWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void dugmeLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = unosEmail.Text;
            string password = unosPassword.Password;
            Repository.UserRepository userRepository = new Repository.UserRepository();
            Service.UserService userService = new Service.UserService(userRepository);
            Model.User? user=userService.PronadjiPoEmailu(email);
            if (user == null)
            {
                MessageBox.Show("User not found");
                return;
            }
           
            bool validno = BCrypt.Net.BCrypt.Verify(password,user.Password);
            if (validno == true)
            {
                MainWindow otvori = new MainWindow(user);
                otvori.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect password");
            }
        }
    }
}
