
-- =============================================
-- Author:		Sumeet Pattanaik
-- Create date: 29-10-18
-- Description:	Save Data In XML format to Tbl_PatientInformation
-- =============================================
create PROCEDURE [dbo].[SP_CREATE_PATIENTINFO]
	-- Add the parameters for the stored procedure here
	@PatientDetails xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into dbo.Tbl_PatientInformation(PatientInfo) values(@PatientDetails)
END
