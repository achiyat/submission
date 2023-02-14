using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace PromoIt.Entitis
{ 
    public class UsersOfTwitter : BaseEntity
    {
        Logger Log;
        public UsersOfTwitter(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public UserTwitter userTwitter = new UserTwitter();
        public List<UserTwitter> ListUserTwitter = new List<UserTwitter>();

        // ייבוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Import
        public object ImportData(string SqlQuery)
        {
            DAL.PromoItQuery.ImportDataFromDB(SqlQuery, ReadFromDb);
            return ListUserTwitter;
        }

        // ייבוא נתונים - 2
        // Imports the data from the database into the server
        public void ReadFromDb(SqlDataReader reader)
        {
            try
            {
                //Clear List Before Inserting Information From Sql Server
                ListUserTwitter.Clear();
                while (reader.Read())
                {
                    UserTwitter userTwitter = new UserTwitter();
                    userTwitter.IdTweet = reader.GetString(reader.GetOrdinal("IdTweet"));
                    userTwitter.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                    userTwitter.TextTweet = reader.GetString(reader.GetOrdinal("TextTweet"));
                    userTwitter.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                    userTwitter.URL = reader.GetString(reader.GetOrdinal("URL"));
                    userTwitter.CountTweet = reader.GetInt32(reader.GetOrdinal("CountTweet"));

                    //Cheking If Hashtable contains the key
                    if (ListUserTwitter.Contains(userTwitter))
                    {
                        //key already exists
                    }
                    else
                    {
                        //Filling a hashtable
                        ListUserTwitter.Add(userTwitter);
                    }
                }
            }
            catch (Exception ex)
            { MainManager.Instance.logger.Exception($"UsersOfTwitter/ReadFromDb : {ex.Message}", ex); }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, UserTwitter Class)
        {
            userTwitter = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            // @IdTweet,@UserName,@TextTweet,@Hashtag,@URL,@CountTweet
            try
            {
                command.Parameters.AddWithValue("@IdTweet", userTwitter.IdTweet);
                command.Parameters.AddWithValue("@UserName", userTwitter.UserName);
                command.Parameters.AddWithValue("@TextTweet", userTwitter.TextTweet);
                command.Parameters.AddWithValue("@Hashtag", userTwitter.Hashtag);
                command.Parameters.AddWithValue("@URL", userTwitter.URL);
                command.Parameters.AddWithValue("@CountTweet", userTwitter.CountTweet);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            { MainManager.Instance.logger.Exception($"UsersOfTwitter/changeTheDB : {ex.Message}", ex); }

        }
    }
}
