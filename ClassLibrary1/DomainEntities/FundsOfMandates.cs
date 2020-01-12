namespace Rabobank.Training.ClassLibrary.DomainEntities
{
    /// <summary>
    /// This class is a wrapper class. 
    /// A collection of FundOfMandates
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class FundsOfMandates
    {

        private FundOfMandates[] fundOfMandatesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FundOfMandates")]
        public FundOfMandates[] FundOfMandates
        {
            get
            {
                return this.fundOfMandatesField;
            }
            set
            {
                this.fundOfMandatesField = value;
            }
        }
    }
}
