using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entitis
{
    public class Activists : BaseEntity
    {
        Logger Log;
        public Activists(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Activist Activ = new Activist();
        public Dictionary<string, Activist> activists = new Dictionary<string, Activist>();

        // ייבוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Import
        public object ImportData(string SqlQuery)
        {
            DAL.PromoItQuery.ImportDataFromDB(SqlQuery, ReadFromDb);
            return activists;
        }

        // ייבוא נתונים - 2
        // Imports the data from the database into the server
        public void ReadFromDb(SqlDataReader reader)
        {
            //Clear Hashtable Before Inserting Information From Sql Server
            activists.Clear();
            while (reader.Read())
            {
                Activist newActivist = new Activist();
                newActivist.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                newActivist.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                newActivist.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                newActivist.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                newActivist.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));
                newActivist.NameUserTweeter = reader.GetString(reader.GetOrdinal("NameUserTweeter"));

                //Cheking If Hashtable contains the key
                if (activists.ContainsKey(newActivist.EmailActivist))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    activists.Add(newActivist.EmailActivist, newActivist);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Activist Class)
        {
            Activ = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            command.Parameters.AddWithValue("@IDactivist", Activ.IDactivist);
            command.Parameters.AddWithValue("@NameActivist", Activ.NameActivist);
            command.Parameters.AddWithValue("@EmailActivist", Activ.EmailActivist);
            command.Parameters.AddWithValue("@AddressActivist", Activ.AddressActivist);
            command.Parameters.AddWithValue("@phoneActivist", Activ.phoneActivist);
            command.Parameters.AddWithValue("@NameUserTweeter", Activ.NameUserTweeter);
            command.ExecuteNonQuery();
        }
    }
}
