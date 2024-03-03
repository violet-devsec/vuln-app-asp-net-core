CREATE PROCEDURE [dbo].[sp_doAdminLogin]
	@uname varchar(20),
	@pass varchar(20)
AS
	SELECT * FROM [tbl_users] WHERE [uname] = @uname AND [password] = @pass AND [group] = '1';
RETURN 0
