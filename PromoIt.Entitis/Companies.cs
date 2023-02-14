using PromoIt.Entitis;
using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entities
{
    public class Companies : BaseEntity
    {
        Logger Log; 
        public Companies(Logger log) : base(log)
        {
            Log = LogManager;
        }

        public Hashtable hash = new Hashtable();
        public Company company = new Company();
        
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
                Company newcompany = new Company();
                newcompany.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                newcompany.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                newcompany.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                newcompany.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));
                newcompany.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));


                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newcompany.EmailCompany))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newcompany.EmailCompany, newcompany);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Company Class)
        {
            company = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            // @IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany
            command.Parameters.AddWithValue("@IDCompany", company.IDCompany);
            command.Parameters.AddWithValue("@NameCompany", company.NameCompany);
            command.Parameters.AddWithValue("@OwnerCompany", company.OwnerCompany);
            command.Parameters.AddWithValue("@EmailCompany", company.EmailCompany);
            command.Parameters.AddWithValue("@PhoneCompany", company.PhoneCompany);

            command.ExecuteNonQuery();
        }

    }
}


//  נתונים
/*
         public void Db(SqlCommand command)
        {
            command.Parameters.AddWithValue("@ID", company.ID);
            command.Parameters.AddWithValue("@Name", company.Name);
            command.Parameters.AddWithValue("@Owner", company.Owner);
            command.Parameters.AddWithValue("@Phone", company.Phone);
            command.Parameters.AddWithValue("@Email", company.Email);
            command.ExecuteNonQuery();
        }
 */



/*
         public object ReadFromDb(SqlDataReader reader)
        {
            //Clear Hashtable Before Inserting Information From Sql Server
            hash.Clear();
            object retHash = null;
            while (reader.Read())
            {
                Company company = new Company();
                company.IDCompany = reader.GetString(reader.GetOrdinal("IDCompany"));

                company.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));

                company.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));

                company.Phone = reader.GetString(reader.GetOrdinal("Phone"));

                company.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(company.IDCompany))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(company.IDCompany, company);
                }
            }
            retHash = hash;
            return retHash;
        }
 */