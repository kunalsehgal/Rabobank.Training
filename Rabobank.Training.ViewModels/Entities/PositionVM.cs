using System.Collections.Generic;
namespace Rabobank.Training.ViewModels.Entities
{
    /// <summary>
    /// View Model class to be used by Front End.
    /// Contains position information along with list of mapped MandateVM objects.
    /// </summary>
    public class PositionVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public List<MandateVM> Mandates { get; set; }
    }
}
