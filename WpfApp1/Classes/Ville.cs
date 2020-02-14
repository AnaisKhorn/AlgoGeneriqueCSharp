using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Table("Ville")]
    public class Ville
    {
        [PrimaryKey, AutoIncrement]
        public string ID { get; set; }
        [Column("Name")]
        public string Nom { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Ville() { }

        public Ville(string nom, int x, int y)
        {
            Nom = nom;
            X = x;
            Y = y;
        }

        public Ville(string id, string nom, int x, int y)
        {
            ID = id;
            Nom = nom;
            X = x;
            Y = y;
        }

    }
}
