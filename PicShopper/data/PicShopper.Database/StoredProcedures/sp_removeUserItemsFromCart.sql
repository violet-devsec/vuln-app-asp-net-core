CREATE PROCEDURE [dbo].[sp_removeUserItemsFromCart]
	@userId int 

AS
BEGIN
  SET NOCOUNT ON;

  UPDATE dbo.tbl_userCart
    SET [active] = 0,
		[deleted_on] = CURRENT_TIMESTAMP
    WHERE [user_id] = @userId AND [active] = 1;

  IF @@ROWCOUNT = 0
    RETURN -1;

  RETURN 0;
END