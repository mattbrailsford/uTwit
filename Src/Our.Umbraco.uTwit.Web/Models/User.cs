using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "User")]
    public class User
    {
        [DataMember]
        public string Name { get; internal set; }

        [DataMember]
        public string ScreenName { get; internal set; }

        [DataMember]
        public string ProfileImageUrl { get; internal set; }

        [DataMember]
        public string ProfileImageUrlHttps { get; internal set; }

        [DataMember]
        public string Description { get; internal set; }

        [DataMember]
        public string Location { get; internal set; }

        [DataMember]
        public string Url { get; internal set; }

        [DataMember]
        public int FavouritesCount { get; internal set; } 

        [DataMember]
        public int FollowersCount { get; internal set; }

        [DataMember]
        public int StatusesCount { get; internal set; }

        [DataMember]
        public int ListedCount { get; internal set; }

        [DataMember]
        public int FriendsCount { get; internal set; } 

        [DataMember]
        public string PermalinkUrl
        {
            get
            {
                return string.Format("https://twitter.com/{0}",
                    ScreenName);
            }
            internal set
            {
                // Don't do anything
            }
        }
    }
}