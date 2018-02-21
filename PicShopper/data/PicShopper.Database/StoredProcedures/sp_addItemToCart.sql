CREATE PROCEDURE [dbo].[sp_addItemToCart]
	@name VARCHAR(50),
	@price int,
	@count int,
	@userId int
AS
BEGIN
	INSERT INTO [tbl_userCart] ([item_name], [item_price], [item_count], [user_id], [active], [added_on])
	VALUES(@name, @price, @count, @userId, 1, CURRENT_TIMESTAMP)
END
