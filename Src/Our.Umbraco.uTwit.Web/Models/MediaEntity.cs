using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "Media")]
    public class MediaEntity : Entity
    {
        [DataMember]
        public string Type { get; internal set; }

        [DataMember]
        public string MediaUrl { get; internal set; }

        [DataMember]
        public string DisplayUrl { get; internal set; }

     

    }
}