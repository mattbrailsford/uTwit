using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Our.Umbraco.uTwit.DataType;
using Our.Umbraco.uTwit.Extensions;
using Our.Umbraco.uTwit.Helpers;
using Our.Umbraco.uTwit.Models;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uTwit.Web.UI.Umbraco.Plugins.uTwit
{
    public partial class TwitterOAuth1Callback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var consumerKey = Constants.ConsumerKey;
            var consumerSecret = Constants.ConsumerSecret;

            int dtdId;
            if (Request.QueryString.AllKeys.Contains("dtdid")
                && !string.IsNullOrWhiteSpace(Request.QueryString["dtdid"])
                && int.TryParse(Request.QueryString["dtdid"], out dtdId)
                && dtdId > 0)
            {
                try
                {
                    var opts = uTwitHelper.GetPreValueOptionsById(dtdId);

                    if (!string.IsNullOrWhiteSpace(opts.ConsumerKey)
                        || !string.IsNullOrWhiteSpace(opts.ConsumerSecret))
                    {
                        consumerKey = opts.ConsumerKey;
                        consumerSecret = opts.ConsumerSecret;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to retreive prevalue options for the data type with the id: " + dtdId, ex);
                }
            }

            var session = CreateOAuthSession(consumerKey, consumerSecret);

            var requestTokenString = Request[Parameters.OAuth_Token];
            var verifier = Request[Parameters.OAuth_Verifier];

            if(string.IsNullOrEmpty(verifier))
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
                    self.opener.uTwit_SetValue('" + Request.QueryString["wrapperId"] + @"', '" + accountSettings.screen_name + @"', '" + accessToken.Token + @"', '" + accessToken.TokenSecret + @"');
                    window.close();
                    </script>");
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