CREATE TABLE [dbo].[People] (
    [ID]            INT   IDENTITY   NOT NULL,
    [FirstName]     NVARCHAR (50) NOT NULL,
	[LastName]      NVARCHAR (50) NOT NULL,
    [Gender]        INT NOT NULL,
	[Birthdate]     DATETIME2 (7) NOT NULL,
    [ImageFilename] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([ID] ASC)
);

