using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientDemographicService.Repository.GetWay
{
    public interface IServiceAgent
    {
        int SetPatientInfo(string xmlvalue);
        List<string> GetPatientInfo(int? ID);
    }
}