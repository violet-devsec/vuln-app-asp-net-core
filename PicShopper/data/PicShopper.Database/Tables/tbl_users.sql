CREATE TABLE [dbo].[tbl_users]
(
	[u_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [uname] VARCHAR(20) NOT NULL, 
    [firstname] VARCHAR(50) NOT NULL, 
    [lastname] VARCHAR(50) NOT NULL, 
    [password] VARCHAR(10) NOT NULL, 
    [dollors] INT NOT NULL, 
    [created_on] DATETIME NULL, 
    [last_login] DATETIME NULL, 
    [group] INT NOT NULL
)