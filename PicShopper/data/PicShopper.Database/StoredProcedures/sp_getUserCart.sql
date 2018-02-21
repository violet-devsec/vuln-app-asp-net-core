CREATE PROCEDURE [dbo].[sp_getUserCart]
	@userId int
AS
	SELECT [item_name], [item_price], [item_count] from dbo.tbl_userCart WHERE [user_id] = @userId
RETURN 0
