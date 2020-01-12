using Rabobank.Training.ViewModels.Entities;

namespace Rabobank.Training.Business.Interfaces
{
    /// <summary>
    /// This interface exposes a method which only deals with extracting updated portfolio.
    /// It takes input parameter as a file holding Funds and Mandates information in the form of XML.
    /// </summary>
    public interface  IPortfolioProcessor
    {
         PortfolioVM GetUpdatedPortfolio(string fileName);
    }
}
