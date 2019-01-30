using System;
using RestSharp;

namespace WordPressly
{
    public class Platform
    {
        protected static string WordPressAPIUrl = "https://hub.backinmotion.com.au/wp-json/";
        protected static internal RestClient RestClient { get; set; }
        protected static internal PlatformInitialization Config;

        public static void Init(PlatformInitialization args)
        {
            Config = args;

            // RestClient init
            RestClient = new RestClient(WordPressAPIUrl)
            {
                CookieContainer = Utility.CookiesManager.ReadCookiesFromDisk()
            };

            // Wordpress init
            WordPressAPIUrl = args.WordPressAPIUrl;
            WordPress.Posts = new Repository.Posts();
            WordPress.Categories = new Repository.Categories();
            WordPress.Media = new Repository.Media();
        }

        public static System.Net.CookieContainer SetCookies
        {
            set
            {
                RestClient.CookieContainer = value;
            }
        }
    }

    public class PlatformInitialization
    {
        public string WordPressAPIUrl { get; set; }
    }
}
