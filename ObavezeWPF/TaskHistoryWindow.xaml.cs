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
    /// Interaction logic for TaskHistoryWindow.xaml
    /// </summary>
    public partial class TaskHistoryWindow : Window
    {
        public TaskHistoryWindow(Model.Task task)
        {
            InitializeComponent();
            DataContext = task;

            Repository.TaskEventRepository taskEventRepository = new Repository.TaskEventRepository();
            Service.TaskEventService taskEventService = new Service.TaskEventService(taskEventRepository);
            TaskHistoryDG.ItemsSource=taskEventService.GetAll(task.Id);
        }
        //close
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
