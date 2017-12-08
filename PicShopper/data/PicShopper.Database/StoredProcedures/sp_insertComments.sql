CREATE PROCEDURE [dbo].[sp_insertComments]
	@name VARCHAR(50),
	@comment VARCHAR(250)
AS
BEGIN
	INSERT INTO [tbl_guestBook] (name, comment)
	VALUES(@name, @comment)
END
