using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Our.Umbraco.uTwit.Extensions;

namespace Our.Umbraco.uTwit
{
    internal class Constants
    {
        internal static string ConsumerKey
        {
            get { return "O0zPY+FTZb9xhisPypYQFAGOGK/j7X4o".Decrypt(); }
        }

        internal static string ConsumerSecret
        {
            get { return "Z5vKVOyJuEDX3h9NBwM7R7Hik4aVUy5F/r6AaT2fy5YvunjsTEbZWQ==".Decrypt(); }
        }

        internal const string RequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        internal const string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";
        internal const string AccessTokenUrl = "https://api.twitter.com/oauth/access_token";

        internal const string VerifyCredentialsUrl = "https://api.twitter.com/1.1/account/verify_credentials.json";
    }
}
