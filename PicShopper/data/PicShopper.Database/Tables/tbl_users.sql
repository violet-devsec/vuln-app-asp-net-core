CREATE TABLE [dbo].[tbl_users]
(
	[u_id] INT NOT NULL PRIMARY KEY, 
    [name] VARCHAR(50) NOT NULL, 
    [password] VARCHAR(50) NOT NULL, 
    [group] INT NOT NULL
)
