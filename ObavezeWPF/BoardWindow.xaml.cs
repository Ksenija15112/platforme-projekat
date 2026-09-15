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
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        Model.Task izabraniTask;
        Model.Category izabranaKategorija;
        Model.User ulogovaniKorisnik;


        public BoardWindow(Model.User user)
        {
            InitializeComponent();
            ulogovaniKorisnik = user;
            UcitajBoard();
           
            

            


        }

        private void UcitajBoard()
        {

            BoardPanel.Children.Clear();

            Repository.CategoryRepository categoryRepository = new Repository.CategoryRepository();
            Service.CategoryService categoryService = new Service.CategoryService(categoryRepository);
            var lista = categoryService.GetAll();
            Repository.TaskRepository taskRepository = new Repository.TaskRepository();
            Service.TaskService taskService = new Service.TaskService(taskRepository);
            var listaTaskova = taskService.GetAll();
            foreach (var i in lista)
            {
                StackPanel kolona = new StackPanel();
                Button naslov = new Button();
                naslov.Content = i.Name;
                naslov.Tag = i;
                kolona.Children.Add(naslov);
                BoardPanel.Children.Add(kolona);
                kolona.Width = 250;
                naslov.Click += Kategorija_Click;



                foreach (var j in listaTaskova)
                {
                    if (i.Id == j.CategoryId && ulogovaniKorisnik.Id==j.UserId)
                    {
                        Button kartica = new Button();
                        kartica.Content = j.Title;
                        kartica.Tag = j;
                        kolona.Children.Add(kartica);
                        kartica.Click += Kartica_Click;

                    }
                }

            }

        }

        private void Kartica_Click(object sender, RoutedEventArgs e)
        {
            Button kartica = sender as Button;
            izabraniTask = kartica.Tag as Model.Task;
        }
        private void Kategorija_Click(object sender, RoutedEventArgs e)
        {
            Button naslov = sender as Button;
            izabranaKategorija = naslov.Tag as Model.Category;

            if (izabraniTask == null)
            {
                MessageBox.Show("Please select a task first.");
                return;
            }

            izabraniTask.Category = izabranaKategorija;

            Repository.TaskRepository taskRepository = new Repository.TaskRepository();
            Service.TaskService taskService = new Service.TaskService(taskRepository);
            taskService.Save(izabraniTask);
            UcitajBoard();

        }
    }
}
