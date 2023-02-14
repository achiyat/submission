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
    public class Inner_Joins : BaseEntity
    {
        Logger Log;
        public Inner_Joins(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Hashtable hash = new Hashtable();

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
                InnerJoin InnerJoin = new InnerJoin();
                InnerJoin.IDProduct = reader.GetInt32(reader.GetOrdinal("IDProduct"));
                InnerJoin.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                InnerJoin.Price = reader.GetInt32(reader.GetOrdinal("Price"));
                InnerJoin.Inventory = reader.GetInt32(reader.GetOrdinal("Inventory"));
                InnerJoin.SelectedProduct = reader.GetBoolean(reader.GetOrdinal("SelectedProduct"));
                InnerJoin.StatusProduct = reader.GetBoolean(reader.GetOrdinal("StatusProduct"));

                InnerJoin.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                InnerJoin.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                InnerJoin.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                InnerJoin.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                InnerJoin.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                InnerJoin.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));


                InnerJoin.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                InnerJoin.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                InnerJoin.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                InnerJoin.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));


                InnerJoin.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                InnerJoin.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                InnerJoin.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                InnerJoin.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));
                InnerJoin.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));



                InnerJoin.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                InnerJoin.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                InnerJoin.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                InnerJoin.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                InnerJoin.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));
                InnerJoin.NameUserTweeter = reader.GetString(reader.GetOrdinal("NameUserTweeter"));
                InnerJoin.MoneyActivist = reader.GetInt32(reader.GetOrdinal("MoneyActivist"));
                InnerJoin.MoneySpent = reader.GetInt32(reader.GetOrdinal("MoneySpent"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(InnerJoin.IDProduct))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(InnerJoin.IDProduct, InnerJoin);
                }
            }
        }
    }
}
