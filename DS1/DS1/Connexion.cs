using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace tpAdoConnecteGesper
{
    class Connexion
    {
        static private MySqlConnection cnx;
                public Connexion(string h, string u, string db, string p)
        {
            string sCnx;
            // chaîne de caractères de connexion
                    sCnx = String.Format("server={0};uid={1};database={2};port=3306;pwd={3}", h, u, db, p);
            //création d'un objet connexion
            cnx = new MySqlConnection(sCnx);

        }
        public MySqlConnection Cnx
        {
            get { return Connexion.cnx; }

        }
        public void ouvrir()
        {
            //ouverture de la connexion
            try
            {
                cnx.Open();
                Console.WriteLine("connexion réussie");
            }
            catch (Exception e)
            {
                Console.WriteLine("erreur connexion " + e.Message.ToString());
            }

        }

        public void fermer()
        {
            cnx.Close();
        }

    }
}
