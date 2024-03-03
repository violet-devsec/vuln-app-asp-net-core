CREATE PROCEDURE [dbo].[sp_getRecentPictures]
AS
	SELECT TOP (3) [p_id], [title], [file_name] FROM [tbl_pictures]  ORDER BY p_id DESC
RETURN 0
