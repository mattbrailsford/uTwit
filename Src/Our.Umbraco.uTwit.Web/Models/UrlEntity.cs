using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "Url")]
    public class UrlEntity : Entity
    {
        [DataMember]
        public string Url { get; internal set; }

        [DataMember]
        public string DisplayUrl { get; internal set; }

        [DataMember]
        public string ExpandedUrl { get; internal set; }
    }
}