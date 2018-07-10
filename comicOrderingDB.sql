/* Check if database already exists and delete it if it does exist */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'comicOrderingDB')
BEGIN
	DROP DATABASE comicOrderingDB
	print '' print '*** Dropping Database comicOrderingDB ***'
END
GO

print '' print '*** Creating Database comicOrderingDB ***'
GO
CREATE DATABASE comicOrderingDB
GO

print '' print '*** Using Database comicOrderingDB ***'
GO
USE [comicOrderingDB]
GO

print '' print '*** Creating Customer Table ***'
GO
/* ********** Object: Table [dbo].[Customer]  */
CREATE TABLE [dbo].[Customer](
	[CustomerID] 	[int] IDENTITY (1000,1)	NOT NULL,
	[FirstName]		[varchar](50)			NOT NULL,
	[LastName]		[varchar](50)			NOT NULL,
	[Zip]			[varchar](5)			NOT NULL,
	[PhoneNumber]	[varchar](16)			NOT NULL,
	[Email]			[varchar](50)			NOT NULL,
	[Active]		[bit]					NOT NULL DEFAULT 1,

	CONSTRAINT [PK_CustomerID] PRIMARY KEY([CustomerID] ASC),
	CONSTRAINT [AK_Email] UNIQUE ([Email] ASC)
)
GO

print '' print '*** Creating User Table ***'
GO
/* ********** Object: Table [dbo].[User]  */
CREATE TABLE [dbo].[User](
	[UserID] 		[int] IDENTITY (1000,1)		NOT NULL,
	[Email]			[varchar](50)				NOT NULL,
	[Active]		[bit]						NOT NULL DEFAULT 1,

	CONSTRAINT [PK_UserID] PRIMARY KEY([UserID] ASC),
	CONSTRAINT [AK_User_Email] UNIQUE ([Email] ASC)
)
GO

print '' print '*** Creating OrderForm Table ***'
GO
/* ********** Object: Table [dbo].[OrderForm]  */
CREATE TABLE [dbo].[OrderForm](
	[OrderFormID]		[int] IDENTITY (100000,1) 	NOT NULL,
	/* [OrderFormLineID]	[int]						NOT NULL, */
	[CustomerID]		[int]						NOT NULL,
	[Date]				[date]						NOT NULL,
	[Completed]			[bit]						NOT NULL  DEFAULT 0,
	
	CONSTRAINT [PK_OrderFormID] PRIMARY KEY([OrderFormID] ASC)
)
GO

print '' print '*** Creating OrderFormLine Table ***'
GO
/* ********** Object: Table [dbo].[OrderFormLine]  */
CREATE TABLE [dbo].[OrderFormLine](
	[OrderFormLineID]	[int] IDENTITY (100000,1) 	NOT NULL,
	[OrderFormID]		[int]						NOT NULL,
	[ComicID]			[int]						NOT NULL,
	[Quantity]			[int]						NOT NULL,
	[Price]				[decimal]					NOT NULL,
	
	CONSTRAINT [PK_OrderFormLineID] PRIMARY KEY([OrderFormLineID] ASC)
)
GO 

print '' print '*** Creating PullList Table ***'
GO
/* ********** Object: Table [dbo].[PullList] */
CREATE TABLE [dbo].[PullList](
	[CustomerID]	[int]					NOT NULL,
	[ComicID]		[int]					NOT NULL,
	CONSTRAINT [PK_CustomerIDComicID] PRIMARY KEY([CustomerID] ASC, [ComicID] ASC)
)
GO

print '' print '*** Creating Employee Table ***'
GO
/* ********** Object: Table [dbo].[Employee]  */
CREATE TABLE [dbo].[Employee](
	[EmployeeID] 	[int] IDENTITY (1000,1)	NOT NULL,
	[FirstName]		[varchar](50)			NOT NULL,
	[LastName]		[varchar](50)			NOT NULL,
	[PhoneNumber]	[varchar](10)			NOT NULL,
	[Zip]			[varchar](5)			NOT NULL,
	[Email]			[varchar](50)			NOT NULL,
	[Active]		[bit]					NOT NULL DEFAULT 1,

	CONSTRAINT [PK_EmployeeID] PRIMARY KEY([EmployeeID] ASC)
)
GO

print '' print '*** Inserting User Test Records ***'
GO
INSERT INTO [dbo].[User]
		([Email], [Active])
		VALUES
			('bob.tester@gmail.com', 1),
			('j.testeroni@hotmail.com', 1),
			('bambamsam@juno.com', 1),
			('guy.simpson@comicbooks.com', 1)
GO

print '' print '*** Creating Roles Table'
GO
CREATE TABLE [dbo].[Role](
	[RoleID]				[varchar](50)		NOT NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** Insert Roles Table Records'
GO
INSERT INTO [dbo].[Role]
		([RoleID])
	VALUES
		('Employee'),
		('Customer')
GO

print '' print '*** Creating User Role Table'
GO
CREATE TABLE [dbo].[UserRole](
	[UserID]				[int]				NOT NULL,
	[RoleID]				[varchar](50)		NOT NULL,
	CONSTRAINT [pk_UserID_RoleID] PRIMARY KEY([UserID] ASC, [RoleID] ASC)
)
GO

print '' print '*** Inserting Sample UserRole Records'
INSERT INTO [dbo].[UserRole]
		([UserId], [RoleID])
	VALUES
		(1000, 'Customer'),
		(1001, 'Customer'),
		(1002, 'Customer'),
		(1003, 'Employee')
GO

print '' print '*** Creating Comic Table ***'
GO
/* ********** Object: Table [dbo].[Comic] */
CREATE TABLE [dbo].[Comic](
	[ComicID]		[int] IDENTITY (100000,1)	NOT NULL,
	[ComicType]		[varchar](25)						NOT NULL,
	[DistributorID]	[int]						NOT NULL,
	[Title]			[varchar](250)				NOT NULL,
	[Issue]			[int]						,
	[Authors]		[varchar](250)				NOT NULL,
	[Publisher]		[varchar](250)				NOT NULL,
	[Rating]		[varchar](25)				,
	[Description]	[varchar](750)				,
	[Quantity]		[int]						NOT NULL,
	[Price]			[decimal](10, 2)			NOT NULL,
	[Status]		[bit]						NOT NULL	DEFAULT 1,
	
	CONSTRAINT [PK_ComicID] PRIMARY KEY([ComicID] ASC)
)
GO

print '' print '*** Inserting Comic Test Records ***'
GO
INSERT INTO [dbo].[Comic]
		([ComicType], [DistributorID], [Title], [Issue], [Authors], [Publisher], [Rating], [Description], [Quantity], [Price])
		VALUES
		('Individual book', 1000, 'Saga', 37, 'Fiona Staples, Brian Vaughan', 'Image', 'Mature', 'Sci-fi fantasy space action drama', 5, 2.99),
		('Individual book', 1000, 'Star Wars', 20, 'Brian Wood, Carlos D''Anda, Gabe Eltaeb', 'Dark Horse Comics', 'Teen', 'Classic Star Wars characters in the Rebellion done up by Brian Wood.', 1, 2.99),
		('Individual book', 1000, 'The Unwritten Apocaplypse', 2, 'Mike Carey, Peter Gross', 'Vertigo', 'Mature', 'Fantasy story riffing on Harry Potter', 3, 3.99),
		('Individual book', 1000, 'Saga', 36, 'Fiona Staples, Brian Vaughan', 'Image', 'Mature', 'Sci-fi fantasy space action drama', 4, 2.99),
		('Individual book', 1000, 'The Massive', 18, 'Brian Wood, Garry Brown, Jordie Bellaire', 'Dark Horse Comics', 'Not rated', 'Post-apololyptic action adventure story', 4, 3.5),
		('Bound trade', 1000, 'Prophet: Empire', 3, 'Brandon Graham, Simon Roy, Giannis Milonogiannis', 'Image', 'Teen', 'Collected issues 34-38. Thousands of years into the distant future, the once-great Earth Empire has returned.', 2, 11.99),
		('Individual book', 1000, 'Saga', 35, 'Fiona Staples, Brian Vaughan', 'Image', 'Mature', 'Sci-fi fantasy space action drama', 4, 2.99),
		('Graphic novel', 1000, 'The Left Bank Gang', 0, 'Jason', 'Fantagraphics Books', 'Not rated', 'Hemingway, Fitzgerald, Pound and Joyce reimagined as struggling cartoonists.', 1, 15.99),
		('Bound trade', 1000, 'Rasl: The Drift', 1, 'Jeff Smith', 'Cartoon Books', 'Mature', 'Sci-fi time travel caper series from the creator of Bone.', 1, 8.99),
		('Individual book', 1000, 'The Sandman Overture', 2, 'Neil Gaiman, J.H. Williams III', 'Vertigo', 'Mature', 'New story by Gaiman for the epic Sandman series.', 2, 3.99),
		('Bound trade', 1000, 'Star Wars Invasion: Refugees', 1, 'Tom Taylor, Colin Wilson', 'Dark Horse Comics', 'Teen', 'Twenty-five years after the Battle of Yavin, the New Republic is faces new challenges.', 3, 11.99),
		('Individual book', 1000, 'Saga', 34, 'Fiona Staples, Brian Vaughan', 'Image', 'Mature', 'Sci-fi fantasy space action drama', 2, 2.99),
		('Graphic Novel', 1000, 'Caricature', 0, 'Daniel Clowes', 'Fantagraphics Books', 'Not rated', 'Nine collected short stories by the famed underground comics writer/artist.', 4, 16.95),
		('Manga', 1000, 'Lone Wolf and Cub: The Black Wind', 5, 'Kazuo Koike, Goseki Kojima', 'Dark Horse Comics', 'Teen', 'Separated from his son while battling Yagyu assassins, Itto Ogami’s mission of retribution heats to a boil as he cuts through any obstacle to find his missing child.', 1, 7.99)
GO

print '' print '*** Creating ComicType Table ***'
GO
/* ********** Object: Table [dbo].[ComicType] */
CREATE TABLE [dbo].[ComicType](
	[ComicType]			[varchar](25)				NOT NULL,
	[TypeDescription]	[varchar](250)				NOT NULL,
	CONSTRAINT [PK_ComicType] PRIMARY KEY([ComicType] ASC)
)
GO

print '' print '*** Inserting ComicType Records ***'
GO
INSERT INTO [dbo].[ComicType]
		([ComicType], [TypeDescription])
	VALUES
		('Individual book', 'Regularly issued individual comic book.'),
		('Bound trade', 'A collection of individual comic books bound as one volume'),
		('Graphic novel', 'A novel in comic format'),
		('Manga', 'A style of Japanese comic books, typically read right to left.')
GO

print '' print '*** Creating Distributor Table ***'
GO
/* ********** Object: Table [dbo].[Distributor] */
CREATE TABLE [dbo].[Distributor](
	[DistributorID]	[int] IDENTITY (1000,1)		NOT NULL,
	[Name]			[varchar](50)				NOT NULL,
	[Address]		[varchar](250)				NOT NULL,
	[Address2]		[varchar](250)				,
	[City]			[varchar](250)				NOT NULL,
	[State]			[varchar](2)				NOT NULL,
	[Zip]			[varchar](5)				NOT NULL,
	[PhoneNumber]	[varchar](15)				NOT NULL,
	[Email]			[varchar](50)				NOT NULL,
	
	CONSTRAINT [PK_DistributorID] PRIMARY KEY([DistributorID] ASC)
)
GO

print '' print '*** Inserting Distributor Records ***'
GO
INSERT INTO [dbo].[Distributor]
		([Name], [Address], [Address2], [City], [State], [Zip], [PhoneNumber], [Email])
	VALUES
		('Diamond Comics', '10150 York Road', 'Suite 300', 'Hunt Valley', 'MD', '21030', '443-318-8001', 'order@diamondcomics.com')
GO

print '' print '*** Creating PullList ComicID foreign key ***'
GO
ALTER TABLE [dbo].[PullList]  WITH NOCHECK 
	ADD CONSTRAINT [FK_ComicID] FOREIGN KEY([ComicID])
	REFERENCES [dbo].[Comic] ([ComicID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating PullList CustomerID foreign key ***'
GO
ALTER TABLE [dbo].[PullList]  WITH NOCHECK 
	ADD CONSTRAINT [FK_CustomerID] FOREIGN KEY([CustomerID])
	REFERENCES [dbo].[Customer] ([CustomerID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating OrderFormLine OrderFormID foreign key ***'
GO
ALTER TABLE [dbo].[OrderFormLine]  WITH NOCHECK 
	ADD CONSTRAINT [FK_OrderFormID] FOREIGN KEY([OrderFormID])
	REFERENCES [dbo].[OrderForm] ([OrderFormID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Comic ComicTypeID foreign key ***'
GO
ALTER TABLE [dbo].[Comic]  WITH NOCHECK 
	ADD CONSTRAINT [FK_ComicType] FOREIGN KEY([ComicType])
	REFERENCES [dbo].[ComicType] ([ComicType])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Comic DistributorID foreign key ***'
GO
ALTER TABLE [dbo].[Comic]  WITH NOCHECK 
	ADD CONSTRAINT [FK_DistributorID] FOREIGN KEY([DistributorID])
	REFERENCES [dbo].[Distributor] ([DistributorID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating sp_retrieve_user_by_email_login ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_user_by_email_login]
	(
	@Email			varchar(50)
	)
AS
	BEGIN
		SELECT UserID, Email, Active
		FROM [dbo].[User]
		WHERE [User].[Email] = @Email
	END
GO

print '' print '*** Creating sp_retrieve_customer_by_email_login ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_email_login]
	(
	@Email			varchar(50)
	)
AS
	BEGIN
		SELECT CustomerID, FirstName, LastName, PhoneNumber, Zip, Email, Active
		FROM Customer
		WHERE [Customer].[Email] = @Email
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_email_login ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_email]
	(
	@Email			varchar(50)
	)
AS
	BEGIN
		SELECT EmployeeID, FirstName, LastName, Email
		FROM Employee
		WHERE [Employee].[Email] = @Email
	END
GO

print '' print '*** Creating sp_retrieve_customer_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_id]
	(
	@CustomerID			[int]
	)
AS
	BEGIN
		SELECT CustomerID, FirstName, LastName, PhoneNumber, Zip, Email, Active
		FROM Customer
		WHERE [Customer].[CustomerID] = @CustomerID
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_id]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		SELECT EmployeeID, FirstName, LastName, PhoneNumber, Zip, Email, Active
		FROM Employee
		WHERE [Employee].[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_retrieve_comic_by_status ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_comic_by_status]
	(
	@Status			bit
	)
AS
	BEGIN
		SELECT ComicID, ComicType, DistributorID, Title, Issue, Authors, Publisher,
			Rating, Description, Quantity, Price, Status
		FROM Comic
		WHERE Status = 1
	END
GO

print '' print '*** Creating sp_retrieve_comic_by_title ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_comic_by_title]
	(
	@Title			varchar(250)
	)
AS
	BEGIN
		SELECT ComicID, ComicType, DistributorID, Title, Issue, Authors, Publisher,
			Rating, Description, Quantity, Price, Status
		FROM Comic
		WHERE [Comic].[Title] LIKE '%' + @Title + '%' 
	END
GO

print '' print '*** Creating sp_retrieve_comic_by_authors ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_comic_by_authors]
	(
	@Authors		varchar(250)
	)
AS
	BEGIN
		SELECT ComicID, ComicType, DistributorID, Title, Issue, Authors, Publisher,
			Rating, Description, Quantity, Price, Status
		FROM Comic
		WHERE [Comic].[Authors] LIKE '%' + @Authors + '%'
	END
GO

print '' print '*** Creating sp_retrieve_comic_by_publisher ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_comic_by_publisher]
	(
	@Publisher		varchar(250)
	)
AS
	BEGIN
		SELECT ComicID, ComicType, DistributorID, Title, Issue, Authors, Publisher,
			Rating, Description, Quantity, Price, Status
		FROM Comic
		WHERE [Comic].[Publisher] = @Publisher
	END
GO

print '' print '*** Creating sp_retrieve_customer_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_active]
	(
	@Active			bit
	)
AS
	BEGIN
		SELECT CustomerID, FirstName, LastName, Zip,
			PhoneNumber, Email, Active
		FROM Customer
		WHERE Active = 1
	END
GO

print '' print '*** Creating sp_retrieve_customer_by_inactive ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_inactive]
	(
	@Active			bit
	)
AS
	BEGIN
		SELECT CustomerID, FirstName, LastName, Zip,
			PhoneNumber, Email, Active
		FROM Customer
		WHERE Active = 0
	END
GO

print '' print '*** Creating sp_retrieve_user_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_user_roles]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [Role].RoleID
		FROM UserRole, Role
		WHERE [UserRole].[UserID] = @UserID
		AND [Role].[RoleID] = [Role].[RoleID]
	END
GO

print '' print '*** Creating sp_update_customer_email'
GO
CREATE PROCEDURE [dbo].[sp_update_customer_email]
	(
	@CustomerID		int,
	@Email			varchar(50)
	)
AS
	BEGIN
		UPDATE Customer
			SET Email = @Email
			WHERE CustomerID = @CustomerID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_customer_record'
GO
CREATE PROCEDURE [dbo].[sp_update_customer_record]
	(
		@CustomerID				[int],
		@OldFirstName			[varchar](50),
		@OldLastName			[varchar](50),
		@OldPhoneNumber			[varchar](10),
		@OldZip					[varchar](5),
		@OldEmail				[varchar](50),
		@OldActive				[bit],
		
		@NewFirstName			[varchar](50),
		@NewLastName			[varchar](50),
		@NewPhoneNumber			[varchar](10),
		@NewZip					[varchar](5),
		@NewEmail				[varchar](50),
		@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE Customer
			SET FirstName = @NewFirstName,
				LastName = @NewLastName,
				PhoneNumber = @NewPhoneNumber,
				Zip = @NewZip,
				Email = @NewEmail,
				Active = @NewActive
			WHERE 	FirstName = @OldFirstName
				AND	LastName = @OldLastName
				AND	PhoneNumber = @OldPhoneNumber
				AND	Zip = @OldZip
				AND	Email = @OldEmail
				AND	Active = @OldActive
				AND CustomerID = @CustomerID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_employee_record'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_record]
	(
		@EmployeeID				[int],
		@OldFirstName			[varchar](50),
		@OldLastName			[varchar](50),
		@OldPhoneNumber			[varchar](10),
		@OldZip					[varchar](5),
		@OldEmail				[varchar](50),
		@OldActive				[bit],
		
		@NewFirstName			[varchar](50),
		@NewLastName			[varchar](50),
		@NewPhoneNumber			[varchar](10),
		@NewZip					[varchar](5),
		@NewEmail				[varchar](50),
		@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE Employee
			SET FirstName = @NewFirstName,
				LastName = @NewLastName,
				PhoneNumber = @NewPhoneNumber,
				Zip = @NewZip,
				Email = @NewEmail,
				Active = @NewActive
			WHERE 	FirstName = @OldFirstName
				AND	LastName = @OldLastName
				AND	PhoneNumber = @OldPhoneNumber
				AND	Zip = @OldZip
				AND	Email = @OldEmail
				AND	Active = @OldActive
				AND EmployeeID = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_insert_comic ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_comic]
	(
		@Title				[varchar](250),
		@Issue				[int],
		@Authors			[varchar](250),
		@Publisher			[varchar](250),
		@ComicType			[varchar](25),
		@DistributorID		[int],
		@Rating				[varchar](25),
		@Description		[varchar](750),
		@Quantity			[int],
		@Price				[decimal](10, 2),
		@Status				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Comic]
			([Title], [Issue], [Authors], [Publisher], [ComicType], [DistributorID], 
				[Rating], [Description], [Quantity], [Price], [Status])
		VALUES
			(@Title, @Issue, @Authors, @Publisher, @ComicType, @DistributorID, 
				@Rating, @Description, @Quantity, @Price, @Status)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_comic_record'
GO
CREATE PROCEDURE [dbo].[sp_update_comic_record]
	(
		@ComicID				[int],
		@OldTitle				[varchar](250),
		@OldIssue				[int],
		@OldAuthors				[varchar](250),
		@OldPublisher			[varchar](250),
		@OldComicType			[varchar](25),
		@OldDistributorID		[int],
		@OldRating				[varchar](25),
		@OldDescription			[varchar](750),
		@OldQuantity			[int],
		@OldPrice				[decimal](10, 2),
		@OldStatus				[bit],
		
		@NewTitle				[varchar](250),
		@NewIssue				[int],
		@NewAuthors				[varchar](250),
		@NewPublisher			[varchar](250),
		@NewComicType			[varchar](25),
		@NewDistributorID		[int],
		@NewRating				[varchar](25),
		@NewDescription			[varchar](750),
		@NewQuantity			[int],
		@NewPrice				[decimal](10, 2),
		@NewStatus				[bit]
	)
AS
	BEGIN
		UPDATE Comic
			SET Title = @NewTitle,
				Issue = @NewIssue,
				Authors = @NewAuthors,
				Publisher = @NewPublisher,
				ComicType = @NewComicType,
				DistributorID = @NewDistributorID,
				Rating = @NewRating,
				Description = @NewDescription,
				Quantity = @NewQuantity,
				Price = @NewPrice,
				Status = @NewStatus
			WHERE 	Title = @OldTitle
				AND Issue = @OldIssue
				AND Authors = @OldAuthors
				AND Publisher = @OldPublisher
				AND ComicType = @OldComicType
				AND DistributorID = @OldDistributorID
				AND Rating = @OldRating
				AND Description = @OldDescription
				AND Quantity = @OldQuantity
				AND Price = @OldPrice
				AND Status = @OldStatus
				AND ComicID = @ComicID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_new_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_create_new_customer]
	(
		@FirstName			[varchar](50),
		@LastName			[varchar](50),
		@PhoneNumber		[varchar](10),
		@Zip				[varchar](5),
		@Email				[varchar](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Customer]
			([FirstName], [LastName], [PhoneNumber], [Zip], [Email])
		VALUES
			(@FirstName, @LastName, @PhoneNumber, @Zip, @Email)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_new_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_create_new_employee]
	(
		@FirstName			[varchar](50),
		@LastName			[varchar](50),
		@PhoneNumber		[varchar](10),
		@Zip				[varchar](5),
		@Email				[varchar](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
			([FirstName], [LastName], [PhoneNumber], [Zip], [Email])
		VALUES
			(@FirstName, @LastName, @PhoneNumber, @Zip, @Email)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_add_comic_to_pullist ***'
GO
CREATE PROCEDURE [dbo].[sp_add_comic_to_pullist]
	(
		@CustomerID			[int],
		@ComicID			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[PullList]
			([CustomerID], [ComicID])
		VALUES
			(@CustomerID, @ComicID)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_comic_by_customer_pull ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_comic_by_customer_pull]
	(
		@CustomerID			[int]
	)
AS
	BEGIN
		SELECT Comic.ComicID, Title, Issue, Authors, Publisher, Rating, Description, Price
		FROM PullList, Comic
		WHERE [PullList].[CustomerID] = @CustomerID
			AND [PullList].[ComicID] = Comic.ComicID
	END
GO

print '' print '*** Creating sp_remove_comic_from_pull ***'
GO
CREATE PROCEDURE [dbo].[sp_remove_comic_from_pull]
	(
		@ComicID			[int],
		@CustomerID			[int]
	)
AS
	BEGIN
		DELETE FROM [dbo].[PullList]
		WHERE [PullList].[ComicID] = @ComicID
			AND [PullList].[CustomerID] = @CustomerID
	END
GO

print '' print '*** Creating sp_add_comic_to_order ***'
GO
CREATE PROCEDURE [dbo].[sp_add_comic_to_order]
	(
		@ComicID			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[OrderFormLine]
			([ComicID])
		VALUES
			(@ComicID)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_remove_comic_from_order ***'
GO
CREATE PROCEDURE [dbo].[sp_remove_comic_from_order]
	(
		@ComicID			[int]
	)
AS
	BEGIN
		DELETE FROM [dbo].[OrderFormLine]
		WHERE [OrderFormLine].[ComicID] = @ComicID
	END
GO

/* print '' print '*** Creating sp_submit_new_order ***'
GO
CREATE PROCEDURE [dbo].[sp_submit_new_order]
	(
		@CustomerID			[int],
		@ComicID			[int],
		@Date				[date]
	)
AS
	BEGIN
		INSERT INTO [dbo].[OrderForm]
			([CustomerID], [ComicID], [Date])
		VALUES
			(@CustomerID, @ComicID, @Date)
		RETURN @@ROWCOUNT
	END
GO */

/* print '' print '*** Creating sp_retrieve_comics_by_customer_order ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_comics_by_customer_order]
	(
		@CustomerID			[int]
	)
AS
	BEGIN
		SELECT Comic.ComicID, Title, Issue, Authors, Publisher, Rating, Description, Price
		FROM OrderForm, Comic
		WHERE [OrderForm].[CustomerID] = @CustomerID
			AND [OrderForm].[ComicID] = Comic.ComicID
	END
GO */

/* print '' print '*** Creating sp_calculate_order_total ***'
GO
CREATE PROCEDURE [dbo].[sp_calculate_order_total]
	(
		@CustomerID			[int],
		@ComicID			[int]
	)
AS
	BEGIN
		SELECT SUM(Comic.Price)
		FROM Comic, OrderForm
		WHERE [OrderForm].[CustomerID] = @CustomerID
			AND [OrderForm].[ComicID] = @ComicID
	END
GO */