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

namespace Oef13_9_ComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void colorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)colorComboBox.SelectedItem;
            string selectedColor = Convert.ToString(item.Content);
            switch (selectedColor)
            {
                case "Red":
                    colorLabel.Background = new SolidColorBrush(Colors.Red);
                    break;
                case "Green":
                    colorLabel.Background = new SolidColorBrush(Colors.Green);
                    break;
                case "Blue":
                    colorLabel.Background= new SolidColorBrush(Colors.Blue);
                    break;
            }
        }
    }
}
