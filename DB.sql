CREATE TABLE [Users] (
	UCode integer,
	CCode integer,
	Name text,
	Phone text,
	Password text,
	Email text,
	News boolean,
	Reminders boolean,
	Notifications boolean,
  CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED
  (
  [UCode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [Users] WITH CHECK ADD CONSTRAINT [Users_fk0] FOREIGN KEY ([CCode]) REFERENCES [City]([CCode])
ON UPDATE CASCADE
GO
ALTER TABLE [Users] CHECK CONSTRAINT [Users_fk0]
GO

CREATE TABLE [Advert] (
	ACode integer,
	UCode integer,
	Status integer,
	CCode integer,
	ACCode integer,
	Contact text,
	Phone text,
	Email text,
	Title text,
	Description text,
	History text,
	Price integer,
	Date datetime,
  CONSTRAINT [PK_ADVERT] PRIMARY KEY CLUSTERED
  (
  [ACode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [Advert] WITH CHECK ADD CONSTRAINT [Advert_fk2] FOREIGN KEY ([ACCode]) REFERENCES [Advert_Category]([ACCode])
ON UPDATE CASCADE
GO
ALTER TABLE [Advert] CHECK CONSTRAINT [Advert_fk2]
GO

CREATE TABLE [Advert_Category] (
	ACCode integer,
	Name text,
  CONSTRAINT [PK_ADVERT_CATEGORY] PRIMARY KEY CLUSTERED
  (
  [ACCode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

CREATE TABLE [Countries] (
	CoCode integer,
	Country text,
  CONSTRAINT [PK_COUNTRIES] PRIMARY KEY CLUSTERED
  (
  [CoCode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

CREATE TABLE [Images] (
	ICode binary,
	Url text,
  CONSTRAINT [PK_IMAGES] PRIMARY KEY CLUSTERED
  (
  [ICode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

CREATE TABLE [Images_To_Advert] (
	ACode integer,
	ICode integer,
  CONSTRAINT [PK_IMAGES_TO_ADVERT] PRIMARY KEY CLUSTERED
  (
  [ACode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [Images_To_Advert] WITH CHECK ADD CONSTRAINT [Images_To_Advert_fk1] FOREIGN KEY ([ICode]) REFERENCES [Images]([ICode])
ON UPDATE CASCADE
GO
ALTER TABLE [Images_To_Advert] CHECK CONSTRAINT [Images_To_Advert_fk1]
GO

CREATE TABLE [City] (
	CCode integer,
	Name text,
	CoCode integer,
  CONSTRAINT [PK_CITY] PRIMARY KEY CLUSTERED
  (
  [CCode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [City] WITH CHECK ADD CONSTRAINT [City_fk0] FOREIGN KEY ([CoCode]) REFERENCES [Countries]([CoCode])
ON UPDATE CASCADE
GO
ALTER TABLE [City] CHECK CONSTRAINT [City_fk0]
GO

CREATE TABLE [User_To_Advert] (
	UCode integer,
	ACode integer,
  CONSTRAINT [PK_USER_TO_ADVERT] PRIMARY KEY CLUSTERED
  (
  [UCode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [User_To_Advert] WITH CHECK ADD CONSTRAINT [User_To_Advert_fk1] FOREIGN KEY ([ACode]) REFERENCES [Advert]([ACode])
ON UPDATE CASCADE
GO
ALTER TABLE [User_To_Advert] CHECK CONSTRAINT [User_To_Advert_fk1]
GO

CREATE TABLE [Notification] (
	NCode integer,
	AuthorCode integer,
	RecipientCode integer,
	Content text,
  CONSTRAINT [PK_NOTIFICATION] PRIMARY KEY CLUSTERED
  (
  [NCode] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [Notification] WITH CHECK ADD CONSTRAINT [Notification_fk1] FOREIGN KEY ([RecipientCode]) REFERENCES [Users]([UCode])
ON UPDATE CASCADE
GO
ALTER TABLE [Notification] CHECK CONSTRAINT [Notification_fk1]
GO
