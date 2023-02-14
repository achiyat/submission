using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis
{
    public class Associations : BaseEntity
    {
        Logger Log;
        public Associations(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Hashtable hash = new Hashtable();
        public Association Asso = new Association();

         
        // ייבוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Import
        public object ImportData(string SqlQuery)
        {
            DAL.PromoItQuery.ImportDataFromDB(SqlQuery, ReadFromDb);
            return hash;
        }

        // ייבוא נתונים - 2
        // Imports the data from the database into the server
        public void ReadFromDb(SqlDataReader reader)
        {
            //Clear Hashtable Before Inserting Information From Sql Server
            hash.Clear();
            while (reader.Read())
            {
                Association newAssociation = new Association();
                newAssociation.IDassn= reader.GetInt32(reader.GetOrdinal("IDassn"));
                newAssociation.NameAssn= reader.GetString(reader.GetOrdinal("NameAssn"));
                newAssociation.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newAssociation.EmailAssn))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newAssociation.EmailAssn, newAssociation);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Association Class)
        {
            Asso = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            // @IDassn,@NameAssn,@EmailAssn
            command.Parameters.AddWithValue("@IDassn", Asso.IDassn);
            command.Parameters.AddWithValue("@NameAssn", Asso.NameAssn);
            command.Parameters.AddWithValue("@EmailAssn", Asso.EmailAssn);
            command.ExecuteNonQuery();
        }

    }
}
