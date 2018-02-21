CREATE TABLE [dbo].[tbl_pictures]
(
	[p_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [title] VARCHAR(20) NOT NULL, 
    [width] INT NULL, 
    [height] INT NULL, 
    [file_name] VARCHAR(50) NOT NULL, 
    [price] INT NOT NULL, 
    [uploaded_on] DATETIME NOT NULL, 
    [uploaded_by] INT NOT NULL
)
