﻿CREATE TABLE [dbo].[B]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[DateAndTime] DATETIME2 NOT NULL CONSTRAINT [DF_B_DateAndTime] DEFAULT(GETDATE()),
	CONSTRAINT [PK_B] PRIMARY KEY ([Id])
)