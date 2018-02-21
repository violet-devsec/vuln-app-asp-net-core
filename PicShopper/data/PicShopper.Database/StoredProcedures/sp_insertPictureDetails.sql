CREATE PROCEDURE [dbo].[sp_insertPictureDetails]
	@title VARCHAR(50),
	@width int,
	@height int,
	@file VARCHAR(50),
	@price int,
	@user int
AS
BEGIN
	INSERT INTO [tbl_pictures] (title, width, height, file_name, price, uploaded_on, uploaded_by)
	VALUES (@title, 128, 128, @file, @price, CURRENT_TIMESTAMP, @user)
END
