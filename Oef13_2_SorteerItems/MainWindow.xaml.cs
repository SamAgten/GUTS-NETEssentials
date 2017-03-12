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

namespace Oef13_2_SorteerItems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Sort();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object obj = seriesListBox.SelectedItem;
            if (obj != null)
                seriesListBox.Items.Remove(obj);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            ListBoxItem item = new ListBoxItem();
            item.Content = itemTextBox.Text;
            seriesListBox.Items.Add(item);
            Sort();
        }

        private void Sort()
        {
            //bron: http://www.c-sharpcorner.com/resources/855/sorting-a-wpf-listbox-items.aspx
            seriesListBox.Items.SortDescriptions.Clear();
            seriesListBox.Items.SortDescriptions.Add(
                        new System.ComponentModel.SortDescription("Content",
                        System.ComponentModel.ListSortDirection.Ascending));
        }
    }
}
