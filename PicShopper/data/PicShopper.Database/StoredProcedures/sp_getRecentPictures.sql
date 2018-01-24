CREATE PROCEDURE [dbo].[sp_getRecentPictures]
AS
	SELECT [title], [file_name], [price] FROM [tbl_pictures]
RETURN 0
