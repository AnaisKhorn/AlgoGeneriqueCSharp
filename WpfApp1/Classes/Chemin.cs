using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Chemin
    {
       
        public List<Ville> Villes { get; set; }

        public Chemin(List<Ville> villes)
        {
            Villes = villes;
        }

        public Chemin(List<Ville> villes, StringBuilder path)
        {
            Villes = villes;
            Path = path;
        }
        public Chemin()
        {
        }

        /*   public List<Chemin> setFirstGen(int nb, Chemin cm)
           {
               int limit = 0;
               do
               {
                   bool alreadyExist = false;
                   Chemin pivot = new Chemin();
                   pivot.Villes = new List<Ville>(cm.Villes); // appel au constructeur par copie de la classe List

                   pivot.Path.Clear();
                   //int index = rnd.Next(pivot.Villes.Count);
                   for (int i = 0; i < cm.Villes.Count - 1; i++)
                   {
                       int destination = rnd.Next(pivot.Villes.Count);
                       pivot.Path.Append(pivot.Villes[destination].ID);
                       pivot.Villes.RemoveAt(destination);
                   }
                   pivot.Path.Append(pivot.Villes[0].ID);
                   Console.WriteLine("Chemin parcouru : " + pivot.Path);

                   for (int i = 0; i< Chemins.Count; i++)
                   {
                       if (Chemins[i].Path.Equals(pivot.Path))
                       {
                           alreadyExist = true;
                       }
                   }
                   if (alreadyExist == false)
                   {
                   Chemins.Add(pivot);
                       limit++;
                   }
                   else
                   {
                       Console.WriteLine("Le chemin existe déjà ! ");
                   }



               } while (limit < nb);


               return Chemins;
           }*/

        public static List<Chemin> setFirstGen(int nb, List<Ville> vs)
        {
            int limit = 0;
            Random rnd = new Random();
            List<Chemin> Chemins = new List<Chemin>();

            do
            {
                bool alreadyExist = false;
                Chemin pivot = new Chemin();
                pivot.Villes = new List<Ville>(vs); // appel au constructeur par copie de la classe List

                pivot.Path.Clear();
                //int index = rnd.Next(pivot.Villes.Count);
                for (int i = 0; i < vs.Count; i++)
                {
                    int destination = rnd.Next(pivot.Villes.Count);
                    pivot.Path.Append(pivot.Villes[destination].ID);
                    pivot.Villes.RemoveAt(destination);
                }
                //pivot.Path.Append(pivot.Villes[0].ID);
                Console.Write("Chemin parcouru : " + pivot.Path + " ");

                for (int i = 0; i < Chemins.Count; i++)
                {
                    if (Chemins[i].Path.Equals(pivot.Path))
                    {
                        alreadyExist = true;
                    }
                }
                if (alreadyExist == false)
                {
                    for (int i = 0; i < vs.Count; i++)
                    {
                        IEnumerable<Ville> v = from vill in vs
                                               where vill.ID == pivot.Path[i].ToString()
                                               select vill;
                        foreach (Ville vl in v)
                        {
                            pivot.Villes.Add(vl);
                        }
                    }
                    Chemins.Add(pivot);
                    Calcul(pivot);
                    Console.WriteLine(" Score du chemin : " + pivot.Score);
                    limit++;
                }
                else
                {
                    Console.WriteLine("Le chemin existe déjà ! ");
                }



            } while (limit < nb);


            return Chemins;
        }



        public static List<Chemin> crossover(int nb, List<Chemin> chemins, List<Ville> villesDepart)
        {
            Random rnd = new Random();
            List<Chemin> CheminsCrossOver = new List<Chemin>();
            for (int i = 0; i < 3 * nb; i++)
            {

                int r = rnd.Next(chemins.Count);
                //Chemin ch1 = chemins[r];
                Chemin ch1 = new Chemin(chemins[r].Villes, chemins[r].Path);
                int r2;

                do
                {
                    r2 = rnd.Next(chemins.Count);
                } while (r2 == r);

                Chemin ch2 = new Chemin(chemins[r2].Villes, chemins[r2].Path);
                int pivot = rnd.Next(ch1.Villes.Count);
                //découpage
                Chemin ch3 = new Chemin();
                ch3.Villes = ch1.Villes.Take(pivot).ToList();
                ch3.Villes.AddRange(ch2.Villes.Skip(pivot).ToList());
                var test1 = ch1.Villes.Take(pivot).ToList();
                var test2 = ch2.Villes.Skip(pivot).ToList();

                //on prépare une liste qui servira à identifier les doublons
                List<String> listId = new List<String>();

                // on crée une liste qui contient tous les ID des villes fournies
                List<String> listVillesID = new List<String>();

                for (int l = 0; l < villesDepart.Count; l++)
                {
                    listVillesID.Add(ch1.Villes[l].ID);
                }

                // on place dans les liste les bons éléments et on met du 
                // vide là où il y a des doublons
                for (int l = 0; l < villesDepart.Count; l++)
                {
                    if (!listId.Contains(ch3.Villes[l].ID))
                    {
                        listId.Add(ch3.Villes[l].ID);
                    }
                    else
                    {
                        listId.Add("");
                    }
                }

                // ici nous remplaçons les valeurs manquantes par les ID non utilisés
                // puis nous les mettons dans le chemin que nous ajouterons à la liste
                for (int l = 0; l < villesDepart.Count; l++)
                {
                    if (listId[l] == "")
                    {
                        List<String> listId2 = listVillesID.Except(listId).ToList();
                        listId[l] = listId2[rnd.Next(listId2.Count)];
                    }
                    ch3.Path.Append(listId[l]);

                }

                // nous mettons les villes bien dans l'ordre une fois le nouveau path crée
                // (servira pour calculer le score)
                ch3.Villes.Clear();
                for (int k = 0; k < villesDepart.Count; k++)
                {
                    IEnumerable<Ville> v = from vill in villesDepart
                                           where vill.ID == ch3.Path[k].ToString()
                                           select vill;
                    foreach (Ville vl in v)
                    {
                        ch3.Villes.Add(vl);
                    }
                }



                //listId = listId.ToList<Ville>().Union(Villes).ToList();
                /*ch3.Villes = ch3.Villes.Distinct().ToList();
                ch3.Villes = ch3.Villes.Union(Villes).ToList();*/
                /*for (int j = 0; j < ch3.Villes.Count; j++)
                {
                    ch3.Path.Append(ch3.Villes[j].ID);
                }*/
                //ajout du nouveau chemin dans la liste
                CheminsCrossOver.Add(ch3);
            }
            Chemin.Calcul(CheminsCrossOver);
            return CheminsCrossOver;
        }
        public static List<Chemin> mutation(int nb, List<Chemin> chemins)
        {
            Random rnd = new Random();
            List<Chemin> CheminsMutation = new List<Chemin>();
            for (int i = 0; i < 3 * nb; i++)
            {
                int r = rnd.Next(chemins.Count);
                Chemin ch1 = chemins[r];
                int rand1 = rnd.Next(ch1.Villes.Count);
                int rand2;
                do
                {
                    rand2 = rnd.Next(ch1.Villes.Count);
                } while (rand2 == rand1);
                String permu1 = ch1.Path[rand1].ToString();
                String permu2 = ch1.Path[rand2].ToString();
                //permutation
                String t = "&";/*
                permu1 = permu2;
                permu2 = t;*/
                Console.WriteLine("Ancien chemin : " + ch1.Path);
                ch1.Path.Replace(permu2, t);
                ch1.Path.Replace(permu1, permu2);
                ch1.Path.Replace(t, permu1);
                Console.WriteLine("Nouveau chemin : " + ch1.Path);
                CheminsMutation.Add(ch1);
            }
            Calcul(CheminsMutation);
            return CheminsMutation;
        }

        //cm : la génération ; number : le nombre d'élite que l'on souhaite garder
        public static List<Chemin> Elite(List<Chemin> cm, int number)
        {
            List<Chemin> best = new List<Chemin>();
            double[] scores = new double[cm.Count];

            for (int i = 0; i < cm.Count; i++)
            {
                scores[i] = cm[i].Score;
            }
            Array.Sort(scores);

            for (int j = 0; j < number; j++)
            {

                IEnumerable<Chemin> v = from C in cm
                                        where C.Score == scores[0]
                                        select C;

                foreach (Chemin cc in v)
                {
                    best.Add(cc);
                    break;
                }
                // pour enlever le 1er element de la liste
                scores = scores.Where((item, index) => index != 0).ToArray();

                // return best;
            }
            /*
            IEnumerable<Chemin> v = from C in cm
                                   where C.Score == scores[0] || C.Score == scores[1]
                                   select C;

            foreach (Chemin cc in v)
            {
                best.Add(cc);

            }
   */

            // recrée le score pour véridier que les données soient les bonnes.
            scores = new double[cm.Count];

            for (int i = 0; i < cm.Count; i++)
            {
                scores[i] = cm[i].Score;
            }
            Array.Sort(scores);
            return best;
        }

        public static List<Chemin> setNewGen(List<Chemin> xover, List<Chemin> muty, List<Chemin> elite, int number)
        {
            List<Chemin> lists = new List<Chemin>();

            lists.AddRange(xover);
            lists.AddRange(muty);
            lists.AddRange(elite);

            List<Chemin> result = Elite(lists, number);

            return result;
        }


        public double Score { get; set; } = 0;
        public StringBuilder Path = new StringBuilder();
        public static double Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public static void Calcul(Chemin cm)
        {
            for (int i = 0; i < cm.Villes.Count - 1; i++)
            {
                //cm.Path.Append(cm.Villes[i].ID);
                cm.Score += Distance(cm.Villes[i].X, cm.Villes[i].Y, cm.Villes[i + 1].X, cm.Villes[i + 1].Y);
            }
        }


        public static void Calcul(List<Chemin> lcm)
        {
            foreach (Chemin cm in lcm)
            {
                for (int i = 0; i < cm.Villes.Count - 1; i++)
                {
                    //cm.Path.Append(cm.Villes[i].ID);
                    cm.Score += Distance(cm.Villes[i].X, cm.Villes[i].Y, cm.Villes[i + 1].X, cm.Villes[i + 1].Y);
                }
            }
        }
    }
}
