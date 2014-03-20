using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using Our.Umbraco.uTwit.Models;

namespace Our.Umbraco.uTwit.Converters
{
    internal class TwitterTypeConverter : JavaScriptConverter
    {
        private const string _dateFormat = "ddd MMM dd HH:mm:ss zzz yyyy";

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new[] { typeof(object) };
            }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            var entity = Activator.CreateInstance(type);
            var props = entity.GetType().GetProperties();

            foreach (var key in dictionary.Keys)
            {
                var prop = props.FirstOrDefault(t => t.Name.ToLower() == key.Replace("_", "").ToLower());
                if (prop != null) 
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(entity, DateTime.ParseExact(dictionary[key] as string, _dateFormat, CultureInfo.InvariantCulture), null);

                    }
                    else if (prop.PropertyType == typeof(IHtmlString))
                    {
                        prop.SetValue(entity, new HtmlString(dictionary[key] as string), null);
                    }
                    else
                    {
                        prop.SetValue(entity, serializer.ConvertToType(dictionary[key], prop.PropertyType), null);
                    }
                }
            }

            return entity;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    
}