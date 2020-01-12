using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rabobank.Training.Business.Interfaces;
using Rabobank.Training.ViewModels.Entities;
using WebApplication1.Controllers;

namespace Rabobank.Training.WebApp.Test
{
    [TestClass]
    public class PortfolioControllerTest
    {
        private IPortfolioProcessor portfolioProcessor = null;
        private IConfiguration config;
        private ILoggerFactory loggerFactory;

        [TestMethod]
        public void ShouldReturnCorrectPortfolioObjectBackAndRunSuccessfully()
        {
            portfolioProcessor = Substitute.For<IPortfolioProcessor>();
            config = Substitute.For<IConfiguration>();
            loggerFactory = Substitute.For<ILoggerFactory>();
            config["FundsOfMandatesFile"] = "FundOfMandatesDataV1.xml";
            loggerFactory.ClearReceivedCalls();

            var dummyPortfolio = new PortfolioVM
            {
                Positions = new List<PositionVM> {

                     new PositionVM { Code="NL0000287100", Name="Henekens", Value=12345 },
                     new PositionVM { Code="NL000029332", Name="Optimix", Value=23456 },
                     new PositionVM { Code="NL0000440584", Name="DP Global", Value=34567 },
                     new PositionVM { Code="NL0000440588", Name="Rabobank core", Value=45678 },
                     new PositionVM { Code="inc005", Name="Morgan Stanley", Value=56789 },
                     new PositionVM { Code="inc005", Name="Morgan Stanley", Value=56789 }
                    }
            };

            portfolioProcessor.GetUpdatedPortfolio(Arg.Any<string>()).Returns(dummyPortfolio);

            PortfolioController controller = new PortfolioController(portfolioProcessor, config, loggerFactory);
            var httpResult = controller.Get();
            httpResult.Should().HaveCount(6, "Because we passed 6 Positions in dummy return object");

        }


        [TestMethod]
        public void ShouldThrowArgumentExceptionIfPortfolioReturnedIsNull()
        {
            portfolioProcessor = Substitute.For<IPortfolioProcessor>();
            config = Substitute.For<IConfiguration>();
            loggerFactory = Substitute.For<ILoggerFactory>();
            config["FundsOfMandatesFile"] = "FundOfMandatesDataV1.xml";
            loggerFactory.ClearReceivedCalls();
            PortfolioVM portfolio = null;

            portfolioProcessor.GetUpdatedPortfolio(Arg.Any<string>()).Returns(portfolio);

            PortfolioController controller = new PortfolioController(portfolioProcessor, config, loggerFactory);
            Func<PositionVM[]> func = () => controller.Get();

            func.Should().Throw<ArgumentException>("Because GetPortfolio returns null here and client code mus throw an Argument Exception").WithMessage("Necessary Portfolio is not available to display");

        }



        [TestMethod]
        public void ShouldThrowExceptionIfPortfolioObjectDoesNowHavePositions()
        {
            portfolioProcessor = Substitute.For<IPortfolioProcessor>();
            config = Substitute.For<IConfiguration>();
            loggerFactory = Substitute.For<ILoggerFactory>();
            config["FundsOfMandatesFile"] = "FundOfMandatesDataV1.xml";
            loggerFactory.ClearReceivedCalls();
            PortfolioVM portfolio = new PortfolioVM
            {
                Positions = null

            };

            portfolioProcessor.GetUpdatedPortfolio(Arg.Any<string>()).Returns(portfolio);

            PortfolioController controller = new PortfolioController(portfolioProcessor, config, loggerFactory);
            Func<PositionVM[]> func = () => controller.Get();

            func.Should().Throw<Exception>("Because GetPortfolio returns no Positions in Portfolio and client code must throw an exception").WithMessage("No Valid Positions found under portfolio.");

        }

    }
}

