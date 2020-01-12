namespace Rabobank.Training.ClassLibrary.DomainEntities
{
    /// <summary>
    /// A wrapper class.
    /// Contains a collection of Mandates
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class Mandates
    {

        private Mandate[] mandateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Mandate")]
        public Mandate[] Mandate
        {
            get
            {
                return this.mandateField;
            }
            set
            {
                this.mandateField = value;
            }
        }
    }
}