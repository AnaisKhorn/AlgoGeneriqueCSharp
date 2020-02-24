using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class Database
    {
        private static Database instance = new Database();


      private static SQLiteConnection connection = null;
        private Database()
        {
            if (connection == null)
            {

            }
            SQLiteConnection co = new SQLiteConnection("db");
            co.CreateTable<Ville>();
            
            
        }
       

        public void saveVille(Ville v)
        {
            connection.Insert(v);
        }

        public List<Ville> getVille()
        {
            return connection.Table<Ville>().ToList<Ville>();
        }

        public static Database GetDatabase()
        {
            if (instance == null)
            {
                instance = new Database();
            }
            return instance;
        }


    }
}
