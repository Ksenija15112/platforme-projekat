using Microsoft.VisualBasic;
using ObavezeWPF.Repository;
using ObavezeWPF.Service;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObavezeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskService taskService;

        LabelService labelService;

        public Model.User UlogovaniUser { get; set; }

        private string titleSearchQuery = string.Empty;
        public string TitleSearchQuery
        {
            get
            { return titleSearchQuery; }
            set
            {
                titleSearchQuery = value;
                TasksView.Refresh();
            }
        }
        public ICollectionView TasksView { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            ITaskRepository taskRepository = new TaskRepository();
            taskService = new TaskService(taskRepository);

            var tasks = taskService.GetAll();
            TasksView = CollectionViewSource.GetDefaultView(tasks);
            TasksView.Filter = (task) => (task as Model.Task).Title.Contains(TitleSearchQuery ?? "");

            LabelRepository labelRepository = new LabelRepository();
            labelService = new LabelService(labelRepository);


            DataContext = this;




        }

        public MainWindow(Model.User user)
        {
            InitializeComponent();
            this.UlogovaniUser = user;
            ITaskRepository taskRepository = new TaskRepository();
            taskService = new TaskService(taskRepository);

           
            LabelRepository labelRepository = new LabelRepository();
            labelService = new LabelService(labelRepository);

            DataContext = this;
            OsveziTaskove();

        }
        private void OsveziTaskove()
        {
            List<Model.Task> tasks= taskService.GetAll();
           
            List<Model.Task> mojiTaskovi = new List<Model.Task>();
            foreach(var i in tasks)
            {
                if (i.UserId == UlogovaniUser.Id)
                {
                    mojiTaskovi.Add(i);
                }
            }
            
            TasksView = CollectionViewSource.GetDefaultView(mojiTaskovi);
            TasksView.Filter = (task) => (task as Model.Task).Title.Contains(TitleSearchQuery ?? "");
            TasksDG.ItemsSource = TasksView;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addEditTaskWindow = new AddEditTaskWindow();
            var changesMade = addEditTaskWindow.ShowDialog();
            if (changesMade == true)
            {
                OsveziTaskove();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TasksDG.SelectedItem as Model.Task;
            if (selectedItem != null)
            {
                if (UlogovaniUser.Id == selectedItem.UserId)
                {
                    var addEditTaskWindow = new AddEditTaskWindow(selectedItem);
                    var changesMade = addEditTaskWindow.ShowDialog();
                    if (changesMade == true)
                    {
                        OsveziTaskove();
                    }
                }
                
            }
            else
            {
                MessageBox.Show("You haven't selected a task.");
            }
        }
        private void labelsClick(object sender, RoutedEventArgs e)
        {
            LabelsWindow labelsWindow = new LabelsWindow();
            labelsWindow.ShowDialog();
        }

        private void usersClick(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.ShowDialog();
        }
        //delete dugme
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model.Task izabranRed = TasksDG.SelectedItem as Model.Task;

            if (izabranRed!=null)
            {
                if (UlogovaniUser.Id == izabranRed.UserId)
                {
                    taskService.Obrisi(izabranRed.Id);
                    OsveziTaskove();
                }
                
            }
            else
            {
                MessageBox.Show("You haven't selected a row");
            }
        }
    }
}