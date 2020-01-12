namespace Rabobank.Training.ClassLibrary.DomainEntities
{
    /// <summary>
    /// Class is root of all Funds of Mandates.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class FundsOfMandatesData
    {

        private FundOfMandates[] fundsOfMandatesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FundOfMandates", IsNullable = false)]
        public FundOfMandates[] FundsOfMandates
        {
            get
            {
                return this.fundsOfMandatesField;
            }
            set
            {
                this.fundsOfMandatesField = value;
            }
        }
    }
}
