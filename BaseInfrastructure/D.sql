﻿CREATE TABLE [dbo].[D]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[D] NVARCHAR(64) NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,
	CONSTRAINT [PK_D] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UQ_D_D] UNIQUE ([D] ASC)
)
