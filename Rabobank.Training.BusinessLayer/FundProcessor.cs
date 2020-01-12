using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Rabobank.Training.Business.Interfaces;
using Rabobank.Training.ClassLibrary.DomainEntities;
using Rabobank.Training.ViewModels.Entities;

namespace Rabobank.Training.BusinessLayer
{
    /// <summary>
    /// This is the core business class dealing with all the core functionalities including - 
    /// 1. Updating all PositionVM with corresponding mandates
    /// 2. Getting all Portfolios.
    /// 3. Reading FundsOfMandates XML file
    /// </summary>
    public class FundProcessor : IFundsProcessor
    {
        /// <summary>
        /// THIS METHOD IS USED TO UPDATE POSITION VIEWMODEL TO MAP CORRESPONDING MANDATES WITH IT PRESENT UNDER PASSED FUNDOFMANDATE.
        /// METHOD USE POSITION VIEW MODEL AND FUNDOFMANDATE OBJECTS AND RETURN UPDATED POSITIONVM BACK TO CLIENT.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="fundOfmandates"></param>
        /// <returns></returns>
        public PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fundOfmandates)
        {

            if (position.Code == fundOfmandates.InstrumentCode && fundOfmandates.Mandates != null && fundOfmandates.Mandates.Length > 0)
            {
                position.Mandates = new List<MandateVM>();
                position.Mandates.AddRange
                 (
                            fundOfmandates.Mandates.ToList().Select(x => new MandateVM
                            {
                                name = x.MandateName,
                                Value = Math.Round((position.Value * x.Allocation) / 100),
                                Allocation = x.Allocation / 100
                            })
                 );

                if (fundOfmandates.LiquidityAllocation > 0)
                {
                    var newMandate = new MandateVM
                    {
                        name = "Liquidity",                        
                        //NOT VERY CLEAR OF THE LOGIC MENTIONED IN THE WORD DOC SHARED WITH EXERCISE. IT SAYS SUBSTRACT FROM POSITION VALUE BUT THE EXAMPLE IN THE DOC SHOWS THAT ITS STRAIGHT POSITION.VALUE * LIQUIDITY ALLOCATION.
                        //SO COMENTING THE BELOW LINE OF CODE AND KEEPING IT SIMPLE BASED ON EXAMPLE
                                //Value = (position.Value - ((position.Value * fundOfmandates.LiquidityAllocation) / 100)),
                        Value = Math.Round((position.Value * fundOfmandates.LiquidityAllocation) / 100),
                        Allocation = fundOfmandates.LiquidityAllocation / 100
                    };

                    position.Mandates.Add(newMandate);
                }
            }
            else
            {
                //Maybe we can Log something here as needed..
            }

            return position;

        }

        /// <summary>
        /// Method to get static Portfolio object and return back to Client.
        /// </summary>
        /// <returns></returns>
        public PortfolioVM GetPortfolio()
        {
            PortfolioVM portfolio = null;
            try
            {
                portfolio = new PortfolioVM
                {
                    Positions = new List<PositionVM> {

                     new PositionVM { Code="NL0000287100", Name="Henekens", Value=12345 },
                     new PositionVM { Code="NL000029332", Name="Optimix", Value=23456 },
                     new PositionVM { Code="NL0000440584", Name="DP Global", Value=34567 },
                     new PositionVM { Code="NL0000440588", Name="Rabobank core", Value=45678 },
                     new PositionVM { Code="inc005", Name="Morgan Stanley", Value=56789 }
                    }
                };

            }
            catch (Exception e)
            {
                //Log here if any error and throw to client
                throw e;
            }

            return portfolio;
        }

        /// <summary>
        /// Method Read Funds XML file and read its data and set Domain Classes accordingly.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<FundOfMandates> ReadFundOfMandatesFile(string fileName)
        {
            try
            {
                List<FundOfMandates> funds = null;
                StreamReader reader = null;
                FundsOfMandatesData fundsOfMandatesData = null;
                XmlSerializer serealizer = new XmlSerializer(typeof(FundsOfMandatesData));

                using (reader = new StreamReader(fileName))
                {
                    fundsOfMandatesData = (FundsOfMandatesData)serealizer.Deserialize(reader);
                }

                //WE CAN ALSO PERFORM SCHEMA VALIDATION OVER XML AND ITS DATA BUT DUE TO DOWNLOAD RESTRICTIONS , I MANUALLY WROTE ENTIRE SCHEMA WITHOUT TAKING CONSIDERATION ON NAMESPACES.
                //DUE TO CHANGE IN NAMESPACES SCHEMA VALIDATION WILL FAIL AND HENCE NOT PERFORMING IT.
                //HENCE NOT HANDLING SCHEMA VALIDATION HERE.


                if (fundsOfMandatesData == null)
                {
                    //Log error
                    throw new Exception("UnExpected Error. Please check that file contains data correctly.");

                }
                else if (fundsOfMandatesData != null && fundsOfMandatesData.FundsOfMandates.Length == 0)
                {
                    //Log error
                    throw new Exception("Unable to Read blank FundOfMandatesFile. Please check the file.");
                }
                else
                {
                    funds = fundsOfMandatesData.FundsOfMandates.ToList();
                }

                return funds;
            }
            catch (InvalidOperationException inv)
            {
                //Log err
                throw inv;
            }
            catch (Exception ex)
            {
                //Log err
                throw ex;
            }
        }

    }
}
