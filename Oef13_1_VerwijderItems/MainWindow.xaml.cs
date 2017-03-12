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

namespace Oef13_1_VerwijderItems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            singersListBox.SelectionChanged += singersListBox_SelectionChanged;
        }

        void singersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object obj = singersListBox.SelectedItem;
            if (obj != null)
                singersListBox.Items.Remove(obj);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object obj = seriesListBox.SelectedItem;
            if (obj != null)
                seriesListBox.Items.Remove(obj);
        }
    }
}
