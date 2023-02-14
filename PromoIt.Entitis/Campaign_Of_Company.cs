using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entitis
{
    public class Campaign_Of_Company : BaseEntity
    {
        Logger Log;
        public Campaign_Of_Company(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Hashtable hash = new Hashtable();
        public CampaignOfCompany campaign = new CampaignOfCompany();
         
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
                CampaignOfCompany newCampaign = new CampaignOfCompany();
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

                newCampaign.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                newCampaign.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                newCampaign.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                newCampaign.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));
                newCampaign.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));


                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newCampaign.NameCampaign))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newCampaign.NameCampaign, newCampaign);
                }
            }
        }


        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, CampaignOfCompany Class)
        {
            campaign = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            // @IDcampaign
            // @NameCampaign,@IDAssn,@NameAssn,@EmailAssn,@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign
            // @IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany
            command.Parameters.AddWithValue("@IDcampaign", campaign.IDcampaign);
            command.Parameters.AddWithValue("@NameCampaign", campaign.NameCampaign);
            command.Parameters.AddWithValue("@IDassn", campaign.IDassn);
            command.Parameters.AddWithValue("@NameAssn", campaign.NameAssn);

            command.Parameters.AddWithValue("@EmailAssn", campaign.EmailAssn);
            command.Parameters.AddWithValue("@Fundraising", campaign.Fundraising);

            command.Parameters.AddWithValue("@linkURL", campaign.linkURL);
            command.Parameters.AddWithValue("@Hashtag", campaign.Hashtag);
            command.Parameters.AddWithValue("@SelectedCampaign", campaign.SelectedCampaign);
            command.Parameters.AddWithValue("@StatusCampaign", campaign.StatusCampaign);

            command.Parameters.AddWithValue("@IDCompany", campaign.IDCompany);
            command.Parameters.AddWithValue("@NameCompany", campaign.NameCompany);
            command.Parameters.AddWithValue("@OwnerCompany", campaign.OwnerCompany);
            command.Parameters.AddWithValue("@EmailCompany", campaign.EmailCompany);
            command.Parameters.AddWithValue("@PhoneCompany", campaign.PhoneCompany);

            command.ExecuteNonQuery();
        }
    }
}