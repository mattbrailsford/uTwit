using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using Our.Umbraco.uTwit.Extensions;
using umbraco;

namespace Our.Umbraco.uTwit
{
    //[XsltExtension("uTwit")]
    public class uTwitXslt
    {
        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="count">The count.</param>
        /// <param name="includeReplies">if set to <c>true</c> include replies.</param>
        /// <param name="includeRetweets">if set to <c>true</c> include retweets.</param>
        /// <returns></returns>
        public static XPathNavigator GetLatestTweets(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string screenName,
            int count,
            bool includeReplies,
            bool includeRetweets)
        {
            var tweets = uTwit.GetLatestTweets(oauthToken,
                oauthTokenSecret,
                consumerKey,
                consumerSecret,
                screenName,
                count,
                includeReplies,
                includeRetweets);

            return tweets.ToXml(new Dictionary<string, string>
            {
                { "arrayofstatus", "statuses" }
            });
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
        public static XPathNavigator SearchTweets(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string query,
            int count = 10)
        {
            var tweets = uTwit.SearchTweets(oauthToken,
                oauthTokenSecret,
                consumerKey,
                consumerSecret,
                query,
                count);

            return tweets.ToXml(new Dictionary<string, string>
            {
                { "arrayofstatus", "statuses" }
            });
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="screenName">The screen name.</param>
        /// <returns></returns>
        public static XPathNavigator GetUser(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string screenName)
        {
            var user = uTwit.GetUser(oauthToken,
                oauthTokenSecret,
                consumerKey,
                consumerSecret,
                screenName);

            return user.ToXml();
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="oauthTokenSecret">The oauth token secret.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="screenNames">The screen names.</param>
        /// <returns></returns>
        public static XPathNavigator GetUsers(string oauthToken,
            string oauthTokenSecret,
            string consumerKey,
            string consumerSecret,
            string screenNames)
        {
            var user = uTwit.GetUsers(oauthToken,
                oauthTokenSecret,
                consumerKey,
                consumerSecret,
                screenNames.Split(','));

            return user.ToXml(new Dictionary<string, string>
            {
                { "arrayofuser", "users" }
            });
        }

        /// <summary>
        /// Formats the date in the official Twitter format.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatDate(DateTime date)
        {
            return uTwit.FormatDate(date);
        }
    }
}