CREATE PROCEDURE [dbo].[sp_getPicture]
	@picId int
AS
	SELECT [price], [title], [file_name] FROM [tbl_pictures] where p_id = @picId
RETURN 0
