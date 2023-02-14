using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entitis
{
    public class Campaigns_Of_Asso : BaseEntity
    {
        Logger Log;
        public Campaigns_Of_Asso(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Hashtable hash = new Hashtable();
        public CampaignOfAsso Campaign = new CampaignOfAsso();
         
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
                CampaignOfAsso newCampaign = new CampaignOfAsso();
                newCampaign.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                newCampaign.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                newCampaign.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                newCampaign.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                newCampaign.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                newCampaign.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));


                newCampaign.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                newCampaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                newCampaign.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                newCampaign.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newCampaign.IDcampaign))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newCampaign.IDcampaign, newCampaign);
                }
            }
            int a = 0;
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, CampaignOfAsso Class)
        {
            Campaign = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            // @IDcampaign
            // @NameCampaign
            // @IDAssn,@NameAssn,@EmailAssn
            // @Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign
            command.Parameters.AddWithValue("@IDcampaign", Campaign.IDcampaign);
            command.Parameters.AddWithValue("@NameCampaign", Campaign.NameCampaign);
            command.Parameters.AddWithValue("@IDAssn", Campaign.IDassn);
            command.Parameters.AddWithValue("@NameAssn", Campaign.NameAssn);

            command.Parameters.AddWithValue("@EmailAssn", Campaign.EmailAssn);
            command.Parameters.AddWithValue("@Fundraising", Campaign.Fundraising);

            command.Parameters.AddWithValue("@linkURL", Campaign.linkURL);
            command.Parameters.AddWithValue("@Hashtag", Campaign.Hashtag); 
            command.Parameters.AddWithValue("@SelectedCampaign", Campaign.SelectedCampaign);
            command.Parameters.AddWithValue("@StatusCampaign", Campaign.StatusCampaign);
            command.ExecuteNonQuery();
        }
    }
}
