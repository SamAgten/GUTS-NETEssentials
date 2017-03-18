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

namespace Oef13_8_Personen
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

        
        // Dubbelklik is niet zo eenvoudig met WPF, Windows Forms was eenvoudiger
        // zie: http://stackoverflow.com/questions/2547442/wpf-listboxitem-double-click
        // Daarom hier met Click gewerkt.
        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
