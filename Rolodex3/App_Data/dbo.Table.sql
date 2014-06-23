CREATE TABLE [dbo].[Person]
(
	[Identity] INT NOT NULL PRIMARY KEY, 
    [firstName] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NCHAR(2) NULL, 
    [Zip] NCHAR(5) NULL
)
