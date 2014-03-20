using System;
using System.Linq;
using System.Web.UI;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Our.Umbraco.uTwit.Extensions;
using Our.Umbraco.uTwit.Helpers;
using Our.Umbraco.uTwit.Models;

namespace Our.Umbraco.uTwit.Web.UI.App_Plugins.uTwit
{
    public partial class TwitterOAuth1Callback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
	        var consumerKey = Constants.ConsumerKey;
	        var consumerSecret = Constants.ConsumerSecret;

			RequestOptions options;
			if (Request.QueryString.AllKeys.Contains("o")
				&& !string.IsNullOrWhiteSpace(Request.QueryString["o"])
				&& RequestOptions.TryParse(Request.QueryString["o"], out options)
				&& options != null)
			{  
				try
				{
					var preval = uTwitHelper.GetPreValueOptionsByAlias(options.ContentTypeAlias,
						options.PropertyAlias);

					if (preval != null 
						&& preval.ContainsKey("consumerKey") && !string.IsNullOrWhiteSpace(preval["consumerKey"])
						&& preval.ContainsKey("consumerSecret") && !string.IsNullOrWhiteSpace(preval["consumerSecret"]))
					{
						consumerKey = preval["consumerKey"];
						consumerSecret = preval["consumerSecret"];
					}

					var session = CreateOAuthSession(consumerKey, consumerSecret);

					var requestTokenString = Request[Parameters.OAuth_Token];
					var verifier = Request[Parameters.OAuth_Verifier];

					if (string.IsNullOrEmpty(verifier))
					{
						var requestToken = session.GetRequestToken();

						Session[requestToken.Token] = requestToken;

						Response.Redirect(session.GetUserAuthorizationUrlForToken(requestToken), true);
					}
					else
					{
						var requestToken = (IToken)Session[requestTokenString];

						var accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, verifier);

						var accountSettings = session
							.Request()
							.Get()
							.ForUrl(Constants.VerifyCredentialsUrl)
							.ToString()
							.DeserializeJsonTo<User>();

						Page.ClientScript.RegisterClientScriptBlock(typeof(TwitterOAuth1Callback), "callback", @"<script>
							self.opener.angular.element('#uTwit_" + options.ScopeId + @"').scope()." + options.Callback + @"({ screenName: '" + accountSettings.screen_name + @"', token:'" + accessToken.Token + @"', tokenSecret:'" + accessToken.TokenSecret + @"' });
							window.close();
							</script>");
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException("Unable to retreive prevalue options for property editor", ex);
				}
			}
        }

        private IOAuthSession CreateOAuthSession(string consumerKey,
            string consumerSecret)
        {
            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext,
                Constants.RequestTokenUrl,
                Constants.AuthorizeUrl,
                Constants.AccessTokenUrl,
                Request.Url.ToString())
                    .RequiresCallbackConfirmation(); 
        }
    }
}