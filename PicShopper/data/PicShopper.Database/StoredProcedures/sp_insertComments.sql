CREATE PROCEDURE [dbo].[sp_insertComments]
	@name VARCHAR(50),
	@comment VARCHAR(250)
AS
BEGIN
	INSERT INTO [tbl_guestBook] (uname, comment, created_on)
	VALUES(@name, @comment, CURRENT_TIMESTAMP)
END
