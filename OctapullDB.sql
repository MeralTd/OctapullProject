USE [OctapullDB]
GO
/****** Object:  Table [dbo].[MeetingDeleteLog]    Script Date: 7.02.2025 22:19:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingDeleteLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MeetingID] [int] NULL,
	[MeetingName] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Description] [nvarchar](500) NULL,
	[Document] [nvarchar](max) NULL,
	[DeletedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MeetingDeleteLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meetings]    Script Date: 7.02.2025 22:19:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meetings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MeetingName] [nvarchar](50) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Document] [nvarchar](max) NULL,
	[IsCancelled] [bit] NOT NULL,
	[IsCancelledDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[DeletedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 7.02.2025 22:19:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[Expires] [datetime2](7) NOT NULL,
	[CreatedByIP] [nvarchar](max) NOT NULL,
	[Revoked] [datetime2](7) NULL,
	[RevokedByIP] [nvarchar](max) NULL,
	[ReplacedByToken] [nvarchar](max) NULL,
	[ReasonRevoked] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[DeletedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7.02.2025 22:19:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EMail] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[ProfilUrl] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[DeletedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Meetings] ON 

INSERT [dbo].[Meetings] ([ID], [MeetingName], [StartDate], [EndDate], [Description], [Document], [IsCancelled], [IsCancelledDate], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (16, N'd', CAST(N'2025-02-12T21:00:00.0000000' AS DateTime2), CAST(N'2025-02-02T21:00:00.0000000' AS DateTime2), N'we', NULL, 0, NULL, CAST(N'2025-02-07T21:41:52.7600596' AS DateTime2), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Meetings] OFF
GO
ALTER TABLE [dbo].[RefreshTokens] ADD  CONSTRAINT [DF_Table_1_created_date]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users1] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users1]
GO
/****** Object:  Trigger [dbo].[trg_MeetingDelete]    Script Date: 7.02.2025 22:19:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trg_MeetingDelete]
ON [dbo].[Meetings]
FOR DELETE
AS
BEGIN
   
    INSERT INTO MeetingDeleteLog (MeetingID, MeetingName, StartDate, EndDate,  Description, Document, DeletedDate)
    SELECT 
		ID,
        MeetingName,
		StartDate,
		EndDate,
		Description,
		Document,
        GETDATE()
    FROM deleted;
END;
GO
ALTER TABLE [dbo].[Meetings] ENABLE TRIGGER [trg_MeetingDelete]
GO
