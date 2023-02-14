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
    public class Messages : BaseEntity
    {
        Logger Log;
        public Messages(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Hashtable hash = new Hashtable();
        public Message message = new Message();
        
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
                Message newMessage = new Message();
                newMessage.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                newMessage.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                newMessage.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                newMessage.Email = reader.GetString(reader.GetOrdinal("Email"));
                newMessage.MessageUser = reader.GetString(reader.GetOrdinal("MessageUser"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newMessage.UserId))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newMessage.UserId, newMessage);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Message Class)
        {
            message = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            command.Parameters.AddWithValue("@ID", message.UserId);
            command.Parameters.AddWithValue("@Name", message.FullName);
            command.Parameters.AddWithValue("@Phone", message.Phone);
            command.Parameters.AddWithValue("@Email", message.Email);
            command.Parameters.AddWithValue("@Message", message.MessageUser);
            command.ExecuteNonQuery();
        }
    }
}







