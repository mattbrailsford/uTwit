using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "Hashtag")]
    public class HashtagEntity : Entity
    {
        [DataMember]
        public string Text { get; internal set; }
    }
}