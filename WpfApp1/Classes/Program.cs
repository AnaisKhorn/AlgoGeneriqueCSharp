using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Program
    {
        private static void Main(string[] args)
        {
            /*Ville v = new Ville("Toulon", 14, 220);
            Ville v2 = new Ville("Nice", 20, 50);
            Ville v3 = new Ville("Sophia", 100, 10);*/

            int number = 10;


            Ville v = new Ville(1, "Toulon", 14, 220);
            Ville v2 = new Ville(2, "Nice", 20, 50);
            Ville v3 = new Ville(3, "Sophia", 100, 10);
            Ville v4 = new Ville(4, "Monaco", 62, 78);
            Ville v5 = new Ville(5, "Bordeaux", 50, 200);
            Ville v6 = new Ville(6, "Paris", 180, 398);
            List<Ville> allVilles = new List<Ville>();
            allVilles.Add(v);
            allVilles.Add(v2);
            allVilles.Add(v3);
            allVilles.Add(v4);
            allVilles.Add(v5);
            allVilles.Add(v6);

            Chemin cm = new Chemin();

            cm.Villes = new List<Ville>();

            cm.Villes.AddRange(allVilles);
            /*cm.Villes.Add(v);
            cm.Villes.Add(v2);
            cm.Villes.Add(v3);
            cm.Villes.Add(v4);
            cm.Villes.Add(v5);
            cm.Villes.Add(v6);*/

            // cm.Calcul(cm.Villes);

            Console.WriteLine("Test d'affichage : " + v.Nom + ", affichage de l'abscisse " + cm.Villes[0].X);

            List<Chemin> Chemins = Chemin.setFirstGen(number, cm.Villes);


            List<Chemin> elites = Chemin.Elite(Chemins, number / 5);
            List<Chemin> testo = Chemin.crossover(number, Chemins, allVilles);
            List<Chemin> muty = Chemin.mutation(number, Chemins);
            List<Chemin> newGen = Chemin.setNewGen(testo, muty, elites, number);

            for (int j = 0; j < testo.Count; j++)
            {
                Console.WriteLine("Chemin affiché : " + testo[j].Path);
            }

            //Console.WriteLine("Chemin parcouru : " + cm.Path);
            //Console.WriteLine("Test de distances : " + cm.Score);
            //Console.Write("Test d'affichage : " + v.nom + " " + cm.Villes);
            //Console.WriteLine("Test d'affichage : ");
        }

    }
}
