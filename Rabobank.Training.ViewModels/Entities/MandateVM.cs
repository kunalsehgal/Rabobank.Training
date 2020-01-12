namespace Rabobank.Training.ViewModels.Entities
{
    /// <summary>
    /// View Model class to be used by Front End.
    /// Maps to Mandate Entity
    /// </summary>
    public class MandateVM
    {
        public string name { get; set; }
        public decimal Allocation { get; set; }

        public decimal Value { get; set; }
    }
}
