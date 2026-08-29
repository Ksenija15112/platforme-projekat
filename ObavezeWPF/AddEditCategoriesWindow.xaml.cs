using ObavezeWPF.Model;
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
    /// Interaction logic for AddEditCategoriesWindow.xaml
    /// </summary>
    public partial class AddEditCategoriesWindow : Window
    {
        public AddEditCategoriesWindow()
        {
            InitializeComponent();
            Model.Category category = new Model.Category();
            DataContext = category;

            
        }

        public AddEditCategoriesWindow(Category category)
        {
            InitializeComponent();
            DataContext = category;
        }
        //save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model.Category category2 = DataContext as Model.Category;

            //validacija

            if (string.IsNullOrWhiteSpace(category2.Name))
            {
                MessageBox.Show("Name is required.");
                return;
            }

            Repository.CategoryRepository categoryRepository = new Repository.CategoryRepository();
            Service.CategoryService categoryService = new Service.CategoryService(categoryRepository);

            categoryService.Save(category2);

            DialogResult = true;
            Close();

        }
        //cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Changes were not saved.");
            Close();
        }
    }
}
