CREATE TABLE [dbo].[tbl_guestBook]
(
	[c_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [uname] VARCHAR(50) NOT NULL, 
    [comment] VARCHAR(50) NULL, 
    [created_on] DATETIME NULL
)
