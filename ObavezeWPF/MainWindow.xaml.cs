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

            DataContext = this;


        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addEditTaskWindow = new AddEditTaskWindow();
            var changesMade = addEditTaskWindow.ShowDialog();
            if (changesMade == true)
            {
                TasksDG.ItemsSource = taskService.GetAll();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TasksDG.SelectedItem as Model.Task;
            if (selectedItem != null)
            {
                var addEditTaskWindow = new AddEditTaskWindow(selectedItem);
                var changesMade = addEditTaskWindow.ShowDialog();
                if (changesMade == true)
                {
                    TasksDG.ItemsSource = taskService.GetAll();
                }
            }
        }
    }
}