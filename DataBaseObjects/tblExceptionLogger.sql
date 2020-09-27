
CREATE TABLE dbo.[tblExceptionLogger]
(
[ExcepLPK] INT identity(1,1),
[AssociateId] varchar(12) NOT NULL,
[ProxyId] varchar(12) NOT NULL,
[ExceptionSource] varchar(150) NULL,
[ExceptionMessage] varchar(2000) NULL,
[ExceptionStackTrace] varchar(2000) NULL,
[InnerException] varchar(2000) NULL,
[ApplicationType] varchar(50) NULL,
[ExceptionDateTime] datetime NOT NULL
PRIMARY KEY CLUSTERED 
(
       [ExcepLPK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
