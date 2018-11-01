using System;
using System.Collections.Generic;

namespace PatientDemographicService.Models
{
    // The patient details
    public class PatientDetails
    {
        // The forename
        public string ForeName { get; set; }
        // The surname
        public string Surname { get; set; }
        // Date of birth
        public DateTime DateOfBirth { get; set; }
        // Gender
        public Gender Gender { get; set; }
        // Telephone numbers
        public List<Telephone> Telephones { get; set; }
    }
    
}
