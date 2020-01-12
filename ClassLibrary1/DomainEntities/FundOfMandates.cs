namespace Rabobank.Training.ClassLibrary.DomainEntities
{

    /// <summary>
    /// This class holds all details of FundOfMandate
    /// A FundOfMandates object
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class FundOfMandates
    {

        private string instrumentCodeField;

        private string instrumentNameField;

        private decimal liquidityAllocationField;

        private Mandate[] mandatesField;

        /// <remarks/>
        public string InstrumentCode
        {
            get
            {
                return this.instrumentCodeField;
            }
            set
            {
                this.instrumentCodeField = value;
            }
        }

        /// <remarks/>
        public string InstrumentName
        {
            get
            {
                return this.instrumentNameField;
            }
            set
            {
                this.instrumentNameField = value;
            }
        }

        /// <remarks/>
        public decimal LiquidityAllocation
        {
            get
            {
                return this.liquidityAllocationField;
            }
            set
            {
                this.liquidityAllocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Mandate", IsNullable = false)]
        public Mandate[] Mandates
        {
            get
            {
                return this.mandatesField;
            }
            set
            {
                this.mandatesField = value;
            }
        }
    }
}
