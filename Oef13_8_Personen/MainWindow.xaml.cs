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
        private List<Persoon> persoonslijst;
        
        public MainWindow()
        {
            InitializeComponent();

            persoonslijst = new List<Persoon>();
            persoonslijst.Add(new Persoon("Hermans", "Kris", "Kerkhof 24, 3560 Houthalen", new DateTime(1975, 5, 15), "1234567", GeslachtEnum.M));
            persoonslijst.Add(new Persoon("Stasik", "Marijke", "Kerkhof 24, 3560 Houthalen", new DateTime(1975, 2, 14), "12345667", GeslachtEnum.V));
            persoonslijst.Add(new Persoon("Hermans", "Ella", "Kerkhof 24, 3560 Houthalen", new DateTime(2003, 12, 25), "1234567", GeslachtEnum.V));
            persoonslijst.Add(new Persoon("Hermans", "Gilles", "Kerkhof 24, 3560 Houthalen", new DateTime(2008, 9, 29), "1234567", GeslachtEnum.M));

            // we doen het hier iets geavanceerder via data binding
            persoonListBox.ItemsSource = persoonslijst;
        }

        
        // Dubbelklik is niet zo eenvoudig met WPF, Windows Forms was eenvoudiger
        // zie: http://stackoverflow.com/questions/2547442/wpf-listboxitem-double-click
        // Daarom hier met Click gewerkt.
        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (persoonListBox.SelectedItem != null)
            {
                Persoon p = (Persoon)persoonListBox.SelectedItem;
                Window w = new DetailsWindow(p);
                w.Show();
            }
        }
    }
}
