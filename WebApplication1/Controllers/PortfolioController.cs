using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rabobank.Training.Business.Interfaces;
using Rabobank.Training.ViewModels.Entities;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// THIS IS THE API CONTROLLER WHICH WILL HANDLE REST CALL FROM ANGULAR FRONT END TO RETRIEVE THE PORTFOLIO POSITIONS.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioProcessor portfolioProcessor = null;
        private readonly IConfiguration config;
        private readonly ILoggerFactory loggerFactory;

        /// <SUMMARY>
        /// CUNSTRUCTOR WILL INITIALIZE PORTFOLIOPROCESSOR TO RETRIEVE PORTFOLIO.
        /// .NET CORE INBUILT DEPENDENCY INJECTION FRAMEWORK HAS BEEN USED HERE.
        /// SERVICES ARE CONFIGURED IN STARTUP.CS FOR THIS PURPOSE.
        /// </summary>
        /// <param name="processor"></param>
        public PortfolioController(IPortfolioProcessor processor, IConfiguration config, ILoggerFactory loggerfactory)
        {
            portfolioProcessor = processor;
            this.config = config;
            this.loggerFactory = loggerfactory;
        }

        /// <summary>
        /// METHOD CALLED BY ANGULAR.
        /// ITS HTTP GET CALL.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PositionVM[] Get()
        {
            PositionVM[] positions = null;
            try
            {
                var fundsFilePath = config["FundsOfMandatesFile"];
                var portfolioViewModel = portfolioProcessor.GetUpdatedPortfolio(fundsFilePath);

                if (portfolioViewModel == null)
                {
                    throw new ArgumentException("Necessary Portfolio is not available to display");
                }
                if (portfolioViewModel.Positions == null || portfolioViewModel.Positions.Count ==0)
                {
                    throw new Exception("No Valid Positions found under portfolio.");
                }

                positions =  portfolioViewModel.Positions.ToArray();
            }            
            catch(Exception e)
            {
                var logger = loggerFactory.CreateLogger("Generic Logger");
                logger.LogError(e, "Error occered while retrieving Positions from Portfolio", null);
                throw e;
            }

            return positions;
        }

    }
}
