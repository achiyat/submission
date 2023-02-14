using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Model
{
    public class UserTwitter
    {
        public string IdTweet { get; set; }
        public string UserName { get; set; }
        //public DateTime DateTweet { get; set; }
        public string TextTweet { get; set; }
        public string Hashtag { get; set; }
        public string URL { get; set; }
        public int CountTweet { get; set; }
    }
}
