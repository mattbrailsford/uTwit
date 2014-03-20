using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "Entity")]
    public class Entity
    {
        [DataMember]
        public int[] Indices { get; internal set; }
    }
}