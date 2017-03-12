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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oef13_6_Zoeken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            seriesListBox.Items.Add("Maja de bij");
            seriesListBox.Items.Add("wicky de Viking");
            seriesListBox.Items.Add("Prinsessia");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Contains werkt hier enkel indien je rechtstreeks met strings werkt
            // en niet met ListBoxItem -> dan zou je zelf de lus moeten schrijven
            
           if (seriesListBox.Items.Contains(findTextBox.Text))
            {
                string msg = String.Format("Item gevonden op positie {0}",
                                seriesListBox.Items.IndexOf(findTextBox.Text));
                MessageBox.Show(msg, "Gevonden");
            }
            else
            {
                MessageBox.Show("Item niet gevonden", "Niet gevonden");
            }
        }
    }
}
