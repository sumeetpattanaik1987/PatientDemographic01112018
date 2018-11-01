Steps:

1. Host application PatientDemographic.RestAPI in IIS as PatientDemographicService.
2. If you face security issue then you have to give permission on Database side for your Apppool User.
3. Change connection string  in serviceagent.cs page in PatientDemographic.Reposiitory project according to your database set up .
4. Execute all database scripts from SQLDBSCRIPT folder.
5. Make start up project PatientDemographic.UI and run it.