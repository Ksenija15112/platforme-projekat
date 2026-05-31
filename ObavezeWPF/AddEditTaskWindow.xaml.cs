using ObavezeWPF.Model;
using ObavezeWPF.Repository;
using ObavezeWPF.Service;
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
    /// Interaction logic for AddEditTaskWindow.xaml
    /// </summary>
    public partial class AddEditTaskWindow : Window
    {
        protected StateService stateService;
        protected CategoryService categoryService;
        protected TaskService taskService;

        //protected Model.Task? existingTask;
        public AddEditTaskWindow(Model.Task? task = null)
        {
            InitializeComponent();

            //existingTask = task;
            DataContext = task != null ? task.Clone() : new Model.Task();

            var stateRepo = new StateRepository();
            stateService = new StateService(stateRepo);
            var categoryRepo = new CategoryRepository();
            categoryService = new CategoryService(categoryRepo);
            var taskRepo = new CSVTaskRepository();
            taskService = new TaskService(taskRepo);

            PopulateForm();
        }

        private void PopulateForm()
        {
            // Popuniti State i Category ComboBoxes
            // Ako je edit u pitanju, postaviti vrednosti...
        
            CategoryCB.ItemsSource = categoryService.GetAll();
            StateCB.ItemsSource = stateService.GetAll();
            
            /*var states = stateService.GetAll();
            foreach (var state in states)
            {
                var cBItem = new ComboBoxItem();
                cBItem.Content = state.Name;
                cBItem.Tag = state.Id;
                StateCB.Items.Add(cBItem);
            }

            var categories = categoryService.GetAll();
            foreach(var category in categories)
            {
                var cBItem = new ComboBoxItem();
                cBItem.Content = category.Name;
                cBItem.Tag = category.Id;
                CategoryCB.Items.Add(cBItem);
            }*/

/*
            if (existingTask != null)
            {
                IdTB.Text = existingTask.Id.ToString();
                TitleTB.Text = existingTask.Title;
                DescriptionTB.Text = existingTask.Description;
                DueDateDP.Text = existingTask.DueDateTime.ToString();

                var priorityCBItems = PriorityCB.Items.Cast<ComboBoxItem>();
                foreach (var priorityCBItem in priorityCBItems)
                {
                    if (((string)priorityCBItem.Content).ToLower() == existingTask.Priority.ToString().ToLower())
                    {
                        PriorityCB.SelectedItem = priorityCBItem;
                        break;
                    }
                }
                
                var categoryCBItems = CategoryCB.Items.Cast<ComboBoxItem>();
                foreach(var categoryCBItem in categoryCBItems)
                {
                    if((int) categoryCBItem.Tag == existingTask.Category?.Id)
                    {
                        CategoryCB.SelectedItem = categoryCBItem;
                        break;
                    }
                }

                var stateCBItems = StateCB.Items.Cast<ComboBoxItem>();
                foreach( var stateCBItem in stateCBItems)
                {
                    if((int)stateCBItem.Tag == existingTask.State?.Id)
                    {
                        StateCB.SelectedItem = stateCBItem;
                        break;
                    }
                }
            }
        */}

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            /*var task = existingTask != null ? existingTask : new Model.Task();
            task.Title = TitleTB.Text;
            task.Description = DescriptionTB.Text;

            if (PriorityCB.SelectedIndex != -1)
            {
                var selectedPriorityCBItem = (ComboBoxItem)PriorityCB.SelectedItem;
                var priority = (Priority)Enum.Parse(typeof(Priority), (string)selectedPriorityCBItem.Content);
                task.Priority = priority;
            }
            
            if (!string.IsNullOrEmpty(DueDateDP.Text))
            {
                var selectedDueDateStr = DueDateDP.Text;
                var selectedDueDate = DateTime.Parse(selectedDueDateStr);
                task.DueDateTime = selectedDueDate;
            }

            if(CategoryCB.SelectedIndex != -1)
            {
                var selectedCategoryCBItem = (ComboBoxItem)CategoryCB.SelectedItem;
                var categoryId = (int)selectedCategoryCBItem.Tag;
                task.Category = categoryService.Get(categoryId);
            }

            if (StateCB.SelectedIndex != -1)
            {
                var selectedStateCBItem = (ComboBoxItem)StateCB.SelectedItem;
                var stateId = (int)selectedStateCBItem.Tag;
                task.State = stateService.Get(stateId);
            }*/

            taskService.Save((Model.Task) DataContext);

            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Siguran?", "Grupa F App", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Close();
            }
        }
    }
}
