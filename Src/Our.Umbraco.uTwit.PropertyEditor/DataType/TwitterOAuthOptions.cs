using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Our.Umbraco.uTwit.PropertyEditor
{
    /// <summary>
    /// Options for the Twitter OAuth authentication data type
    /// </summary>
    public class TwitterOAuthOptions
    {
        /// <summary>
        /// Gets or sets the consumer key.
        /// </summary>
        /// <value>
        /// The consumer key.
        /// </value>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>
        /// The consumer secret.
        /// </value>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>
        /// The data format.
        /// </value>
        public TwitterOAuthDataFormat DataFormat { get; set; }
    }
}
