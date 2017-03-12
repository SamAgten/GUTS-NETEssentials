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

namespace Oef13_7_Maandconversie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<string> months;
        
        public MainWindow()
        {
            InitializeComponent();
            months = new List<string>();
            months.Add("January");
            months.Add("February");
            months.Add("March");
            months.Add("April");
            months.Add("May");
            months.Add("June");
            months.Add("July");
            months.Add("August");
            months.Add("September");
            months.Add("October");
            months.Add("November");
            months.Add("December");  
        }

        private void lookupButton_Click(object sender, RoutedEventArgs e)
        {
            int monthNumber;
            string monthname;

            monthNumber = Convert.ToInt32(monthNumberTextBox.Text);
            monthname = months[monthNumber - 1];
            monthNameTextBox.Text = monthname;
        }
    }
}
