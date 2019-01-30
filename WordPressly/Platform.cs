using System;
using RestSharp;

namespace WordPressly
{
    public class Platform
    {
        protected static string WordPressUrl = "https://wordpress.org/news/";
        protected static internal RestClient RestClient { get; set; }
        protected static internal PlatformInitialization Config;

        public static void Init(PlatformInitialization args)
        {
            Config = args;

            // RestClient init
            RestClient = new RestClient(WordPressUrl)
            {
                CookieContainer = Utility.CookiesManager.ReadCookiesFromDisk()
            };

            // Wordpress init
            WordPressUrl = args.WordPressUrl;
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
        public string WordPressUrl { get; set; }
    }
}
