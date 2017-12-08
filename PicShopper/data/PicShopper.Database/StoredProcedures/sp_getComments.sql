CREATE PROCEDURE [dbo].[sp_getComments]
AS
	SELECT [usr_name], [comment] FROM [GBComments]
RETURN 0
