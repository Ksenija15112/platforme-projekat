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
    /// Interaction logic for LabelsWindow.xaml
    /// </summary>
    public partial class LabelsWindow : Window
    {

        LabelService labelService;
        public LabelsWindow()
        {
            InitializeComponent();
            Repository.LabelRepository labelRepository = new Repository.LabelRepository();
            labelService = new Service.LabelService(labelRepository);
            tabelaLabel.ItemsSource=labelService.Iscitaj();
        }

        private void dugmeAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditLabelWindow addEditLabelWindow = new AddEditLabelWindow();
            addEditLabelWindow.ShowDialog();
            tabelaLabel.ItemsSource = labelService.Iscitaj();
        }

        private void dugmeEdit_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaLabel.SelectedItem == null)
            {
                MessageBox.Show("Please select a label.");
                return;
            }
            
            Model.Label promenljiva = (Model.Label) tabelaLabel.SelectedItem;
            

            AddEditLabelWindow editLabelWindow = new AddEditLabelWindow(promenljiva);
            editLabelWindow.ShowDialog();
            tabelaLabel.ItemsSource = labelService.Iscitaj();


        }

        private void dugmeDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaLabel.SelectedItem == null)
            {
                MessageBox.Show("Please select a label.");
                return;
            }
            Model.Label promenljiva = (Model.Label)tabelaLabel.SelectedItem;

            MessageBoxResult odgovor = MessageBox.Show(
            "Are you sure you want to delete this label?",
            "Delete",
             MessageBoxButton.YesNo
            );

            if (odgovor == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                labelService.Obrisi(promenljiva.Id);
                tabelaLabel.ItemsSource = labelService.Iscitaj();
            }
        }
    }
}
