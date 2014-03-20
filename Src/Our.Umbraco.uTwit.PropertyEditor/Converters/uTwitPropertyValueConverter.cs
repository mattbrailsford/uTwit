using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.uTwit.Extensions;
using Our.Umbraco.uTwit.Models;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Our.Umbraco.uTwit.Converters
{
	[PropertyValueType(typeof(uTwitModel))]
	[PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
	public class uTwitPropertyValueConverter : PropertyValueConverterBase
	{
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
			return propertyType.PropertyEditorAlias.Equals("Our.Umbraco.uTwit");
		}

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
			try
			{
				if (source != null && !source.ToString().IsNullOrWhiteSpace())
				{
					return source.ToString().DeserializeJsonTo<uTwitModel>();
				}
			}
			catch (Exception e)
			{
				LogHelper.Error<uTwitPropertyValueConverter>("Error converting uTwit value", e);
			}

			return null;
		}
	}
}