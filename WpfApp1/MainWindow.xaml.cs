using System;
using SQLite;
using SQLitePCL;
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
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Ville> items = new List<Ville>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void testClic(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            var test = me.GetPosition(testCanvas);
            Ville newVille = new Ville((items.Count + 1), "Ville " + (items.Count + 1), test.X, test.Y);
            items.Add(newVille);

            listVilles.Items.Add(newVille);
        }
/*
        private void saveVilles(object sender, RoutedEventArgs e)
        {
            Ville ville = new Ville()
            {
                Nom = NomTextBox.Text,
                X = XTextBox.Text,
                Y = YTextBox.Text

            };

            Database.GetDatabase().saveVille(ville);

            

            Close();

        }
        */
    }
}
