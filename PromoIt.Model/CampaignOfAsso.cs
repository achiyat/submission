using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Model
{
    public class CampaignOfAsso : Association
    {
        public int IDcampaign { get; set; }
        public string NameCampaign { get; set; }

        //public int IDassn { get; set; }
        //public string NameAssn { get; set; }
        //public string EmailAssn { get; set; }
        public int Fundraising { get; set; }

        public string linkURL { get; set; }
        public string Hashtag { get; set; }
        public bool SelectedCampaign { get; set; } 
        public bool StatusCampaign { get; set; }
    }
}
