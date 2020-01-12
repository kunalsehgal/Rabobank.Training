using System.Collections.Generic;
namespace Rabobank.Training.ViewModels.Entities
{
    /// <summary>
    /// View Model class to be used by Front End.
    /// Comtains list of Positions which are currently static based on the given requirement.
    /// </summary>
    public class PortfolioVM
    {
        public List<PositionVM> Positions { get; set; }
    }
}
