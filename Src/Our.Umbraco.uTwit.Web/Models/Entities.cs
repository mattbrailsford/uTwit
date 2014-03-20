using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "Entities")]
    public class Entities
    {
        [DataMember]
        public IEnumerable<UrlEntity> Urls { get; internal set; }

        [DataMember]
        public IEnumerable<UserMentionEntity> UserMentions { get; internal set; }

        [DataMember]
        public IEnumerable<HashtagEntity> Hashtags { get; internal set; }
    }
}