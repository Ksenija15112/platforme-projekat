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
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        Repository.CategoryRepository categoryRepository;
        Service.CategoryService categoryService;
        public CategoriesWindow()
        {
            InitializeComponent();
            categoryRepository = new Repository.CategoryRepository();
            categoryService = new Service.CategoryService(categoryRepository);

            CategoryDG.ItemsSource = categoryService.GetAll();
        }

       
        //add
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEditCategoriesWindow addEditCategoriesWindow = new AddEditCategoriesWindow();
            var sacuvano=addEditCategoriesWindow.ShowDialog();

            if (sacuvano == true)
            {
                CategoryDG.ItemsSource = categoryService.GetAll();
            }
        }
        //edit
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Model.Category izabranaKategorija = (CategoryDG.SelectedItem as Model.Category);

            if (izabranaKategorija != null)
            {
                AddEditCategoriesWindow addEditCategoriesWindow = new AddEditCategoriesWindow(izabranaKategorija);
                var osveziTabelu=addEditCategoriesWindow.ShowDialog();

                if (osveziTabelu == true)
                {
                    CategoryDG.ItemsSource = categoryService.GetAll();
                }
            }
            else
            {
                MessageBox.Show("Please select a category.");
            }
        }
        //delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Model.Category izabranaKategorija = (CategoryDG.SelectedItem as Model.Category);

            if (izabranaKategorija == null)
            {
                MessageBox.Show("Please select a category to delete.");
                return;
            }
            else
            {
                categoryService.Delete(izabranaKategorija.Id);
                CategoryDG.ItemsSource = categoryService.GetAll();

            }
        }
    }
}
