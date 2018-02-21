CREATE TABLE [dbo].[tbl_userCart]
(
	[c_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [item_name] VARCHAR(50) NOT NULL, 
    [item_price] INT NOT NULL, 
    [item_count] INT NOT NULL, 
    [user_id] INT NOT NULL, 
    [active] INT NOT NULL, 
    [added_on] DATETIME NULL, 
    [deleted_on] DATETIME NULL
)
