using PatientDemographicService.Models;
using PatientDemographicService.Repository.GetWay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Xml;
using System.Xml.Serialization;

namespace PatientDemographicService.Controllers
{
    public class PatientController : ApiController
    {
        IServiceAgent objPatientRepository;

        public PatientController()
        {
            objPatientRepository = new ServiceAgent();
        }

        //GetPatientInformation() Method for retrive all patient information
        [HttpGet]
        public HttpResponseMessage GetPatientInformation()
        {
            List<PatientDetails> PatientDetailsList;
            HttpResponseMessage response = null;
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            try
            {
                PatientDetailsList  = new List<PatientDetails>();
                var listofpatientInfo = objPatientRepository.GetPatientInfo(0);

                foreach (var item in listofpatientInfo)
                {
                    PatientDetailsList.Add(CreateXmlToObject(item));
                }

                response = request.CreateResponse<List<PatientDetails>>(HttpStatusCode.OK, PatientDetailsList);
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        //GetPatientInformationById Method for retrive patient information based on ID 
        [HttpGet]
        public HttpResponseMessage GetPatientInformationById(int ID)
        {
            List<PatientDetails> PatientDetailsList;
            HttpResponseMessage response = null;
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            try
            {
                PatientDetailsList = new List<PatientDetails>();
                var listofpatientInfo = objPatientRepository.GetPatientInfo(ID);

                foreach (var item in listofpatientInfo)
                {
                    PatientDetailsList.Add(CreateXmlToObject(item));
                }

                response = request.CreateResponse<List<PatientDetails>>(HttpStatusCode.OK, PatientDetailsList);
            }
            catch (Exception)
            {

                throw;
            }
           
            return response;
        }

        // POST api/values
        // Method For data save in XML format
        [HttpPost]
        public HttpResponseMessage SavePatientInformation(PatientDetails patientInformation)
        {
            string msg = string.Empty;
            HttpResponseMessage response = null;
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            try
            {
                var resultXML = CreateObjectToXML(patientInformation);
                objPatientRepository.SetPatientInfo(resultXML);
                response = request.CreateResponse<PatientDetails>(HttpStatusCode.OK, patientInformation);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return response;
        }

        // Create Method for XMl generation
        private string CreateObjectToXML(Object patientdetails)
        {
            string Msg = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();  //Represents an XML document, 
                                                     // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(patientdetails.GetType());

            // Creates a stream whose backing store is memory. 
            try
            {
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    xmlSerializer.Serialize(xmlStream, patientdetails);
                    xmlStream.Position = 0;
                    //Loads the XML document from the specified string.
                    xmlDoc.Load(xmlStream);
                }
            }
            catch (Exception ex )
            {
               Msg=ex.Message;
            }

            return xmlDoc.InnerXml;
        }

        //Method for Deserialize XML to Object
        private PatientDetails CreateXmlToObject(string XMLString)
        {
            string Msg = string.Empty;
            PatientDetails patientdetails = new PatientDetails();
            XmlSerializer oXmlSerializer = new XmlSerializer(patientdetails.GetType());

            try
            {
                //The StringReader will be the stream holder for the existing XML file 
                patientdetails = (PatientDetails)oXmlSerializer.Deserialize(new StringReader(XMLString));
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
           
            return patientdetails;
        }
    }
}