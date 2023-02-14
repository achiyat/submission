using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis.Commnd
{
    public class CommandManager : BaseEntity
    {
        Logger Log;
        public CommandManager(Logger log) : base(log)
        {
            Log = LogManager;
        }

        private Dictionary<string, ICommand> _CommandList;

        public Dictionary<string, ICommand> CommandList
        { get 
            {
                if (_CommandList == null) Init();
                return _CommandList;
            } 
        }

        private void Init()
        {
            try
            {
                MainManager.Instance.logger.Event("CommandManager : enters the init function");

                _CommandList = new Dictionary<string, ICommand>
                {
                    {"AddActivist", new AddActivist() },
                    {"chooseCampaigns", new chooseCampaigns() },
                    {"AddShipments", new AddShipments() },
                    {"BuyProduct", new BuyProduct() },
                    {"removeFromCrat", new removeFromCrat() },
                    {"GetActivist", new GetActivist() },
                    {"GetActivistById", new GetActivistById() },

                    {"AddCompany", new AddCompany() },
                    {"Upload", new Upload() },
                    {"UpdateProduct", new UpdateProduct() },
                    {"DeleteProduct", new DeleteProduct() },
                    {"GetCompany", new GetCompany() },
                    {"GetCompanyById", new GetCompanyById() },

                    {"AddAssociation", new AddAssociation() },
                    {"Created", new Created() },
                    {"UpdateCampaign", new UpdateCampaign() },
                    {"DeleteCampaign", new DeleteCampaign() },
                    {"GetAssociation", new GetAssociation() },
                    {"GetAssociationById", new GetAssociationById() },

                    {"message", new message() },
                    {"Get-Campaigns", new GetCampaigns() },
                    {"GetCampaignsById", new GetCampaignsById() },
                    {"Get-Campaigns-Activist", new GetCampaignsActivist() },
                    {"Get-Campaigns-Company", new GetCampaignsCompany() },

                    {"SortCampaignsCompany", new SortCampaignsCompany() },
                    {"Get-Product-Activist", new GetProductActivist() },
                    {"Get-Donated-products", new GetDonatedProducts() },
                    {"Get-Shipments", new GetShipments() },
                };
            }
            catch (Exception ex)
            { MainManager.Instance.logger.Exception($"CommandManager : {ex.Message}", ex); }
        }

    }



    public interface ICommand
    {
        object ExecuteCommand(params object[] param);
    }
}

        //string VarActivist = "@IDactivist,@NameActivist,@EmailActivist,@AddressActivist,@phoneActivist,@NameUserTweeter";
        //string VarCompany = "@IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany";
        //string VarAssociation = "@IDassn,@NameAssn,@EmailAssn";
        //string VarCampaignAsso = $"@NameCampaign,{VarAssociation},@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign";
        //string VarCampaignActivist = $"@IDcampaign,{CampaignAsso},{Activist},@MoneyActivist,@MoneySpent";
        //string VarCampaignCompany = $"@IDcampaign,{CampaignAsso},{Company}";
        //string VarProduct = $"@ProductName,@Price,@Inventory,@SelectedProduct,@StatusProduct,{CampaignCompany}";
        //string VarShipping = $"@donated,@bought,@IDProduct,{Product},{Activist},@MoneyActivist,@MoneySpent";