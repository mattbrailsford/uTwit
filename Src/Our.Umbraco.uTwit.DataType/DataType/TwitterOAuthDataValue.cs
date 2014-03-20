using System;
using System.Collections.Generic;
using System.Text;

namespace Our.Umbraco.uTwit.DataType
{
    /// <summary>
    /// The Data Value for the Twitter OAuth authentication data type
    /// </summary>
    public class TwitterOAuthDataValue
    {
        /// <summary>
        /// Gets or sets the screen name of the user.
        /// </summary>
        /// <value>
        /// The screen name.
        /// </value>
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the OAuth token.
        /// </summary>
        /// <value>
        /// The OAuth token.
        /// </value>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the OAuth token secret.
        /// </summary>
        /// <value>
        /// The OAuth token secret.
        /// </value>
        public string OAuthTokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the applications consumer key.
        /// </summary>
        /// <value>
        /// The consumer key.
        /// </value>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the applications consumer secret.
        /// </summary>
        /// <value>
        /// The consumer secret.
        /// </value>
        public string ConsumerSecret { get; set; }
    }
}
