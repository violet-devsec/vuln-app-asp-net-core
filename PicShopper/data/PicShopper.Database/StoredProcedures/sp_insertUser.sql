CREATE PROCEDURE [dbo].[sp_insertUser]
	@uName varchar(10),
	@fName varchar(20),
	@lName varchar(20),
	@dollors int
AS
BEGIN
	INSERT INTO [tbl_users] ([uname], [firstname], [lastname], [password], [dollors], [created_on], [group])
	VALUES(@uName, @fName, @lName, 'user123', @dollors, CURRENT_TIMESTAMP, 2)
END
