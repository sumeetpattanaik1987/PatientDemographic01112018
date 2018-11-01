using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace PatientDemographicService.Repository.GetWay
{

    public  class ServiceAgent : IServiceAgent
    {
        public SqlConnection _connection { get; set; }
        public SqlCommand cmd;

        //---Method for Sql Connection
        public void Connection()
        {
            _connection = new SqlConnection("Data Source=.;Initial Catalog=MyDB;Integrated Security=True;"); //new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDb"].ToString());
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        //---- Method for save personal information to database
        public int SetPatientInfo(string xmlvalue)
        {            
            int i = 0;
            string msg = string.Empty;

            try
            {
                Connection();
                cmd = new SqlCommand("SP_CREATE_PATIENTINFO", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PatientDetails", SqlDbType.Xml).Value = xmlvalue;
                i = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }            
                return i;            
        }

        //---- Method for retrive personal information from database
        public List<string> GetPatientInfo( int? ID)
        {
            var patientmodellist = new List<string>();

            try
            {
                Connection();
                cmd = new SqlCommand("SP_GET_PATIENTINFO", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader _sqlDataReader = cmd.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    patientmodellist.Add(_sqlDataReader.GetString(_sqlDataReader.GetOrdinal("PatientInfo")));
                }

                _connection.Close();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }          

            return patientmodellist;
        }
    }
    
}