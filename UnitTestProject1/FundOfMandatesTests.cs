using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Rabobank.Training.ClassLibrary.DomainEntities;
using Rabobank.Training.ViewModels.Entities;
using Rabobank.Training.BusinessLayer;
using Rabobank.Training.Business.Interfaces;
using System;

namespace UnitTestProject1
{

    /// <summary>
    /// Need to see how to use Fluent Assertions in this test project
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithValidFile.xml")]

        //TEST IF TO TEST FUNCTIONALITY OF READFUNDOFMANDATESFILE
        public void ShouldReadValidXMLFileCorrectly()
        {
            //Arrange

            IFundsProcessor fileProcessor = new FundProcessor();
            int counOfMandatesIntestFile = 4;
            string filePath = "FundOfMandatesDataWithValidFile.xml";

            //Act

            var funds = fileProcessor.ReadFundOfMandatesFile(filePath);


            //Assert

            Assert.IsNotNull(funds);
            Assert.AreEqual(funds.Count, counOfMandatesIntestFile); //Assuming we already know count of mandates inside the test file.
            Assert.IsInstanceOfType(funds, typeof(List<FundOfMandates>));

            //FLUENT ASSERTIONS IN USE
            funds.Should().HaveCount(counOfMandatesIntestFile, "we have passed 4 mandates to XML");
            funds.Should().NotBeNull();
            funds.Should().BeAssignableTo(typeof(List<FundOfMandates>));

        }


        //TEST IF TO TEST FUNCTIONALITY OF READFUNDOFMANDATESFILE
        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithBlankFundsOfMandates.xml")]
        public void ShouldThrowErrorWhenFundsOfMandatesAreNotAvailableInFile()
        {
            //Arrange            
            IFundsProcessor fileProcessor = new FundProcessor();
            string filePath = "FundOfMandatesDataWithBlankFundsOfMandates.xml";


            FluentActions.Invoking(() => fileProcessor.ReadFundOfMandatesFile(filePath)).Should().Throw<Exception>().WithMessage("Unable to Read blank FundOfMandatesFile. Please check the file.");

        }

        //FundOfMandatesDataWithBlankFundsOfMandates.xml


        //TEST IF TO TEST FUNCTIONALITY OF READFUNDOFMANDATESFILE
        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithBadXML.xml")]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void ShouldReturnNullWhileReadingBadFile()
        {
            //Arrange

            IFundsProcessor fileProcessor = new FundProcessor();
            //int counOfMandatesIntestFile = 2;
            string filePath = "FundOfMandatesDataWithBadXML.xml";

            //ACT - JUST USING STANDARD MSTEST ASSERTION FOR THIS METHOD

            var funds = fileProcessor.ReadFundOfMandatesFile(filePath);

            //Assertion using MSTest framework

        }

        //TEST IF TO TEST FUNCTIONALITY OF GetPortfolio
        [TestMethod]
        public void ShouldReturnStaticListOfPortfolios()
        {
            IFundsProcessor fundProcessor = new FundProcessor();
            var portfolio = new PortfolioVM
            {
                Positions = new List<PositionVM> {

                     new PositionVM { Code="NL0000287100", Name="Henekens", Value=12345 },
                     new PositionVM { Code="NL000029332", Name="Optimix", Value=23456 },
                     new PositionVM { Code="NL0000440584", Name="DP Global", Value=34567 },
                     new PositionVM { Code="NL0000440588", Name="Rabobank core", Value=45678 },
                     new PositionVM { Code="inc005", Name="Morgan Stanley", Value=56789 }
                    }
            };

            fundProcessor.GetPortfolio().Should().NotBeNull().And.BeAssignableTo(typeof(PortfolioVM)).And.BeEquivalentTo(portfolio);

        }





        //METHOD TO TEST GETCALCULATEDMANDATES
        [TestMethod]
        public void ShouldAddLiquidityMandateAsAdditionalMandateinPositionVM()
        {
            IFundsProcessor fundsProcessor = new FundProcessor();

            PositionVM inputPosition = new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = null
            };

            PositionVM outputPosition = new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = new List<MandateVM>
                     {
                         new MandateVM { Allocation=.105m, name="Mandate1", Value = 1296},
                         new MandateVM { Allocation=.205m, name="Mandate2", Value=2531 },
                         new MandateVM { Allocation=.305m, name= "Mandate3", Value=3765},
                         new MandateVM { Allocation=.109m, name="Mandate4", Value=1346 },
                          new MandateVM { Allocation=.025m, name="Liquidity", Value=309 }
                     }
            };

            FundOfMandates fundOfMandates = new FundOfMandates
            {
                InstrumentCode = "Pos1",
                InstrumentName = "FundOfMandates1",
                LiquidityAllocation = 2.5m,
                Mandates = new Mandate[]
                     {
                         new Mandate { Allocation=10.5m, MandateId="Mandate1", MandateName = "Mandate1" },
                         new Mandate { Allocation=20.5m, MandateId="Mandate2", MandateName = "Mandate2" },
                         new Mandate { Allocation=30.5m, MandateId="Mandate3", MandateName = "Mandate3" },
                         new Mandate { Allocation=10.9m, MandateId="Mandate4", MandateName = "Mandate4" }
                     }

            };


            var outputPos = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);
            outputPos.Should().NotBeNull().And.BeOfType<PositionVM>().And.BeEquivalentTo(outputPosition);
            outputPos.Mandates.Should().HaveCount(5, "Because Liquidity should get added since we pass Liquidity allocation more than 0");

          

        }



        //METHOD TO TEST GETCALCULATEDMANDATES
        [TestMethod]
        public void ShouldNotAddLiquidityMandateAsAdditionalMandateinPositionVM()
        {
            IFundsProcessor fundsProcessor = new FundProcessor();

            PositionVM inputPosition = new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = null
            };

            PositionVM outputPosition = new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = new List<MandateVM>
                     {
                         new MandateVM { Allocation=.105m, name="Mandate1", Value = 1296},
                         new MandateVM { Allocation=.205m, name="Mandate2", Value=2531 },
                         new MandateVM { Allocation=.305m, name= "Mandate3", Value=3765},
                         new MandateVM { Allocation=.109m, name="Mandate4", Value=1346 }
                     }
            };

            FundOfMandates fundOfMandates = new FundOfMandates
            {
                InstrumentCode = "Pos1",
                InstrumentName = "FundOfMandates1",
                LiquidityAllocation = 0,
                Mandates = new Mandate[]
                     {
                         new Mandate { Allocation=10.5m, MandateId="Mandate1", MandateName = "Mandate1" },
                         new Mandate { Allocation=20.5m, MandateId="Mandate2", MandateName = "Mandate2" },
                         new Mandate { Allocation=30.5m, MandateId="Mandate3", MandateName = "Mandate3" },
                         new Mandate { Allocation=10.9m, MandateId="Mandate4", MandateName = "Mandate4" }
                     }

            };


            fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates).Should().NotBeNull().And.BeOfType<PositionVM>().And.BeEquivalentTo(outputPosition);



        }



        //METHOD TO TEST GETCALCULATEDMANDATES
        [TestMethod]
        public void ShouldNotMakeAnyChangesToPositionVMSinceInstrumentCodeDoNotMatch()
        {
            IFundsProcessor fundsProcessor = new FundProcessor();

            PositionVM inputPosition = new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = null
            };

            PositionVM outputPosition = new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = null
            };

            FundOfMandates fundOfMandates = new FundOfMandates
            {
                InstrumentCode = "Pos2",
                InstrumentName = "FundOfMandates1",
                LiquidityAllocation = 0,
                Mandates = new Mandate[]
                     {
                         new Mandate { Allocation=10.5m, MandateId="Mandate1", MandateName = "Mandate1" },
                         new Mandate { Allocation=20.5m, MandateId="Mandate2", MandateName = "Mandate2" },
                         new Mandate { Allocation=30.5m, MandateId="Mandate3", MandateName = "Mandate3" },
                         new Mandate { Allocation=10.9m, MandateId="Mandate4", MandateName = "Mandate4" }
                     }

            };


            fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates).Should().NotBeNull().And.BeOfType<PositionVM>().And.BeEquivalentTo(outputPosition);



        }


    }
}
