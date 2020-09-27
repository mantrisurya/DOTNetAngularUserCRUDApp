CREATE TABLE [dbo].[tbl_Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[MobileNumber] [varchar](20) NULL,
	[Status] [varchar](15) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](12) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](12) NULL,
	[ModifiedDate] [datetime] NULL,
	[MailSent] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_Users] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[tbl_Users] ADD  DEFAULT ((0)) FOR [MailSent]
GO


