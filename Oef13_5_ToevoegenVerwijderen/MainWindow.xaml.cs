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

namespace Oef13_5_ToevoegenVerwijderen
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
        private void replaceButton_Click(object sender, RoutedEventArgs e)
        {
            int index = seriesListBox.SelectedIndex;
            if (index > -1)
            {
                seriesListBox.Items.RemoveAt(index);
                seriesListBox.Items.Insert(index, replaceTextBox.Text);
            }
        }

        private void verwijderButton_Click(object sender, RoutedEventArgs e)
        {
            seriesListBox.Items.RemoveAt(Convert.ToInt32(posTextBox.Text));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = new ListBoxItem();
            item.Content = itemTextBox.Text;
            seriesListBox.Items.Insert(Convert.ToInt32(posTextBox.Text), item); //.Add(item);

        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            seriesListBox.Items.Clear();
        }
    }
}
