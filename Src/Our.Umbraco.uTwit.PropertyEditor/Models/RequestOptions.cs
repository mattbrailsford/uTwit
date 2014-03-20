using System;
using System.Text;
using Our.Umbraco.uTwit.Extensions;

namespace Our.Umbraco.uTwit.Models
{
	internal class RequestOptions
	{
		public string ContentTypeAlias { get; set; }
		public string PropertyAlias { get; set; }
		public string ScopeId { get; set; }
		public string Callback { get; set; }

		public static bool TryParse(string input, out RequestOptions opts)
		{
			opts = null;

			try
			{
				var base64 = input.Replace('*', '=').Replace('_', '/').Replace('-', '+');
				var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
				opts = json.DeserializeJsonTo<RequestOptions>();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}