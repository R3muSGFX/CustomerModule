ALTER PROCEDURE [dbo].[Customers_Select]
	@ERROR_NUMBER INT OUT,
	@ERROR_MESSAGE NVARCHAR(4000) OUT
AS

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	-- Variables
	DECLARE @TRANCOUNT INT = @@TRANCOUNT
	DECLARE @TRANNAME VARCHAR(38) = NEWID()

	-- Initializations
	SET @ERROR_NUMBER = 0
	SET @ERROR_MESSAGE = N''

	-- Begin transaction block
	BEGIN TRY
		IF @TRANCOUNT = 0
			BEGIN TRANSACTION;
		ELSE
			SAVE TRANSACTION @TRANNAME;
			-- End transaction block
		IF @TRANCOUNT = 0
			COMMIT TRANSACTION;
	
		SELECT 
			C.CUSTOMER_ID,
			C.CUSTOMER_NAME,
			C.CUSTOMER_SURNAME,
			C.CUSTOMER_PHONENUMBER,
			C.CUSTOMER_ADDRESS
		FROM
			[dbo].[Customers] C
	
	END TRY
	BEGIN CATCH
		SET @ERROR_NUMBER = ERROR_NUMBER();
		SET @ERROR_MESSAGE = FORMATMESSAGE('%s:%d - %s', ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE());
		IF @TRANCOUNT = 0
			ROLLBACK TRANSACTION;
		ELSE IF XACT_STATE() != -1
			ROLLBACK TRANSACTION @TRANNAME;
		ELSE
			RAISERROR (@ERROR_MESSAGE, 11, 1);
	END CATCH;