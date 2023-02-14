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
    public class Campaign_Of_Activists : BaseEntity
    {
        Logger Log;
        public Campaign_Of_Activists(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public List<CampaignActivist> ListCampaign = new List<CampaignActivist>();
        public CampaignActivist campaign = new CampaignActivist();

        // ייבוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Import
        public object ImportData(string SqlQuery)
        {
            DAL.PromoItQuery.ImportDataFromDB(SqlQuery, ReadFromDb);
            return ListCampaign;
        }

        // ייבוא נתונים - 2
        // Imports the data from the database into the server
        public void ReadFromDb(SqlDataReader reader)
        {
            //Clear List Before Inserting Information From Sql Server
            ListCampaign.Clear();
            while (reader.Read())
            {
                CampaignActivist newCampaign = new CampaignActivist();
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

                newCampaign.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                newCampaign.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                newCampaign.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                newCampaign.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                newCampaign.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));
                newCampaign.NameUserTweeter = reader.GetString(reader.GetOrdinal("NameUserTweeter"));
                newCampaign.MoneyActivist = reader.GetInt32(reader.GetOrdinal("MoneyActivist"));
                newCampaign.MoneySpent = reader.GetInt32(reader.GetOrdinal("MoneySpent"));

                //Cheking If Hashtable contains the key
                if (ListCampaign.Contains(newCampaign))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    ListCampaign.Add(newCampaign);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, CampaignActivist Class)
        {
            campaign = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            //"@ID,@Name,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Money,@Link,@Hashtag,@Selected,@Status,@IDactiv,@Nameactiv,@EmailActiv,@Address,@Phone"
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

            command.Parameters.AddWithValue("@IDactivist", campaign.IDactivist);
            command.Parameters.AddWithValue("@NameActivist", campaign.NameActivist);
            command.Parameters.AddWithValue("@EmailActivist", campaign.EmailActivist);
            command.Parameters.AddWithValue("@AddressActivist", campaign.AddressActivist);
            command.Parameters.AddWithValue("@phoneActivist", campaign.phoneActivist);
            command.Parameters.AddWithValue("@NameUserTweeter", campaign.NameUserTweeter);
            command.Parameters.AddWithValue("@MoneyActivist", campaign.MoneyActivist);
            command.Parameters.AddWithValue("@MoneySpent", campaign.MoneySpent);
            command.ExecuteNonQuery();
        }
    }
}
