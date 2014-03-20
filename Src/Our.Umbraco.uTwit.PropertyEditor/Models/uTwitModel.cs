using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
	public class uTwitModel
	{
		public string ScreenName { get; set; }
		public string Token { get; set; }
		public string TokenSecret { get; set; }
		public string ConsumerKey { get; set; }
		public string ConsumerSecret { get; set; }
	}
}