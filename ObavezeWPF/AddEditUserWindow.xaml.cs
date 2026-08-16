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


        }
        private void dugmeSave_Click(object sender, RoutedEventArgs e)
        {
            Model.User user2 = DataContext as  Model.User;
            
            Repository.UserRepository userrepository = new UserRepository();
            Service.UserService userservice = new Service.UserService(userrepository);
            if (!IsEdit)
            {
                user2.Password = unosPassword.Password;
                userservice.Dodaj(user2);
                
            }
            else
            {
                if (unosPassword.Password != "")
                {

                    user2.Password = unosPassword.Password;

                }
                userservice.Izmeni(user2);
                
            }
            Close();
        }
    }
}
