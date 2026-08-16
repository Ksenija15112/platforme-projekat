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
using System.Collections.ObjectModel;
using ObavezeWPF.Service;

namespace ObavezeWPF
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        ObservableCollection<Model.User> users;
        Repository.UserRepository userRepository;
        Service.UserService objekatService;
       
        public UsersWindow()
        {
            InitializeComponent();
            users = new ObservableCollection<Model.User>();
            userstabela.ItemsSource = users;
            userRepository = new Repository.UserRepository();
            objekatService = new Service.UserService(userRepository);
            OsveziKorisnike();
        }
        public void OsveziKorisnike()
        {
            users.Clear();
            var lista = objekatService.Iscitaj();
            foreach (var i in lista)
            {
                users.Add(i);
            }
        }
        private void dugmeAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditUserWindow otvori = new AddEditUserWindow();

            otvori.ShowDialog();
            OsveziKorisnike();

        }

        private void dugmeEdit_Click(object sender, RoutedEventArgs e)
        {
            Model.User izabraniUser = userstabela.SelectedItem as Model.User;
            

            if (izabraniUser != null)
            {
                AddEditUserWindow otvori = new AddEditUserWindow(izabraniUser);
                otvori.ShowDialog();
                OsveziKorisnike();

            }
            else
            {
                MessageBox.Show("You haven't selected a user. Please try again.");
            }
        }

        private void dugmeDelete_Click(object sender, RoutedEventArgs e)
        {
            Model.User izabraniUser = userstabela.SelectedItem as Model.User;
            
            if (izabraniUser != null)
            {
                objekatService.Obrisi(izabraniUser.Id);
                users.Remove(izabraniUser);
            }
            else
            {
                MessageBox.Show("You haven't selected a user. Please try again.");
            }
        }
    }
}
