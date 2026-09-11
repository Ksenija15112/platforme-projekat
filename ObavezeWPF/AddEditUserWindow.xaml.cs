using ObavezeWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for AddEditUserWindow.xaml
    /// </summary>
    public partial class AddEditUserWindow : Window
    {
        bool IsEdit = false;
        public AddEditUserWindow()
        {
            InitializeComponent();
            Model.User user1 = new Model. User();
            DataContext = user1;
        }
        public AddEditUserWindow(Model.User user)
        {
            InitializeComponent();
            DataContext = user;
            IsEdit = true;

            if (!string.IsNullOrWhiteSpace(user.ImagePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(user.ImagePath);
                bitmap.EndInit();

                slikaKorisnika.Source = bitmap;
            }


        }
        private void dugmeSave_Click(object sender, RoutedEventArgs e)
        {
            Model.User user2 = DataContext as Model.User;
            //validacija

            if (string.IsNullOrWhiteSpace(user2.Name))
            {
                MessageBox.Show("Name has not been entered.");
                return;
            }
            if (string.IsNullOrWhiteSpace(user2.Email))
            {
                MessageBox.Show("Email has not been entered.");
                return;
            }
            if (!user2.Email.Contains("@"))
            {
                MessageBox.Show("Email format is not valid.");
                return;
            }


            Repository.UserRepository userrepository = new UserRepository();
            Service.UserService userservice = new Service.UserService(userrepository);

           


            if (!IsEdit)
            {
                Model.User? rezultatPretrageEmail = userservice.PronadjiPoEmailu(user2.Email);
                if (rezultatPretrageEmail != null)
                {
                    MessageBox.Show("A user with this email already exists.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(unosPassword.Password))
                {
                    MessageBox.Show("Password has not been entered");
                    return;
                }
                else
                {
                    user2.Password = unosPassword.Password;
                    //hash
                    string hashedPassword =BCrypt.Net.BCrypt.HashPassword(user2.Password);
                    user2.Password = hashedPassword;
                    userservice.Dodaj(user2);
                }
                
            }
            else
            {
                //sprecava da dva korisnika imaju isti email
                Model.User? rezultatPretrageEmail = userservice.PronadjiPoEmailu(user2.Email);
                if (rezultatPretrageEmail!=null && user2.Id!=rezultatPretrageEmail.Id)
                {
                    MessageBox.Show("This email is already in use.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(unosPassword.Password))
                {

                    user2.Password = unosPassword.Password;
                    //hash
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user2.Password);
                    user2.Password = hashedPassword;

                }

                userservice.Izmeni(user2);
                
            }
            Close();
        }

        private void dugmeZaSliku_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            var rezultat = openFileDialog.ShowDialog();
            if (rezultat == true)
            {
                Model.User user = DataContext as Model.User;
                user.ImagePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(user.ImagePath);
                bitmap.EndInit();
                slikaKorisnika.Source = bitmap;
            }
        }
    }
}
