﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace Ado
{
    class Requetes
    {
        private MySqlConnection cnx;

        public Requetes(string h, string u, string db, string pswd)
        {
            connection2 cn = new connection2(h, u, db, pswd);
            this.cnx = cn.Cnx2;  
        }

        public string Listejoueurs()
        {
            string resultat = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "joueur";
            cmdSql.CommandType = CommandType.TableDirect;
            cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                resultat+=String.Format("{0} : {1} : {2} : {3} : {4}\n", reader[0],reader[1], reader[2], reader[3], reader[4]);
            }
            cnx.Close();
            return resultat;
        }

        public string ListeJoueursSponsor()
        {
            string resultat = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "select nomJoueur,nomSponsor from joueur inner join sponsor on joueur.noSponsor = sponsor.noSponsor order by nomSponsor";
            cmdSql.CommandType = CommandType.Text;
            cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                resultat += String.Format("{0} : {1}\n", reader[0], reader[1]);
            }
            cnx.Close();
            return resultat;
        }
        public string ListeJoueursSponsorNation()
        {
            string resultat = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "select nomJoueur,nomSponsor, nation from joueur inner join sponsor on joueur.noSponsor = sponsor.noSponsor where nation=nationalite ";
            cmdSql.CommandType = CommandType.Text;
            cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                resultat += String.Format("{0} : {1} : {2}\n", reader[0], reader[1], reader[2]);
            }
            cnx.Close();
            return resultat;
        }
        public string MontantSponsor()
        {
            string resultat = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "select sum(chiffreaffaires) from sponsor ";
            cmdSql.CommandType = CommandType.Text;
            cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                resultat += String.Format("{0}\n", reader[0]);
            }
            cnx.Close();
            return resultat;
        }
        public void InsertJoueurs()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "Insert into joueur (numLicence,nomjoueur,prenom,agejoueur,nation,noSponsor) values (13,'Nadal','Rafael',30,'ESP',8)";
            cmdSql.CommandType = CommandType.Text;
            cnx.Open();
            cmdSql.ExecuteNonQuery();
            cnx.Close();
        }
        public void UpdateJoueurs()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "Update joueur set ageJoueur = 27 where nomJoueur='Nadal'";
            cmdSql.CommandType = CommandType.Text;
            cnx.Open();
            cmdSql.ExecuteNonQuery();
            cnx.Close();
        }
        public void DeleteJoueurs()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "Delete from joueur where nomJoueur = 'Nadal'";
            cmdSql.CommandType = CommandType.Text;
            cnx.Open();
            cmdSql.ExecuteNonQuery();
            cnx.Close();
        }

        public void CallCreationEmployer()
        {
            string nom = "SAPKIR";
            string prenom = "Ozan";
            string sexe = "M";
            int cadre = 1;
            decimal salaire = 1000000;
            int service = 2;
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "creationEmployer";
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.Parameters.Add(new MySqlParameter("nom", MySqlDbType.String));
            cmdSql.Parameters["nom"].Value = nom;
            cmdSql.Parameters.Add(new MySqlParameter("prenom", MySqlDbType.String));
            cmdSql.Parameters["prenom"].Value = prenom;
            cmdSql.Parameters.Add(new MySqlParameter("sexe", MySqlDbType.String));
            cmdSql.Parameters["sexe"].Value = sexe;
            cmdSql.Parameters.Add(new MySqlParameter("cadre", MySqlDbType.Int32));
            cmdSql.Parameters["cadre"].Value = cadre;
            cmdSql.Parameters.Add(new MySqlParameter("salaire", MySqlDbType.Decimal));
            cmdSql.Parameters["salaire"].Value = salaire;
            cmdSql.Parameters.Add(new MySqlParameter("service", MySqlDbType.Int32));
            cmdSql.Parameters["service"].Value = service;
            this.cnx.Open();
            cmdSql.ExecuteNonQuery();
            this.cnx.Close();
        }

        public string CallListeEmploye()
        {
            string result = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "SelectDip";
            cmdSql.CommandType = CommandType.StoredProcedure;

            this.cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                result += String.Format("{0}, {1}\n", reader[0], reader[1]);
            }
            this.cnx.Close();
            return result;
        }

        public string CallSalaireEmployer()
        {
            decimal borneInferieur = 4000;
            decimal borneSuperieur = 6000;
            string result = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "borneSalaire";
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.Parameters.Add(new MySqlParameter("borneInferieur", MySqlDbType.Decimal));
            cmdSql.Parameters["borneInferieur"].Value = borneInferieur;
            cmdSql.Parameters.Add(new MySqlParameter("borneSuperieur", MySqlDbType.Decimal));
            cmdSql.Parameters["borneSuperieur"].Value = borneSuperieur;

            this.cnx.Open(); MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                result += String.Format("{0}, {1}\n",
                    reader[0], reader[1]);
            }
            this.cnx.Close();
            return result;
        }

        public void CallMajBudget()
        {
            int value = 1;
            decimal numID = 10;
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "Budget";
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.Parameters.Add(new MySqlParameter("value", MySqlDbType.Int32));
            cmdSql.Parameters["value"].Value = value;
            cmdSql.Parameters.Add(new MySqlParameter("numID", MySqlDbType.Decimal));
            cmdSql.Parameters["numID"].Value = numID;

            this.cnx.Open();
            cmdSql.ExecuteNonQuery();
            this.cnx.Close();
        }

        public string CallAvgDiplomeSup()
        {
            string result = "";
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "Moyenne";
            cmdSql.CommandType = CommandType.StoredProcedure;

            this.cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                result += String.Format("{0}, {1}, {2}\n",
                    reader[0], reader[1], reader[2]);
            }
            this.cnx.Close();
            return result;
        }
        public string SuperCadre()
        {
            string result = "";
            cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "select emp_nom from Cadres where emp_salaire > (select AVG(emp_salaire) from Cadres)";
            cmdSql.CommandType = CommandType.Text;
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                result += String.Format("{0}\n", reader[0]);
            }

            this.cnx.Close();
            return result;
        }

    }
}
