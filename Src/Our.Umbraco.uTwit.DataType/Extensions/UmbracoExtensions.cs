using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uTwit.Extensions
{
    internal static class UmbracoExtensions
    {
        /// <summary>
        /// Registers the embedded client resource.
        /// </summary>
        /// <param name="ctl">The control.</param>
        /// <param name="resourceContainer">The resource container.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="type">The type.</param>
        public static void RegisterEmbeddedClientResource(this Control ctl, Type resourceContainer, string resourceName, ClientDependencyType type)
        {
            ctl.Page.RegisterEmbeddedClientResource(resourceContainer, resourceName, type);
        }

        /// <summary>
        /// Registers the embedded client resource.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="resourceContainer">The type containing the embedded resource</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="type">The type.</param>
        public static void RegisterEmbeddedClientResource(this Page page, Type resourceContainer, string resourceName, ClientDependencyType type)
        {
            var target = page.Header;

            // if there's no <head runat="server" /> don't throw an exception.
            if (target != null)
            {
                switch (type)
                {
                    case ClientDependencyType.Css:
                        // get the urls for the embedded resources
                        var resourceUrl = page.ClientScript.GetWebResourceUrl(resourceContainer, resourceName);
                        var link = new HtmlLink();
                        link.Attributes.Add("href", resourceUrl);
                        link.Attributes.Add("type", "text/css");
                        link.Attributes.Add("rel", "stylesheet");
                        target.Controls.Add(link);
                        break;

                    case ClientDependencyType.Javascript:
                        page.ClientScript.RegisterClientScriptResource(resourceContainer, resourceName);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Adds the prevalue controls.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="controls">The controls.</param>
        public static void AddPrevalueControls(this ControlCollection collection, params Control[] controls)
        {
            foreach (var control in controls)
            {
                collection.Add(control);
            }
        }

        /// <summary>
        /// Adds the prevalue row heading.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="heading">The heading.</param>
        public static void AddPrevalueHeading(this HtmlTextWriter writer, string heading)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "row clearfix");
            writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'row'

            writer.RenderBeginTag(HtmlTextWriterTag.H3); // start 'h3'

            writer.Write(heading);

            writer.RenderEndTag(); // end 'h3'

            writer.RenderEndTag(); // end 'row'
        }

        /// <summary>
        /// Adds a new row to the Prevalue Editor.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter.</param>
        /// <param name="label">The label for the field.</param>
        /// <param name="controls">The controls for the field.</param>
        public static void AddPrevalueRow(this HtmlTextWriter writer, string label, params Control[] controls)
        {
            writer.AddPrevalueRow(label, string.Empty, controls);
        }

        /// <summary>
        /// Adds a new row to the Prevalue Editor, (with an optional description).
        /// </summary>
        /// <param name="writer">The HtmlTextWriter.</param>
        /// <param name="label">The label for the field.</param>
        /// <param name="description">The description for the field.</param>
        /// <param name="controls">The controls for the field.</param>
        public static void AddPrevalueRow(this HtmlTextWriter writer, string label, string description, params Control[] controls)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "row clearfix");
            writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'row'

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "label");
            writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'label'

            var lbl = new HtmlGenericControl("label") { InnerText = label };

            if (controls.Length > 0 && !string.IsNullOrEmpty(controls[0].ClientID))
            {
                lbl.Attributes.Add("for", controls[0].ClientID);
            }

            lbl.RenderControl(writer);

            writer.RenderEndTag(); // end 'label'

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "field");
            writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'field'

            foreach (var control in controls)
            {
                control.RenderControl(writer);
            }

            writer.RenderEndTag(); // end 'field'

            if (!string.IsNullOrEmpty(description))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "description");
                writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'description'

                var desc = new Literal() { Text = description };
                desc.RenderControl(writer);

                writer.RenderEndTag(); // end 'description'
            }

            writer.RenderEndTag(); // end 'row'
        }
    }
}