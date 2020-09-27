CREATE PROCEDURE [dbo].[SPExceptionLogger]
(  	 
	@ErrorSource varchar(150) = null ,
	@StackTrace varchar(2000) = null ,
	@InnerException varchar(2000) = null ,
	@strException varchar(2000) = null ,
	@strLogInUser varchar(11)	
)
AS
BEGIN

	 BEGIN TRY   
 SET NOCOUNT ON; 
 BEGIN TRANSACTION

 
	insert into dbo.[tblExceptionLogger](ExceptionSource, 
	ExceptionMessage, 
	ExceptionStackTrace, 
	InnerException,
	ExceptionDateTime, 
	AssociateId) values
	(@ErrorSource,@strException,@StackTrace, @InnerException, GETDATE(),@strLogInUser)

	COMMIT TRANSACTION
SET NOCOUNT OFF;  
END TRY      
BEGIN CATCH  
ROLLBACK TRANSACTION  
DECLARE @ErrorNumber INT    
DECLARE @ErrorMessage Varchar(1000)     
DECLARE @ErrorMessageFinal Varchar(1000)            
SET @ErrorNumber = ERROR_NUMBER()        
SET @ErrorMessage = ERROR_LINE()   
SET @ErrorMessage = @ErrorMessage+ERROR_Message()   
SET @ErrorMessageFinal = 'Deadlock: '+@ErrorMessage     
IF @ErrorNumber = 1205                                    
BEGIN                                    
 RAISERROR(@ErrorMessageFinal,16,1)                
END                                    
END CATCH
END