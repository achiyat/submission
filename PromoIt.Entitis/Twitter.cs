using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
namespace PromoIt.Entitis
{ 
    public class Twitter : BaseEntity
    {
        Logger Log;
        public Twitter(Logger log) : base(log)
        {
            Log = LogManager;
        }

        bool stop = false;

        CampaignActivist campaignActivist = new CampaignActivist();
        UserTwitter twitter = new UserTwitter();
        List<CampaignActivist> CampaignList = new List<CampaignActivist>();
        List<UserTwitter> ListUserTwitter = new List<UserTwitter>();
        CampaignActivist user = new CampaignActivist();
        Dictionary<string, Activist> UserList = new Dictionary<string, Activist>();

        string Query;
        string ConsumerKey = "TgYbOg6MYXxYEamj7giAfh37b";
        string ConsumerSecret = "l2OK0LPIxb9Op6nZBeKIgL56eJyvxFEMRxLj5NhOzykRG8IgEH";
        string AccessToken = "1617200909900980224-P5IEw1WJ0kIF9JLHpNX5jVNBnymTRM";
        string AccessTokenSecret = "ZwQ8BEleGPBUgG5BppxWo2bY5hgSvS6MIAL43fLFhxRVq";
        string BearerToken = "AAAAAAAAAAAAAAAAAAAAAD%2BBlQEAAAAA16IVxPA9QxGaKsPv0u5%2FQXFLdtA%3DYgXZTnH5a0D6ETFMQFpggdQHkLuxzZTH110At1CtrOa6CYPYTt";


        public void TaskRun()
        {
            Task.Run(() =>
            {
                MainManager.Instance.logger.Event("Twitter/TaskRun : Enters the Twitter task and performs an infinite loop");
                while (!stop)
                {
                    try { TaskTwitter(); }
                    catch (Exception ex)
                    { MainManager.Instance.logger.Exception($"Twitter/TaskRun : {ex.Message}", ex); }

                    System.Threading.Thread.Sleep(1000 * 60 * 60);
                }

            });
        }

        private void TaskTwitter()
        {
            try 
            {
                MainManager.Instance.logger.Event("Twitter/TaskTwitter : Checks new tweets for each user");

                UserList = (Dictionary<string, Activist>)MainManager.Instance.Activists.ImportData("select * from Activists");

                TweetSharp.TwitterService twService = new TweetSharp.TwitterService(ConsumerKey, ConsumerSecret);
                twService.AuthenticateWith(AccessToken, AccessTokenSecret);

                // foreach User
                foreach (Activist activist in UserList.Values)
                {
                    int count = 0;

                    // כל הקמפיינים של הפעיל
                    CampaignList = (List<CampaignActivist>)MainManager.Instance.CampaignActivists.ImportData("select * from campaignActivist where IDactivist=" + activist.IDactivist);

                    ListUserTwitter = (List<UserTwitter>)MainManager.Instance.users.ImportData($"select * from Tweeter where UserName='{activist.NameUserTweeter}'");

                    // foreach Campaign
                    for (int Campaign = 0; Campaign < CampaignList.Count; Campaign++)
                    {
                        // Retrieve all tweets for the user
                        var twSearch = twService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = activist.NameUserTweeter });

                        List<TwitterStatus> tweetList = new List<TwitterStatus>(twSearch);

                        // Filter the tweets that contain the hashtag
                        var filteredList = tweetList.Where(t => t.Text.Contains(CampaignList[Campaign].Hashtag) && t.RetweetedStatus == null).ToList();

                        // foreach Tweet
                        for (int Tweet = 0; Tweet < filteredList.Count; Tweet++)
                        {
                            for (int Url = 0; Url < filteredList[Tweet].Entities.Urls.Count; Url++)
                            {
                                var Id = ListUserTwitter.Find(t => t.IdTweet == filteredList[Tweet].IdStr);

                                if (filteredList[Tweet].Entities.Urls[Url].ExpandedValue.Contains(CampaignList[Campaign].linkURL))
                                {
                                    count = count + 1 + filteredList[Tweet].RetweetCount;
                                    if (ListUserTwitter == null || Id == null)
                                    {
                                        twitter.IdTweet = filteredList[Tweet].IdStr;
                                        twitter.UserName = filteredList[Tweet].User.ScreenName;
                                        //twitter.DateTweet = filteredList[Tweet].CreatedDate.Day;
                                        twitter.TextTweet = filteredList[Tweet].Text;
                                        twitter.Hashtag = CampaignList[Campaign].Hashtag;
                                        twitter.URL = CampaignList[Campaign].linkURL;
                                        twitter.CountTweet = 0;

                                        Query = $"insert into Tweeter values(@IdTweet,@UserName,@TextTweet,@Hashtag,@URL,@CountTweet)";
                                        MainManager.Instance.users.ExportFromDB(Query, twitter);
                                    }
                                }
                            }
                        }


                        if (CampaignList != null && CampaignList[Campaign].MoneyActivist < count && filteredList.Count != 0)
                        {
                            if (count - CampaignList[Campaign].MoneySpent <= 0)
                            {
                                CampaignList[Campaign].MoneyActivist = 0;
                            }
                            else
                            {
                                CampaignList[Campaign].MoneyActivist = count - CampaignList[Campaign].MoneySpent;
                            }

                            campaignActivist = CampaignList[Campaign];
                            Query = $"update Shipments set MoneyActivist = @MoneyActivist where IDactivist = @IDactivist and IDcampaign = @IDcampaign update campaignActivist set MoneyActivist = @MoneyActivist where IDactivist = @IDactivist and IDcampaign = @IDcampaign update Tweeter set CountTweet = {count} where UserName = @NameUserTweeter and Hashtag = @Hashtag";
                            MainManager.Instance.CampaignActivists.ExportFromDB(Query, campaignActivist);
                        }
                    }
                }

            }
            catch (Exception ex)
            { MainManager.Instance.logger.Exception($"Twitter/TaskTwitter : {ex.Message}", ex); }

        }

    }
}
