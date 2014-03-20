using System;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Our.Umbraco.uTwit.DataType
{
    /// <summary>
    /// A Twitter OAuth authentication data type
    /// </summary>
    public class TwitterOAuthDataType : AbstractDataEditor
    {
        /// <summary>
        /// The Data Editor for the data-type.
        /// </summary>
        private TwitterOAuthDataEditor _dataEditor;

        /// <summary>
        /// The PreValue Editor for the data-type.
        /// </summary>
        private TwitterOAuthPreValueEditor _preValueEditor;

        /// <summary>
        /// The Data for the data-type.
        /// </summary>
        private TwitterOAuthData _data;

        /// <summary>
        /// Gets the id of the data-type.
        /// </summary>
        /// <value>
        /// The id of the data-type.
        /// </value>
        public override Guid Id
        {
            get { return new Guid("3ADC3568-4231-4A90-8E5F-327C9537551C"); }
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        /// <value>
        /// The name of the data type.
        /// </value>
        public override string DataTypeName
        {
            get { return "Twitter OAuth Data Type"; }
        }

        /// <summary>
        /// Gets the prevalue editor.
        /// </summary>
        /// <value>
        /// The prevalue editor.
        /// </value>
        public override IDataEditor DataEditor
        {
            get { return _dataEditor ?? (_dataEditor = new TwitterOAuthDataEditor(Data, ((TwitterOAuthPreValueEditor)PrevalueEditor).GetPreValueOptions<TwitterOAuthOptions>(), DataTypeDefinitionId)); }
        }

        /// <summary>
        /// Gets the prevalue editor.
        /// </summary>
        /// <value>
        /// The prevalue editor.
        /// </value>
        public override IDataPrevalue PrevalueEditor
        {
            get { return _preValueEditor ?? (_preValueEditor = new TwitterOAuthPreValueEditor(this)); }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public override IData Data
        {
            get { return _data ?? (_data = new TwitterOAuthData(this, ((TwitterOAuthPreValueEditor)PrevalueEditor).GetPreValueOptions<TwitterOAuthOptions>())); }
        }
    }
}
