
using System.Collections.Generic;
using Rabobank.Training.ClassLibrary.DomainEntities;
using Rabobank.Training.ViewModels.Entities;

namespace Rabobank.Training.Business.Interfaces
{
    /// <summary>
    /// Interface exposes set of functions used for processing of file and data massaging for View Model classes.
    /// </summary>
    public interface IFundsProcessor
    {
        List<FundOfMandates> ReadFundOfMandatesFile(string fileName);
        PortfolioVM GetPortfolio();
        PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fundOfmandates);
    }
}
