CREATE PROCEDURE [dbo].[sp_getComments]
AS
	SELECT [uname], [comment] FROM [tbl_guestBook]
RETURN 0
