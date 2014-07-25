using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Storage.Basic; 
using Our.Umbraco.uTwit.Converters;
using Our.Umbraco.uTwit.Extensions;
using Our.Umbraco.uTwit.Helpers;
using Our.Umbraco.uTwit.Models; 
using umbraco; 

namespace Our.Umbraco.uTwit
{
    public static class uTwit 
    {
        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="count">The count.</param>
        /// <param name="includeReplies">if set to <c>true</c> include replies.</param>
        /// <param name="includeRetweets">if set to <c>true</c> include retweets.</param>
        /// <returns></returns>
        public static IEnumerable<Status> GetLatestTweets(uTwitModel config,
            int count = 10,
            bool includeReplies = true,
            bool includeRetweets = true)
        {
            return GetLatestTweets(config,
                config.ScreenName,
                count,
                includeReplies,
                includeRetweets);
        }

        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="count">The count.</param>
        /// <param name="includeReplies">if set to <c>true</c> [include replies].</param>
        /// <param name="includeRetweets">if set to <c>true</c> [include retweets].</param>
        /// <returns></returns>
		public static IEnumerable<Status> GetLatestTweets(uTwitModel config,
            string screenName,
            int count = 10,
            bool includeReplies = true,
            bool includeRetweets = true)
        {
            if(string.IsNullOrWhiteSpace(config.Token)
                || string.IsNullOrWhiteSpace(config.TokenSecret)
                || string.IsNullOrWhiteSpace(screenName))
            {
                return Enumerable.Empty<Status>();
            }

            return GetLatestTweets(config.Token,
                config.TokenSecret,
                config.ConsumerKey,
                config.ConsumerSecret,
                screenName,
                count,
                includeReplies,
                includeRetweets);
        }

        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="count">The count.</param>
        /// <param name="includeReplies">if set to <c>true</c> include replies.</param>
        /// <param name="includeRetweets">if set to <c>true</c> include retweets.</param>
        /// <returns></returns>
        public static IEnumerable<Status> GetLatestTweets(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string screenName,
            int count = 10,
            bool includeReplies = true,
            bool includeRetweets = true)
        {
            if (string.IsNullOrWhiteSpace(consumerKey))
                consumerKey = Constants.ConsumerKey;

            if (string.IsNullOrWhiteSpace(consumerSecret))
                consumerSecret = Constants.ConsumerSecret;

            var session = CreateOAuthSession(consumerKey,
                consumerSecret,
                oauthToken,
                oauthTokenSecret);

            var url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&count={1}&exclude_replies={2}&include_rts={3}",
                screenName,
                count,
                (!includeReplies).ToString().ToLower(),
                includeRetweets.ToString().ToLower());

            return session.Request()
                .Get()
                .ForUrl(url)
                .ToString()
                .DeserializeJsonTo<List<Status>>(new[]
                {
                    new TwitterTypeConverter()
                });
        }


        /// Jay Greasley for Moriyama
        /// 
        /// 
        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="count">The count.</param>
        /// <param name="includeReplies">if set to <c>true</c> [include replies].</param>
        /// <param name="includeRetweets">if set to <c>true</c> [include retweets].</param>
        /// <returns></returns>
        public static IEnumerable<Status> GetHomeTimeline(uTwitModel config,
            string screenName,
            int count = 10,
            bool includeReplies = true,
            bool includeRetweets = true)
        {
            if (string.IsNullOrWhiteSpace(config.Token)
                || string.IsNullOrWhiteSpace(config.TokenSecret)
                || string.IsNullOrWhiteSpace(screenName))
            {
                return Enumerable.Empty<Status>();
            }

            return GetHomeTimeline(config.Token,
                config.TokenSecret,
                config.ConsumerKey,
                config.ConsumerSecret,
                screenName,
                count,
                includeReplies,
                includeRetweets);
        }

        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="count">The count.</param>
        /// <param name="includeReplies">if set to <c>true</c> include replies.</param>
        /// <param name="includeRetweets">if set to <c>true</c> include retweets.</param>
        /// <returns></returns>
        public static IEnumerable<Status> GetHomeTimeline(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string screenName,
            int count = 10,
            bool includeReplies = true,
            bool includeRetweets = true)
        {
            if (string.IsNullOrWhiteSpace(consumerKey))
                consumerKey = Constants.ConsumerKey;

            if (string.IsNullOrWhiteSpace(consumerSecret))
                consumerSecret = Constants.ConsumerSecret;

            var session = CreateOAuthSession(consumerKey,
                consumerSecret,
                oauthToken,
                oauthTokenSecret);

            var url = string.Format("https://api.twitter.com/1.1/statuses/home_timeline.json?screen_name={0}&count={1}",
                screenName,
                count,
                (!includeReplies).ToString().ToLower(),
                includeRetweets.ToString().ToLower());

            return session.Request()
                .Get()
                .ForUrl(url)
                .ToString()
                .DeserializeJsonTo<List<Status>>(new[]
                {
                    new TwitterTypeConverter()
                });
        }

        // Jay Greasley
        /// <summary>
        /// Searches the tweets.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="query">The query.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
		public static IEnumerable<Status> SearchTweets(uTwitModel config,
            string query,
            int count = 10)
        {
            return SearchTweets(config.Token,
                config.TokenSecret,
                config.ConsumerKey,
                config.ConsumerSecret,
                query,
                count);
        }

        /// <summary>
        /// Searches the tweets.
        /// </summary>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="query">The query.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static IEnumerable<Status> SearchTweets(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string query,
            int count = 10)
        {
            if (string.IsNullOrWhiteSpace(consumerKey))
                consumerKey = Constants.ConsumerKey;

            if (string.IsNullOrWhiteSpace(consumerSecret))
                consumerSecret = Constants.ConsumerSecret;

            var session = CreateOAuthSession(consumerKey,
                consumerSecret,
                oauthToken,
                oauthTokenSecret);

            var url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q={0}&count={1}",
                HttpUtility.UrlEncode(query),
                count);

            var results = session.Request()
                .Get()
                .ForUrl(url)
                .ToString()
                .DeserializeJsonTo<SearchResults>(new[]
                {
                    new TwitterTypeConverter()
                });

            return results != null ? results.Statuses : Enumerable.Empty<Status>();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
		public static User GetUser(uTwitModel config)
        {
            return GetUser(config, config.ScreenName);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <returns></returns>
		public static User GetUser(uTwitModel config,
            string screenName)
        {
            return GetUser(config.Token,
                config.TokenSecret,
                config.ConsumerKey,
                config.ConsumerSecret,
                screenName);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <returns></returns>
        public static User GetUser(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string screenName)
        {
            return GetUsers(oauthToken,
                oauthTokenSecret,
                consumerKey,
                consumerSecret,
                new[] { screenName }).SingleOrDefault();
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="screenNames">The screen names.</param>
        /// <returns></returns>
		public static IEnumerable<User> GetUsers(uTwitModel config,
            IEnumerable<string> screenNames)
        {
            return GetUsers(config.Token,
                config.TokenSecret,
                config.ConsumerKey,
                config.ConsumerSecret,
                screenNames);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="screenNames">The screen names.</param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            IEnumerable<string> screenNames)
        {

            if (string.IsNullOrWhiteSpace(consumerKey))
                consumerKey = Constants.ConsumerKey;

            if (string.IsNullOrWhiteSpace(consumerSecret))
                consumerSecret = Constants.ConsumerSecret;

            var session = CreateOAuthSession(consumerKey,
                consumerSecret,
                oauthToken,
                oauthTokenSecret);

            var url = string.Format("https://api.twitter.com/1.1/users/lookup.json?screen_name={0}",
                string.Join(",", screenNames));

            return session.Request()
                .Get()
                .ForUrl(url)
                .ToString()
                .DeserializeJsonTo<List<User>>(new[]
                {
                    new TwitterTypeConverter()
                });
        }

        /// <summary>
        /// Formats the date in the official Twitter format.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatDate(DateTime date)
        {
            var timeSpan = DateTime.Now - date;
 
            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                return timeSpan.Seconds + "s";
            }

            if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                return timeSpan.Minutes + "m";
            }

            if (timeSpan <= TimeSpan.FromHours(24))
            {
                return timeSpan.Hours + "h";
            }

            return date.ToString("d MMM");
        }

        /// <summary>
        /// Creates an OAuth session.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="oauthAccessToken">The oauth access token.</param>
        /// <param name="oauthAccessTokenSecret">The oauth access token secret.</param>
        /// <returns></returns>
        private static IOAuthSession CreateOAuthSession(string consumerKey,
            string consumerSecret,
            string oauthAccessToken,
            string oauthAccessTokenSecret)
        {
            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext,
                Constants.RequestTokenUrl,
                Constants.AuthorizeUrl,
                Constants.AccessTokenUrl)
            {
                AccessToken = new AccessToken
                {
                    ConsumerKey = consumerKey,
                    Token = oauthAccessToken,
                    TokenSecret = oauthAccessTokenSecret
                }
            };
        }
    }
}
