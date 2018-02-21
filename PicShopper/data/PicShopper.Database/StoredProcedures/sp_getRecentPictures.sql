CREATE PROCEDURE [dbo].[sp_getRecentPictures]
AS
	SELECT [p_id], [title], [file_name] FROM [tbl_pictures]
RETURN 0
