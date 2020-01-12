using System.Collections.Generic;
using ConsoleApp2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace UnitTestProject1
{

    /// <summary>
    /// Need to see how to use Fluent Assertions in this test project
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem("TestData//XMLFile1.xml")]
        public void ShouldReadValidXMLFileCorrectly()
        {
            //Arrange

            IFileProcessing fileProcessor = new FileProcessor();
            int counOfMandatesIntestFile = 5;
            string filePath = "XMLFile1.xml";

            //Act

            var funds = fileProcessor.ReadFundOfMandatesFile(filePath);


            //Assert

            Assert.IsNotNull(funds);
            Assert.AreEqual(funds.Count, counOfMandatesIntestFile); //Assuming we already know count of mandates inside the test file.
            Assert.IsInstanceOfType(funds, typeof(List<FundOfMandates>));

            //Fluent Assertions in use
            funds.Should().HaveCount(counOfMandatesIntestFile, "we have passed 5 mandates to XML");
            funds.Should().NotBeNull();
            funds.Should().BeAssignableTo(typeof(List<FundOfMandates>));

        }


        [TestMethod]
        [DeploymentItem("TestData//XMLFile1BadFile.xml")]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void ShouldReturnNullWhileReadingBadFile()
        {
            //Arrange

            IFileProcessing fileProcessor = new FileProcessor();
            //int counOfMandatesIntestFile = 2;
            string filePath = "XMLFile1BadFile.xml";

            //Act

            var funds = fileProcessor.ReadFundOfMandatesFile(filePath);

        }


        [TestMethod]
        public void ShouldMapMandatesFromFundOfMandatesToMandateVM()
        {

            //GetCalculatedMandates
            IFileProcessing fileProcessing = new FileProcessor();

            PositionVM inputPosition = new PositionVM
            {
                Code = "",
                Name = "",
                Value = 0,
                Mandates = new List<MandateVM>()
                    {
                        new MandateVM { Value=0, name="", Allocation= 0 },
                        new MandateVM { Value=0, name="", Allocation= 0 },
                        new MandateVM { Value=0, name="", Allocation= 0 },
                        new MandateVM { Value=0, name="", Allocation= 0 },

                    }
            };

            PositionVM outputPosition = new PositionVM
            {
                Code = "",
                Name = "",
                Value = 0,
                Mandates = new List<MandateVM>()
                    {
                        new MandateVM { Value=0, name="", Allocation= 0 },
                        new MandateVM { Value=0, name="", Allocation= 0 },
                        new MandateVM { Value=0, name="", Allocation= 0 },
                        new MandateVM { Value=0, name="", Allocation= 0 },

                    }
            };

            List<Mandate> mandates = new List<Mandate>
            {
                new Mandate { Allocation = 0, MandateId=1, MandateName="" },
                new Mandate { Allocation = 0, MandateId=1, MandateName="" },
                new Mandate { Allocation = 0, MandateId=1, MandateName="" },
                new Mandate { Allocation = 0, MandateId=1, MandateName="" }
            };

            FundOfMandates fund = new FundOfMandates
            {
                InstrumentCode = "",
                InstrumentName = "",
                LiqAllocation = 0,
                Mandates = new Mandates { MandateList = mandates.ToArray() }

            };

            //you can add code yourself from here. ie use correct objects -- call the actual method---use Fuuent assertions to check that output from method actually
            //returns correct PositionVM with correct set of mandates..


        }

        //similarly  we can dd more tests.. one for COunt of Mandates = 0 Exception.
    }
}
