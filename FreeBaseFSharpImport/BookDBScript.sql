USE [Books]
GO
/****** Object:  UserDefinedTableType [dbo].[tt_BookAuthorImport]    Script Date: 4/23/2015 12:37:55 AM ******/
CREATE TYPE [dbo].[tt_BookAuthorImport] AS TABLE(
	[Title] [varchar](500) NULL,
	[Author] [varchar](500) NULL,
	[Genres] [varchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tt_BookNames]    Script Date: 4/23/2015 12:37:55 AM ******/
CREATE TYPE [dbo].[tt_BookNames] AS TABLE(
	[title] [varchar](500) NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 4/23/2015 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE CHARINDEX('|', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX('|', @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 4/23/2015 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [varchar](500) NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookGenres]    Script Date: 4/23/2015 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookGenres](
	[BookID] [int] NOT NULL,
	[GenreID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Books]    Script Date: 4/23/2015 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](500) NOT NULL,
	[AuthorID] [int] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 4/23/2015 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Genres](
	[GenreID] [int] IDENTITY(1,1) NOT NULL,
	[GenreName] [varchar](500) NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Books_BulkInsert]    Script Date: 4/23/2015 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alex Barry
-- Create date: 04/21/2015
-- Description:	Prototype for batch insert so I can test calling a stored procedure from F#.
-- =============================================
CREATE PROCEDURE [dbo].[Books_BulkInsert] 
	-- Add the parameters for the stored procedure here
	@bookInfo tt_BookAuthorImport READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert Authors
	Merge Authors as tar
		Using (Select Distinct Author From @bookInfo) as sor
		On sor.Author = tar.AuthorName
	When Not Matched By Target 
		And sor.Author <> ''
		Then
		Insert (AuthorName)
		Values (sor.Author);

	-- Insert Books
	Insert Into Books(Title, AuthorID)
	Select b.Title, a.AuthorID
	From @bookInfo b
	Inner Join Authors a on b.Author = a.AuthorName;

	-- Insert Genres
	Declare @BooksAndGenres table (Title varchar(500), Genre varchar(500));
	Insert Into @BooksAndGenres (Genre, Title)
	Select ss.Name as Genre, b.Title
	From @bookInfo b
	Cross Apply dbo.splitstring(b.Genres) ss
	Where ss.Name <> ''

	Merge Genres as tar
		Using (Select Distinct Genre From @BooksAndGenres) as sor
		On sor.Genre = tar.GenreName
	When Not Matched By Target Then
		Insert (GenreName)
		Values (sor.Genre);

	-- Book / Genre association
	Insert Into BookGenres (BookID, GenreID)
	Select BookID, GenreID
	From @BooksAndGenres bg
	Inner Join Books b on bg.Title = b.Title
	Inner Join Genres g on bg.Genre = g.GenreName
			


END

GO
