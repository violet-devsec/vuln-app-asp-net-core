CREATE PROCEDURE [dbo].[sp_getComments]
AS
	SELECT [name], [comment] FROM [tbl_guestBook]
RETURN 0
