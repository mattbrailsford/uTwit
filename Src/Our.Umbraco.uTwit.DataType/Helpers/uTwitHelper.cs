using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.uTwit.DataType;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uTwit.Helpers
{
    internal static class uTwitHelper
    {
        /// <summary>
        /// Gets the pre value options by id.
        /// </summary>
        /// <param name="dtdId">The DateTypeDefinition id.</param>
        /// <returns></returns>
        internal static TwitterOAuthOptions GetPreValueOptionsById(int dtdId)
        {
            var dtd = new DataTypeDefinition(dtdId);
            var pve = (TwitterOAuthPreValueEditor)dtd.DataType.PrevalueEditor;
            return pve.GetPreValueOptions<TwitterOAuthOptions>();
        }
    }
}