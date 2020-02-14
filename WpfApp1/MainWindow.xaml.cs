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
        public MainWindow()
        {
            InitializeComponent();
        }

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
    }
}
