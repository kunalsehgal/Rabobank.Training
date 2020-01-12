namespace Rabobank.Training.ClassLibrary.DomainEntities
{
    /// <summary>
    /// A mandate which is a fund or fund of Mandates under fund of Mandates
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class Mandate
    {

        private string mandateIdField;

        private string mandateNameField;

        private decimal allocationField;

        /// <remarks/>
        public string MandateId
        {
            get
            {
                return this.mandateIdField;
            }
            set
            {
                this.mandateIdField = value;
            }
        }

        /// <remarks/>
        public string MandateName
        {
            get
            {
                return this.mandateNameField;
            }
            set
            {
                this.mandateNameField = value;
            }
        }

        /// <remarks/>
        public decimal Allocation
        {
            get
            {
                return this.allocationField;
            }
            set
            {
                this.allocationField = value;
            }
        }
    }
}