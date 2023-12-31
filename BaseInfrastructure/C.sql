﻿CREATE TABLE [dbo].[C]
(
	[BId] INT NOT NULL,
	[Name] NVARCHAR(32) NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_C] PRIMARY KEY CLUSTERED ([BId] ASC, [Name] ASC),
	CONSTRAINT [FK_C_B] FOREIGN KEY ([BId]) REFERENCES [dbo].[B] ([Id])
)
