using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientDemographicService.Controllers;
using PatientDemographicService.ModelEnum;
using PatientDemographicService.Models;

namespace WebApiWithXMLUnitTest
{
    [TestClass]
    public class PatientControllerTest
    {
        // Test that returns success for patient information save
        [TestMethod]
        public void SavePatientInformation_Success()
        {
            // Arrange
            PatientDetails patientDetails = new PatientDetails()
            {
                ForeName = "Sumeet",
                Surname = "Pattnaik",
                DateOfBirth = DateTime.Now,
                Gender = Gender.Male,
                Telephones = new List<Telephone>()
                {
                    new Telephone()
                    {
                        PhoneNumber="1234",
                        PhoneType=PhoneType.Home
                    },
                    new Telephone()
                    {
                        PhoneNumber="4567",
                        PhoneType=PhoneType.Work
                    },
                    new Telephone()
                    {
                        PhoneNumber="1432",
                        PhoneType=PhoneType.Mobile
                    }
                }
            };
            
            // Mock
            var SQlMock = new Mock<PatientDemographicService.Repository.GetWay.IServiceAgent>();
            SQlMock.Setup(x => x.SetPatientInfo(It.IsAny<string>())).
                Returns(-1);
                    
            PatientController patientController = new PatientController();
            // Act
            HttpResponseMessage message = patientController.SavePatientInformation(patientDetails);

            // Assert
            Assert.IsNotNull(message, "Result was not expected");
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode, "Result was not expected");
        }

        // Test that returns success for patient information retrive
        [TestMethod]
        public void GetPatientInformation()
        {
            // Arrange
            StringBuilder xmlstring = new StringBuilder("<?xml version=\"1.0\"?><PatientDetails xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><ForeName>Summet</ForeName><Surname>Pattnaik</Surname><DateOfBirth>2018-05-05T00:00:00</DateOfBirth><Gender>Male</Gender><Telephones><Telephone><PhoneType>Home</PhoneType><PhoneNumber>9861240765</PhoneNumber></Telephone><Telephone><PhoneType>Work</PhoneType><PhoneNumber>7865434353</PhoneNumber></Telephone><Telephone><PhoneType>Mobile</PhoneType><PhoneNumber>6549875321</PhoneNumber></Telephone></Telephones></PatientDetails>");
            List<string> patientDetails = new List<string>()
            {
                xmlstring.ToString()
            };

            // Mock
            var sqlmock = new Mock<PatientDemographicService.Repository.GetWay.IServiceAgent>();
            sqlmock.Setup(x => x.GetPatientInfo( It.IsAny<int>())).
                Returns(patientDetails);

            PatientController patientController = new PatientController();

            // Act
            HttpResponseMessage message = patientController.GetPatientInformation();

            // Assert
            Assert.IsNotNull(message, "Result was not expected");
        }
    }
}
