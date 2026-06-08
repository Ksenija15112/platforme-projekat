using ObavezeWPF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ObavezeWPF
{
    /// <summary>
    /// Interaction logic for AddEditLabelWindow.xaml
    /// </summary>
    public partial class AddEditLabelWindow : Window
    {
        Service.LabelService labelService;
        public AddEditLabelWindow()
        {
            InitializeComponent();
            DataContext = new Model.Label();
            Repository.LabelRepository labelRepository = new Repository.LabelRepository();
            labelService = new LabelService(labelRepository);
        }

        private void dugmeSave_Click(object sender, RoutedEventArgs e)
        {
            Model.Label label = (Model.Label)DataContext;

            if (ColorOdaberi.SelectedItem != null)
            {
                string izabrano = (ColorOdaberi.SelectedItem as ComboBoxItem).Content.ToString();
                label.Color = izabrano;
            }
            if (string.IsNullOrWhiteSpace(label.Name))
            {
                MessageBox.Show("Name is required.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(label.Description))
            {
                MessageBox.Show("Description is required.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(label.Color))
            {
                MessageBox.Show("Color is required.");
                return;
            }
           
            labelService.Dodaj(label);
            DialogResult = true;
            Close();
        }

        private void dugmeCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
