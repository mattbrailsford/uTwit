using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "UserMention")]
    public class UserMentionEntity : Entity
    {
        [DataMember]
        public long Id { get; internal set; }

        [DataMember]
        public string IdStr { get; internal set; }

        [DataMember]
        public string ScreenName { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }
    }
}