using System.Collections.Generic;
using Rabobank.Training.Business.Interfaces;
using Rabobank.Training.ClassLibrary.DomainEntities;
using Rabobank.Training.ViewModels.Entities;

namespace Rabobank.Training.BusinessLayer
{
    /// <summary>
    /// Class deals with getting the updated Portfolio by calling FundProcessor for all data manupulation based on requirement.
    /// </summary>
    public class PortfolioProcessor : IPortfolioProcessor
    {
        public PortfolioProcessor(IFundsProcessor fundsProcessor)
        {
            FundsProcessor = fundsProcessor;
        }

        public IFundsProcessor FundsProcessor { get; set; }

        public PortfolioVM GetUpdatedPortfolio(string fileName)
        {
            PortfolioVM portfolioVM = null;
            List<FundOfMandates> mandates = null;
           // IFundsProcessor fundProcessor = new FundProcessor(); //dependency injection is possible in these kind of cases

            portfolioVM = FundsProcessor.GetPortfolio();
            mandates = FundsProcessor.ReadFundOfMandatesFile(fileName);

            portfolioVM.Positions.ForEach(position =>
            {
                mandates.ForEach(fundofmandate =>
                {
                    position = FundsProcessor.GetCalculatedMandates(position, fundofmandate);
                });
            });

            return portfolioVM;
        }
    }

}
