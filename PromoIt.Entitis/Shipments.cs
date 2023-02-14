using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis
{
    public class Shipments : BaseEntity
    {
        Logger Log;
        public Shipments(Logger log) : base(log)
        {
            Log = LogManager;
        }

        Shipping shipping = new Shipping();
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
                Shipping GetShipping = new Shipping();
                GetShipping.IDShipments = reader.GetInt32(reader.GetOrdinal("IDShipments"));
                GetShipping.donated = reader.GetBoolean(reader.GetOrdinal("donated"));
                GetShipping.bought = reader.GetBoolean(reader.GetOrdinal("bought"));
                
                GetShipping.IDProduct = reader.GetInt32(reader.GetOrdinal("IDProduct"));
                GetShipping.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                GetShipping.Price = reader.GetInt32(reader.GetOrdinal("Price"));
                GetShipping.Inventory = reader.GetInt32(reader.GetOrdinal("Inventory"));
                GetShipping.SelectedProduct = reader.GetBoolean(reader.GetOrdinal("SelectedProduct"));
                GetShipping.StatusProduct = reader.GetBoolean(reader.GetOrdinal("StatusProduct"));

                GetShipping.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                GetShipping.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                GetShipping.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                GetShipping.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                GetShipping.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                GetShipping.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));
                

                GetShipping.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                GetShipping.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                GetShipping.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                GetShipping.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));

                GetShipping.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                GetShipping.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                GetShipping.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                GetShipping.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));
                GetShipping.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));


                GetShipping.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                GetShipping.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                GetShipping.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                GetShipping.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                GetShipping.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));
                GetShipping.NameUserTweeter = reader.GetString(reader.GetOrdinal("NameUserTweeter"));
                GetShipping.MoneyActivist = reader.GetInt32(reader.GetOrdinal("MoneyActivist"));
                GetShipping.MoneySpent = reader.GetInt32(reader.GetOrdinal("MoneySpent"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(GetShipping.IDShipments))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(GetShipping.IDShipments, GetShipping);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Shipping Class)
        {
            shipping = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            // @IDShipments
            // @donated,@bought
            // @IDProduct
            // @ProductName,@Price,@Inventory,@SelectedProduct,@StatusProduct
            // @IDcampaign,@NameCampaign,@IDAssn,@NameAssn,@EmailAssn,@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign
            // @IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany
            // @IDactivist,@NameActivist,@EmailActivist,@AddressActivist,@phoneActivist,@NameUserTweeter
            // @MoneyActivist,@MoneySpent

            command.Parameters.AddWithValue("@IDShipments", shipping.IDShipments);
            command.Parameters.AddWithValue("@donated", shipping.donated);
            command.Parameters.AddWithValue("@bought", shipping.bought);

            command.Parameters.AddWithValue("@IDProduct", shipping.IDProduct);
            command.Parameters.AddWithValue("@ProductName", shipping.ProductName);
            command.Parameters.AddWithValue("@Price", shipping.Price);
            command.Parameters.AddWithValue("@Inventory", shipping.Inventory);
            command.Parameters.AddWithValue("@SelectedProduct", shipping.SelectedProduct);
            command.Parameters.AddWithValue("@StatusProduct", shipping.StatusProduct);

            command.Parameters.AddWithValue("@IDcampaign", shipping.IDcampaign);
            command.Parameters.AddWithValue("@NameCampaign", shipping.NameCampaign);
            command.Parameters.AddWithValue("@IDassn", shipping.IDassn);
            command.Parameters.AddWithValue("@NameAssn", shipping.NameAssn);

            command.Parameters.AddWithValue("@EmailAssn", shipping.EmailAssn);
            command.Parameters.AddWithValue("@Fundraising", shipping.Fundraising);

            command.Parameters.AddWithValue("@linkURL", shipping.linkURL);
            command.Parameters.AddWithValue("@Hashtag", shipping.Hashtag);
            command.Parameters.AddWithValue("@SelectedCampaign", shipping.SelectedCampaign);
            command.Parameters.AddWithValue("@StatusCampaign", shipping.StatusCampaign);

            command.Parameters.AddWithValue("@IDCompany", shipping.IDCompany);
            command.Parameters.AddWithValue("@NameCompany", shipping.NameCompany);
            command.Parameters.AddWithValue("@OwnerCompany", shipping.OwnerCompany);
            command.Parameters.AddWithValue("@EmailCompany", shipping.EmailCompany);
            command.Parameters.AddWithValue("@PhoneCompany", shipping.PhoneCompany);


            command.Parameters.AddWithValue("@IDactivist", shipping.IDactivist);
            command.Parameters.AddWithValue("@NameActivist", shipping.NameActivist);
            command.Parameters.AddWithValue("@EmailActivist", shipping.EmailActivist);
            command.Parameters.AddWithValue("@AddressActivist", shipping.AddressActivist);
            command.Parameters.AddWithValue("@phoneActivist", shipping.phoneActivist);
            command.Parameters.AddWithValue("@NameUserTweeter", shipping.NameUserTweeter);
            command.Parameters.AddWithValue("@MoneyActivist", shipping.MoneyActivist);
            command.Parameters.AddWithValue("@MoneySpent", shipping.MoneySpent);
            command.ExecuteNonQuery();
        }
    }
}
