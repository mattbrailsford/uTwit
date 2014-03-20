using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Our.Umbraco.uTwit.Extensions;
using umbraco.IO;
using umbraco.cms.businesslogic.datatype;
using umbraco.editorControls;
using umbraco.interfaces;

namespace Our.Umbraco.uTwit.DataType
{
    /// <summary>
    /// The Data Editor for the Twitter OAuth authentication data type
    /// </summary>
    public class TwitterOAuthDataEditor : Panel, IDataEditor
    {
        /// <summary>
        /// The IData for the data-type.
        /// </summary>
        private readonly IData _data;

        /// <summary>
        /// The options for the data-type.
        /// </summary>
        private readonly TwitterOAuthOptions _options;

        /// <summary>
        /// The data type definition id for the data-type;
        /// </summary>
        private readonly int _dtdId;

        /// <summary>
        /// Gets a value indicating whether to show a label or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if want to show label; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLabel
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether treat as rich text editor.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if treat as rich text editor; otherwise, <c>false</c>.
        /// </value>
        public bool TreatAsRichTextEditor
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        public Control Editor
        {
            get { return this; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterOAuthDataEditor"/> class.
        /// </summary>
        internal TwitterOAuthDataEditor(IData data, TwitterOAuthOptions options, int dtdId)
        {
            _data = data;
            _options = options;
            _dtdId = dtdId;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            EnsureChildControls();

            var val = new TwitterOAuthDataValue
            {
                ScreenName = hdnScreenName.Value,
                OAuthToken = hdnToken.Value,
                OAuthTokenSecret = hdnTokenSecret.Value,
                ConsumerKey = _options.ConsumerKey,
                ConsumerSecret = _options.ConsumerSecret
            };

            _data.Value = val.SerializeToJson();
        }

        private Panel ctrlWrapper = new Panel();

        private HiddenField hdnScreenName = new HiddenField();
        private HiddenField hdnToken = new HiddenField();
        private HiddenField hdnTokenSecret = new HiddenField();

        private Label lblValueWrapper = new Label();
        private Label lblScreenName = new Label();
        private HyperLink lnkForget = new HyperLink();
        private HyperLink lnkAuthorize = new HyperLink();

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Page.ClientScript.RegisterClientScriptResource(typeof(TwitterOAuthDataEditor), "Our.Umbraco.uTwit.Resources.Scripts.uTwit.js");

            EnsureChildControls();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(!Page.IsPostBack)
            {
                var val = _data.Value.ToString().DeserializeJsonTo<TwitterOAuthDataValue>();
                if(val != null
                    && !string.IsNullOrWhiteSpace(val.ScreenName)
                    && !string.IsNullOrWhiteSpace(val.OAuthToken)
                    && !string.IsNullOrWhiteSpace(val.OAuthTokenSecret))
                {
                    hdnScreenName.Value = val.ScreenName;
                    hdnToken.Value = val.OAuthToken;
                    hdnTokenSecret.Value = val.OAuthTokenSecret;

                    lblScreenName.Text = "@" + val.ScreenName;
                    lblValueWrapper.Attributes.Add("style", "display:inline;");
                }
                else
                {
                    lblValueWrapper.Attributes.Add("style", "display:none;");
                }
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(hdnScreenName.Value)
                    && !string.IsNullOrWhiteSpace(hdnToken.Value)
                    && !string.IsNullOrWhiteSpace(hdnTokenSecret.Value))
                {
                    lblScreenName.Text = "@" + hdnScreenName.Value;
                    lblValueWrapper.Attributes.Add("style", "display:inline;");
                }
                else
                {
                    lblValueWrapper.Attributes.Add("style", "display:none;");
                }
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            hdnScreenName.ID = ID + "_hdnScreenName";
            hdnToken.ID = ID + "_hdnToken";
            hdnTokenSecret.ID = ID + "_hdnTokenSecret";

            lblValueWrapper.ID = ID + "_lblValueWrapper";
            lblScreenName.ID = ID + "_lblScreenName";

            lnkForget.ID = ID + "_lnkForget";
            lnkForget.Text = Our.Umbraco.uTwit.Web.UI.App_GlobalResources.uTwit.lbl_forget;
            lnkForget.NavigateUrl = "#";
            lnkForget.Attributes.Add("style", "color:red;");

            lblValueWrapper.Controls.Add(lblScreenName);
            lblValueWrapper.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            lblValueWrapper.Controls.Add(lnkForget);
            lblValueWrapper.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            lnkAuthorize.ID = ID + "_lnkAuthorize";
            lnkAuthorize.Text = Our.Umbraco.uTwit.Web.UI.App_GlobalResources.uTwit.lbl_authorize;
            lnkAuthorize.NavigateUrl = "#";

            ctrlWrapper.ID = ID + "_pnlWrapper";

            ctrlWrapper.Controls.Add(hdnScreenName);
            ctrlWrapper.Controls.Add(hdnToken);
            ctrlWrapper.Controls.Add(hdnTokenSecret);

            ctrlWrapper.Controls.Add(lblValueWrapper);
            ctrlWrapper.Controls.Add(lnkAuthorize);

            Controls.Add(ctrlWrapper);
            
            // We need to add the controls before we access ClientID's, so we set the onClick last
            lnkAuthorize.Attributes["onClick"] = CreateAuthorizeLink(ctrlWrapper.ClientID);
            lnkForget.Attributes["onClick"] = CreateForgetLink(ctrlWrapper.ClientID);
        }

        /// <summary>
        /// Renders the control to the specified HTML writer.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            ctrlWrapper.RenderControl(writer);
        }

        /// <summary>
        /// Creates the authorize link.
        /// </summary>
        /// <param name="wrapperId">The wrapper id.</param>
        /// <returns></returns>
        protected string CreateAuthorizeLink(string wrapperId)
        {
            return string.Format("javascript:window.open('{0}/plugins/utwit/twitteroauth1callback.aspx?dtdid={1}&wrapperId={2}', 'Authorize', 'scrollbars=no,resizable=yes,menubar=no,width=800,height=600');return false;",
                IOHelper.ResolveUrl(SystemDirectories.Umbraco),
                _dtdId,
                wrapperId);
        }

        /// <summary>
        /// Creates the forget link.
        /// </summary>
        /// <param name="wrapperId">The wrapper id.</param>
        /// <returns></returns>
        protected string CreateForgetLink(string wrapperId)
        {
            return string.Format("javascript:uTwit_ClearValue('{0}');return false;",
                wrapperId);
        }
    }
}
