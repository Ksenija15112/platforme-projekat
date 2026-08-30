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
    /// Interaction logic for AddEditStatesWindow.xaml
    /// </summary>
    public partial class AddEditStatesWindow : Window
    {
        //za add
        public AddEditStatesWindow()
        {
            InitializeComponent();
            Model.State state = new Model.State();
            DataContext = state;
        }

        //za edit

        public AddEditStatesWindow(Model.State state)
        {
            InitializeComponent();
            DataContext = state;
        }

        //save dugme
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model.State state = DataContext as Model.State;

            //validacija za Name

            if (string.IsNullOrWhiteSpace(state.Name))
            {
                MessageBox.Show("Name is required.");
                return;
            }

            Repository.StateRepository stateRepository = new Repository.StateRepository();
            Service.StateService stateService = new Service.StateService(stateRepository);

            stateService.Save(state);

            DialogResult = true;
            Close();
        }
        //cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
