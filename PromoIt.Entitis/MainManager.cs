using PromoIt.Entitis;
using PromoIt.Entitis.Commnd;
using PromoIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PromoIt.Entitis.Logger;

namespace PromoIt.Entities
{
    public class MainManager
    {
        // singelton
        private MainManager()
        {
            init();
        }

        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance { get { return instance; } }

        public Logger logger;
        public Twitter twitter;
        public UsersOfTwitter users;
        public Companies Companies;
        public Associations Associations;
        public Activists Activists;
        public Messages Messages;
        public Campaigns_Of_Asso CampaignsAsso;
        public Campaign_Of_Company CampaignCompany;
        public Campaign_Of_Activists CampaignActivists;
        public DonatedProducts DonatedProducts;
        public Inner_Joins InnerJoins;
        public Shipments Shipments;

        public CommandManager commandManager;

        public void init()
        {
            logger = new Logger(providerType.logFile);
            twitter = new Twitter(logger);
            twitter.TaskRun();

            users = new UsersOfTwitter(logger);
            Companies = new Companies(logger);
            Associations = new Associations(logger);
            Activists = new Activists(logger);
            Messages = new Messages(logger);
            CampaignsAsso = new Campaigns_Of_Asso(logger);
            CampaignCompany = new Campaign_Of_Company(logger);
            CampaignActivists = new Campaign_Of_Activists(logger);
            DonatedProducts = new DonatedProducts(logger);
            InnerJoins = new Inner_Joins(logger);
            Shipments = new Shipments(logger);

            commandManager = new CommandManager(logger);


        }
    }
}



//public Companies Companies = new Companies();
//public Associations Associations = new Associations();
//public Activists Activists = new Activists();
//public Messages Messages = new Messages();
//public Campaigns_Of_Asso CampaignsAsso = new Campaigns_Of_Asso();
//public Campaign_Of_Company CampaignCompany = new Campaign_Of_Company/();
//public Campaign_Of_Activists CampaignActivists = new Campaign_Of_Activists();
//public DonatedProducts DonatedProducts = new DonatedProducts();
//public Inner_Joins InnerJoins = new Inner_Joins();
//public Shipments Shipments = new Shipments();
//public Logger logger;
//public Products Products = new Products();