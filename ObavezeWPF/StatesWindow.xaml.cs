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
    /// Interaction logic for StatesWindow.xaml
    /// </summary>
    public partial class StatesWindow : Window
    {
        Repository.StateRepository stateRepository;
        Service.StateService stateService;
        public StatesWindow()
        {
            InitializeComponent();
            stateRepository = new Repository.StateRepository();
            stateService = new Service.StateService(stateRepository);

            StateDG.ItemsSource = stateService.GetAll();
        }
        //add dugme
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEditStatesWindow addEditStatesWindow = new AddEditStatesWindow();
            var sacuvaj=addEditStatesWindow.ShowDialog();

            if (sacuvaj == true)
            {
                StateDG.ItemsSource = stateService.GetAll();
            }

        }
        //edit
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Model.State izabraniState = StateDG.SelectedItem as Model.State;

            if (izabraniState != null)
            {
                AddEditStatesWindow addEditStatesWindow = new AddEditStatesWindow(izabraniState);
                var rezultat= addEditStatesWindow.ShowDialog();

                if (rezultat == true)
                {
                    StateDG.ItemsSource = stateService.GetAll();
                }
            }
            else
            {
                MessageBox.Show("You haven't selected a state.");
                return;
            }
        }
        //delete dugme
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Model.State izabraniState = StateDG.SelectedItem as Model.State;

            if (izabraniState != null)
            {
                stateService.Delete(izabraniState.Id);

                StateDG.ItemsSource = stateService.GetAll();

            }
            else
            {
                MessageBox.Show("You haven't selected a state.");
                return;
            }
        }
    }
}
