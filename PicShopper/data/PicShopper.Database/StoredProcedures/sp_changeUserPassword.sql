CREATE PROCEDURE [dbo].[sp_changeUserPassword]
	@uId       VARCHAR(3),
    @newPassword VARCHAR(10)
AS
BEGIN
  SET NOCOUNT ON;

  UPDATE dbo.tbl_users
    SET password = @newPassword
    WHERE u_id = @uId;

  IF @@ROWCOUNT = 0
    RETURN -1;

  RETURN 0;
END

