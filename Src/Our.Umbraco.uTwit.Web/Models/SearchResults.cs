using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "SearchResults")]
    public class SearchResults
    {
        [DataMember]
        public IList<Status> Statuses { get; set; }
    }
}