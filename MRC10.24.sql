y /****** Object:  Database [MRC]    Script Date: 11/20/2024 9:17:37 PM ******/
CREATE DATABASE [MRC]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_Gen5_2', MAXSIZE = 32 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [MRC] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [MRC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MRC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MRC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MRC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MRC] SET ARITHABORT OFF 
GO
ALTER DATABASE [MRC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MRC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MRC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MRC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MRC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MRC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MRC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MRC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MRC] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [MRC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MRC] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MRC] SET  MULTI_USER 
GO
ALTER DATABASE [MRC] SET ENCRYPTION ON
GO
ALTER DATABASE [MRC] SET QUERY_STORE = ON
GO
ALTER DATABASE [MRC] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[id] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NULL,
	[serviceId] [uniqueidentifier] NULL,
	[bookingDate] [datetime] NOT NULL,
	[status] [varchar](255) NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[content] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[id] [uniqueidentifier] NOT NULL,
	[cartId] [uniqueidentifier] NOT NULL,
	[productId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [uniqueidentifier] NOT NULL,
	[categoryName] [nvarchar](255) NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK__Category__3213E83F7E91D615] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[id] [uniqueidentifier] NOT NULL,
	[linkImage] [nvarchar](255) NULL,
	[productId] [uniqueidentifier] NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
 CONSTRAINT [PK__Image__3213E83F81CDEAC7] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NULL,
	[totalPrice] [decimal](18, 2) NULL,
	[status] [varchar](50) NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[shipStatus] [int] NULL,
	[shipCost] [int] NULL,
 CONSTRAINT [PK__Order__3213E83F1F39A9ED] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[id] [uniqueidentifier] NOT NULL,
	[productId] [uniqueidentifier] NULL,
	[orderId] [uniqueidentifier] NULL,
	[price] [decimal](18, 2) NULL,
	[quantity] [int] NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
 CONSTRAINT [PK__OrderDet__3213E83FACD9307B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Otp]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Otp](
	[id] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[otpCode] [nvarchar](50) NOT NULL,
	[createDate] [datetime] NOT NULL,
	[expiresAt] [datetime] NOT NULL,
	[isValid] [bit] NOT NULL,
 CONSTRAINT [PK_Otp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentMethod] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [uniqueidentifier] NOT NULL,
	[productName] [nvarchar](255) NOT NULL,
	[description] [text] NOT NULL,
	[quantity] [int] NOT NULL,
	[status] [varchar](50) NOT NULL,
	[categoryId] [uniqueidentifier] NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[price] [decimal](18, 0) NULL,
 CONSTRAINT [PK__Product__3213E83F30DB6A16] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[id] [uniqueidentifier] NOT NULL,
	[serviceName] [varchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](10, 2) NOT NULL,
	[duration] [int] NOT NULL,
	[status] [varchar](255) NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/20/2024 9:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [uniqueidentifier] NOT NULL,
	[userName] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[email] [varchar](255) NULL,
	[fullName] [varchar](255) NULL,
	[status] [nvarchar](255) NULL,
	[gender] [varchar](10) NULL,
	[role] [varchar](50) NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[phoneNumber] [varchar](50) NULL,
	[delDate] [datetime] NULL,
 CONSTRAINT [PK__User__3213E83F97239305] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'6912dd86-83f1-4360-b73c-0277277279c4', N'7d7c39b4-acd8-4dc2-83f9-7d89604008f7', CAST(N'2024-11-19T17:49:45.197' AS DateTime), CAST(N'2024-11-19T17:49:45.197' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'be0eb160-370c-434d-8fe0-092a257f76af', N'5aa6b11c-771f-458b-96f2-93c8c593a838', CAST(N'2024-10-17T22:35:15.547' AS DateTime), CAST(N'2024-10-17T22:35:15.547' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'7edb694a-bf68-46f0-af9e-1a41deb4f441', N'4260c50a-ff1e-47f2-a312-bfa6df961cd7', CAST(N'2024-09-05T11:21:18.547' AS DateTime), CAST(N'2024-09-05T11:21:18.547' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'da669dad-9a41-4c68-a34d-2516dd535578', N'59d09a3d-38a6-4133-a4c5-80d07b2bed27', CAST(N'2024-10-11T12:17:39.677' AS DateTime), CAST(N'2024-10-11T12:17:39.677' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'66c15da9-a32f-4ca8-b9ad-2a24d1c6dab7', N'f6cdf58c-7388-4db4-be10-ad3e6cf79c9c', CAST(N'2024-09-05T11:19:11.713' AS DateTime), CAST(N'2024-09-05T11:19:11.713' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(N'2024-09-18T22:52:05.913' AS DateTime), CAST(N'2024-09-18T22:52:05.913' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'35eb23bf-d548-4876-b119-7358365c4805', N'e5a971ba-fd77-4b8e-92fe-1c7737b5ac14', CAST(N'2024-08-29T13:09:32.657' AS DateTime), CAST(N'2024-08-29T13:09:32.657' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'6acc99bc-d925-4f1e-bd75-8ad8f150a048', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(N'2024-08-30T13:02:00.163' AS DateTime), CAST(N'2024-08-30T13:02:00.163' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'8c393bc8-5593-4cf7-a3f0-990c8b481191', N'5b9c3699-d94a-44ae-9e79-00004bb96f5f', CAST(N'2024-08-30T13:26:36.800' AS DateTime), CAST(N'2024-08-30T13:26:36.800' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'df47d91a-1a77-41a6-b136-e9fab7893798', N'9bb61de5-af7c-4afa-9a05-4d9578475ed0', CAST(N'2024-08-30T01:38:33.610' AS DateTime), CAST(N'2024-08-30T01:38:33.610' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'592acc1c-9ed9-482b-bdd2-4d3cd0a73b92', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'3b294bb7-3841-466d-a852-5ee45a3ed154', 1, CAST(N'2024-09-23T13:33:23.603' AS DateTime), CAST(N'2024-09-23T13:33:23.603' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'2683c988-5249-4d82-9dd3-97fec055b853', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'b575b6fa-9183-4ed7-a373-762073e58e35', 1, CAST(N'2024-09-23T13:33:33.013' AS DateTime), CAST(N'2024-09-23T13:33:33.013' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'9c47b08d-4937-4c8f-ba3a-00cfeee8894e', N'11111', CAST(N'2024-11-19T22:30:38.453' AS DateTime), CAST(N'2024-11-19T22:43:16.770' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'42bcc747-7aff-4f43-a6c5-16d518c176a0', N'Nước tương', CAST(N'2024-11-19T22:03:53.177' AS DateTime), CAST(N'2024-11-19T22:43:18.613' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'd35abc31-6e07-41d5-b72b-25f81ebdc772', N'12312311', CAST(N'2024-11-19T22:07:09.303' AS DateTime), CAST(N'2024-11-19T22:43:20.120' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'8691d5ac-f407-4882-b0d4-288dafe92c25', N'Tra111', CAST(N'2024-11-19T22:20:50.030' AS DateTime), CAST(N'2024-11-19T22:43:21.750' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'27c29403-ae4a-4bab-bebc-2e5bb57f8880', N'Cà Phơ', CAST(N'2024-11-19T20:32:38.530' AS DateTime), CAST(N'2024-11-19T21:13:25.683' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'2ed3439a-d346-403f-9c05-2eeebe4e9190', N'123123123123', CAST(N'2024-11-19T21:20:30.730' AS DateTime), CAST(N'2024-11-19T21:20:40.263' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'cfa574ad-6a8e-4054-944c-2f93d05fe0cb', N'Trag', CAST(N'2024-11-19T22:20:45.397' AS DateTime), CAST(N'2024-11-19T22:43:22.307' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'e93337c2-7eed-4658-9c31-30325baaa233', N'Cà phê', CAST(N'2024-11-19T22:03:49.337' AS DateTime), CAST(N'2024-11-19T22:43:22.483' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'fecd55e7-c87a-44a0-89da-38b6e6f7ae14', N'123123123123123', CAST(N'2024-11-19T21:14:20.427' AS DateTime), CAST(N'2024-11-19T21:14:22.067' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'2bb0cf5e-87bb-4d02-8e28-3c7a44964514', N'2141231', CAST(N'2024-11-19T22:03:45.753' AS DateTime), CAST(N'2024-11-19T22:43:22.663' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'18edcbe1-8d6d-407c-91b5-3e4e52baa056', N'Bánh', CAST(N'2024-11-19T22:44:11.820' AS DateTime), CAST(N'2024-11-20T01:09:27.023' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'907bab88-6b53-496e-9f61-433f8cb4c1ef', N'tra an than2', CAST(N'2024-11-19T22:22:19.283' AS DateTime), CAST(N'2024-11-19T22:43:22.980' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'090d3bb9-c95e-4f15-a509-5e2702395a5e', N'Cafe', CAST(N'2024-08-16T23:00:28.130' AS DateTime), CAST(N'2024-08-16T23:00:28.130' AS DateTime), NULL)
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'005ffd05-6901-4f5a-8d5f-6212bffccf5e', N'123123123123123123', CAST(N'2024-11-19T21:20:32.583' AS DateTime), CAST(N'2024-11-19T21:20:40.790' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'650955c7-dd33-413f-ad69-6544555b4897', N'Cà phê', CAST(N'2024-11-20T15:27:39.953' AS DateTime), CAST(N'2024-11-20T15:27:39.953' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'ffcbcd26-71b2-4a45-a692-6b3e66e23fb8', N'Bánh', CAST(N'2024-11-19T20:35:59.757' AS DateTime), CAST(N'2024-11-19T21:28:35.783' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'245a1144-dec8-4bc6-a19c-6fdd72ebbe97', N'123123123', CAST(N'2024-11-19T21:20:29.737' AS DateTime), CAST(N'2024-11-19T21:20:39.863' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'867e7a70-e209-4d05-8da3-82de4ac901fb', N'Thảo dược', CAST(N'2024-11-20T15:27:46.027' AS DateTime), CAST(N'2024-11-20T15:27:46.027' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'10bf626e-0d1e-4754-9e2d-8e9a1dced5bd', N'123123', CAST(N'2024-11-19T21:14:18.253' AS DateTime), CAST(N'2024-11-19T21:18:20.570' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'7cbef365-3148-4477-acbf-9376cbc5f761', N'123123123', CAST(N'2024-11-19T21:14:19.467' AS DateTime), CAST(N'2024-11-19T21:14:46.397' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'f414fe97-c731-498f-9f8f-9fea1934df2e', N'123123', CAST(N'2024-11-19T22:03:01.240' AS DateTime), CAST(N'2024-11-19T22:46:04.140' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'1a15bf30-af44-43c8-a6c9-a4f86af9b7a6', N'Trà', CAST(N'2024-08-20T14:07:06.227' AS DateTime), CAST(N'2024-11-19T21:28:34.163' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'f4050b6c-c1a6-4d25-ae07-b3d821e04dad', N'Thảo dược', CAST(N'2024-11-19T20:33:03.303' AS DateTime), CAST(N'2024-11-19T20:42:33.530' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'33a69546-25de-4cf4-899f-bbc2196a51b7', N'D123', CAST(N'2024-11-20T01:09:19.570' AS DateTime), CAST(N'2024-11-20T01:09:30.913' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'e8b3fd1f-47bf-4741-a8cf-bf3611cbff83', N'123123123123123', CAST(N'2024-11-19T21:20:31.587' AS DateTime), CAST(N'2024-11-19T21:20:38.233' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'78564bf0-e7b5-4f22-8af2-dd0360fdbae3', N'123123', CAST(N'2024-11-19T21:20:28.070' AS DateTime), CAST(N'2024-11-19T21:20:39.217' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'38d28eec-b8f4-4fc1-9612-de1b8d51ef36', N'Thuốc', CAST(N'2024-11-20T15:28:08.300' AS DateTime), CAST(N'2024-11-20T15:28:08.300' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', N'Trà', CAST(N'2024-11-20T01:09:42.870' AS DateTime), CAST(N'2024-11-20T01:09:42.870' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'610c70b9-d473-46ac-bfa9-f87bab739e5a', N'trà', CAST(N'2024-11-19T22:44:05.707' AS DateTime), CAST(N'2024-11-20T01:09:34.483' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8648a759-1bca-498f-9751-20642f303db9', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5091bb50-a7f7-4307-90f7-9e7d561c6e7b_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'061b0116-cf04-45fb-a818-069e00a89138', CAST(N'2024-11-20T20:35:14.203' AS DateTime), CAST(N'2024-11-20T20:35:14.213' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'609600d8-6b9e-44e8-986f-27b12f628733', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F77f2f474-146f-4429-93bf-77376dfd7d1d_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.17.03.png?alt=media', N'da95aeca-217d-4e1a-a667-158eaf0deb5f', CAST(N'2024-11-20T19:58:09.753' AS DateTime), CAST(N'2024-11-20T19:58:09.753' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8c175e86-fac5-409c-94d4-2bd5facfdabf', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F37e499da-ad62-43a7-80e2-0f26e802c68b_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2016.06.33.png?alt=media', N'059a27a3-d1e9-457b-aff6-d94596720a2f', CAST(N'2024-11-20T19:45:44.507' AS DateTime), CAST(N'2024-11-20T19:45:44.507' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e29a7712-4e53-41e8-824d-37a97976b812', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fe771bd34-b5ba-4aa1-ab90-ffd567d17210_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.17.03.png?alt=media', N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', CAST(N'2024-11-20T16:42:04.053' AS DateTime), CAST(N'2024-11-20T16:42:04.053' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'a1049d0b-86ef-4974-ac92-523de7d739e1', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F43124184-bf5b-4cbc-8bee-bce8d1e31435_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-03%20lu%CC%81c%2022.34.29.png?alt=media', N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', CAST(N'2024-11-20T16:42:04.053' AS DateTime), CAST(N'2024-11-20T16:42:04.053' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'ea6e3ab3-1f89-4495-8142-5f0eb4d53448', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fdaf9d0e9-9122-48f5-acb8-f147f8982546_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'5ce6d9e9-c0d4-412e-b857-aeab804afcda', CAST(N'2024-11-20T21:09:24.587' AS DateTime), CAST(N'2024-11-20T21:09:24.587' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'20c51703-3f17-4e8b-a590-74fd83e87ae9', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F9533e56e-05db-4ad2-803b-37ab6dbe578e_Lee%20Men%27s%20Relaxed%20Jeans.jpg?alt=media', N'4999d04b-c30d-488d-b5dd-d5d625e0a838', CAST(N'2024-08-17T01:45:40.997' AS DateTime), CAST(N'2024-08-17T01:45:40.997' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8bc10672-cb98-427f-8146-80fd930948d7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ffa05365d-c5f5-4d67-8388-69c6d89efb1d_Mountain%20Hardwear%20Insulated%20Jacke.jpg?alt=media', N'4999d04b-c30d-488d-b5dd-d5d625e0a838', CAST(N'2024-08-17T01:45:40.997' AS DateTime), CAST(N'2024-08-17T01:45:40.997' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'95dd924b-3665-4c79-9999-8ab56f9ac7aa', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F1010e0ec-f93c-4a58-9831-c7ca65d75a95_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'3e30c213-db9c-4e96-9897-45d1731d6084', CAST(N'2024-11-20T21:08:19.723' AS DateTime), CAST(N'2024-11-20T21:08:19.723' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'95d0dc2f-3b5d-485b-8ece-9fb0b96fb373', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F8b6c58dd-fbe1-4b3c-a78a-2edf9695601e_Fila%20Sports%20Hoodie.jpg?alt=media', N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', CAST(N'2024-08-17T01:32:44.430' AS DateTime), CAST(N'2024-08-17T01:32:44.430' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'27d63930-617a-461e-a9bc-a75962c419f2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa41024bf-e0c5-4a7e-a0c4-116a28ca0a1a_Mountain%20Hardwear%20Insulated%20Jacke.jpg?alt=media', N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', CAST(N'2024-08-17T01:32:44.430' AS DateTime), CAST(N'2024-08-17T01:32:44.430' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'c0563950-0b48-428d-81b4-b3c9cbc2d7a4', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fc482e146-8392-4c7a-bc29-7d9b200b1a5a_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2023.43.12.png?alt=media', N'4e66d5c9-ba0d-4617-b1ae-9b9f1734b97a', CAST(N'2024-11-20T16:40:59.833' AS DateTime), CAST(N'2024-11-20T16:40:59.833' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'82428415-6b17-4538-b1f2-b42e4c8765ef', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F375169f3-3a3d-40fa-b0a9-087d6994fd71_Zara%20Wrap%20Dress.jpg?alt=media', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', CAST(N'2024-08-20T11:21:11.690' AS DateTime), CAST(N'2024-08-20T11:21:11.700' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e8590df0-97e0-4119-a12a-bbbd31025f11', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ff3dce429-4c55-4762-9a39-97d9fb2d2b30_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'21ce7e0f-809a-42c4-80e8-a2063a4b7295', CAST(N'2024-11-20T21:03:12.370' AS DateTime), CAST(N'2024-11-20T21:03:12.380' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'9febb893-8a66-445e-8810-cf261a3bc972', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb8553d90-5c66-402b-b7dd-89df5dce066c_Zara%20Wrap%20Dress.jpg?alt=media', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', CAST(N'2024-08-20T11:21:14.317' AS DateTime), CAST(N'2024-08-20T11:21:14.327' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'2acc51cf-8fec-4342-b82f-d8466b7ee58c', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F4a26b3aa-85b8-4f66-b0ab-db0206c4e9f7_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.59.23.png?alt=media', N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', CAST(N'2024-11-20T16:42:04.053' AS DateTime), CAST(N'2024-11-20T16:42:04.053' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'75c16823-ae35-46d4-b636-e22630488ab2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F18f6c3d3-15d3-44db-8645-f02f3a8848c5_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.17.03.png?alt=media', N'190da44b-c901-4d05-ae74-8900067fbde2', CAST(N'2024-11-20T19:59:43.120' AS DateTime), CAST(N'2024-11-20T19:59:43.120' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'55d03299-2801-42c2-acf7-f7ff7c18efe7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fe5abeef6-fedd-4387-8583-7d05cc565351_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'b5e93e04-a704-449e-ab2c-891890d02f2b', CAST(N'2024-11-20T20:47:24.813' AS DateTime), CAST(N'2024-11-20T20:47:24.813' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'98c0d8d0-8a10-4d9b-870c-fa910c548f98', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5c6aa4ac-0eeb-4ef8-8d9e-d61d20340416_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2016.06.33.png?alt=media', N'5593af7c-431d-4dbe-83f0-75475666c0b0', CAST(N'2024-11-20T19:51:46.513' AS DateTime), CAST(N'2024-11-20T19:51:46.513' AS DateTime))
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost]) VALUES (N'4858dbf3-3aaa-4505-851b-1a6427e7f16a', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T13:42:18.947' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost]) VALUES (N'066fcba6-b5d2-4b18-8e08-4d62327a4900', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(630015.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T13:05:43.097' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost]) VALUES (N'902f82cb-c8bc-4386-96e4-5df2b545f33b', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(715045.00 AS Decimal(18, 2)), N'PENDING', CAST(N'2024-09-22T17:49:55.817' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost]) VALUES (N'9fe5f6a7-2f48-4551-8373-68a9d5a64ad0', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T14:07:32.517' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost]) VALUES (N'b4f23e0a-8de2-4095-90c1-7b20d179392e', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(0.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-22T21:37:22.480' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost]) VALUES (N'6415bf4d-ab4c-4505-b7cb-95c66169d255', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(700045.00 AS Decimal(18, 2)), N'PENDING', CAST(N'2024-09-22T16:16:47.510' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'01cd7dd8-121e-4093-9613-08900afed08a', N'b575b6fa-9183-4ed7-a373-762073e58e35', N'6415bf4d-ab4c-4505-b7cb-95c66169d255', CAST(150000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T16:17:06.290' AS DateTime), CAST(N'2024-09-22T16:17:06.293' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9705f6dc-8ee5-4d42-b191-123b69af10d9', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'9fe5f6a7-2f48-4551-8373-68a9d5a64ad0', CAST(15000.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-23T14:07:32.523' AS DateTime), CAST(N'2024-09-23T14:07:32.523' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'bf5173d0-1aa9-40ee-87d8-207e51cfcde4', N'cc8d1ba9-103a-4469-b248-9604178ff639', N'066fcba6-b5d2-4b18-8e08-4d62327a4900', CAST(15.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-23T13:05:43.127' AS DateTime), CAST(N'2024-09-23T13:05:43.127' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'd5489db3-0d73-4aad-84c6-2ff192975470', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(200000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T17:49:55.823' AS DateTime), CAST(N'2024-09-22T17:49:55.823' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9fe7e864-66bb-4eeb-997f-4945ce675fb2', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'6415bf4d-ab4c-4505-b7cb-95c66169d255', CAST(200000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T16:16:52.920' AS DateTime), CAST(N'2024-09-22T16:16:52.927' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'cb017b8c-21c6-41e9-880f-60c357f83534', N'b575b6fa-9183-4ed7-a373-762073e58e35', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(150000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T17:49:55.850' AS DateTime), CAST(N'2024-09-22T17:49:55.850' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9b625c2f-142c-4671-9313-6371290702bb', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'066fcba6-b5d2-4b18-8e08-4d62327a4900', CAST(200000.00 AS Decimal(18, 2)), 3, CAST(N'2024-09-23T13:05:43.103' AS DateTime), CAST(N'2024-09-23T13:05:43.107' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'8fae577c-3d1c-444a-a61f-8ffe15765aa2', N'cc8d1ba9-103a-4469-b248-9604178ff639', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(15.00 AS Decimal(18, 2)), 3, CAST(N'2024-09-22T17:49:55.847' AS DateTime), CAST(N'2024-09-22T17:49:55.847' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'b6dfa558-a3b9-48fb-9f15-a100e2c513ed', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(15000.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-22T17:49:55.850' AS DateTime), CAST(N'2024-09-22T17:49:55.850' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'fd88ce0d-8538-42fb-b6ae-a5733d269e34', N'cc8d1ba9-103a-4469-b248-9604178ff639', N'6415bf4d-ab4c-4505-b7cb-95c66169d255', CAST(15.00 AS Decimal(18, 2)), 3, CAST(N'2024-09-22T16:16:59.590' AS DateTime), CAST(N'2024-09-22T16:16:59.597' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'709bf9c8-e5ee-471c-850b-b37f4e02a75d', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'4858dbf3-3aaa-4505-851b-1a6427e7f16a', CAST(15000.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-23T13:42:18.953' AS DateTime), CAST(N'2024-09-23T13:42:18.953' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'fe600432-b34b-4ad4-aea1-d6bf8be2230c', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'066fcba6-b5d2-4b18-8e08-4d62327a4900', CAST(15000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-23T13:05:43.127' AS DateTime), CAST(N'2024-09-23T13:05:43.127' AS DateTime))
GO
INSERT [dbo].[Otp] ([id], [userId], [otpCode], [createDate], [expiresAt], [isValid]) VALUES (N'5cff6237-0b57-42ea-b46b-03236938bb78', N'7d7c39b4-acd8-4dc2-83f9-7d89604008f7', N'829177', CAST(N'2024-11-19T17:49:45.377' AS DateTime), CAST(N'2024-11-19T17:59:45.377' AS DateTime), 1)
GO
INSERT [dbo].[Otp] ([id], [userId], [otpCode], [createDate], [expiresAt], [isValid]) VALUES (N'e95ead2c-20cd-4ee0-b52b-579210df76b8', N'7d7c39b4-acd8-4dc2-83f9-7d89604008f7', N'416568', CAST(N'2024-11-19T22:37:39.967' AS DateTime), CAST(N'2024-11-19T22:47:39.967' AS DateTime), 1)
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'cfc29ea4-e90e-40a7-b2b0-5617f125b252', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-23T13:41:06.280' AS DateTime), CAST(N'2024-09-23T13:41:06.280' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'4130ddce-df5f-4afa-89ac-67e7f2ea476d', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-23T14:06:00.970' AS DateTime), CAST(N'2024-09-23T14:06:00.970' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'd4d36859-52ee-427b-a4ff-87ac8cbe0db2', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(400045.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-18T23:13:19.600' AS DateTime), CAST(N'2024-09-18T23:13:19.600' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'c84eaca7-78c9-42b2-8341-96f607df7877', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(400045.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-22T16:16:06.063' AS DateTime), CAST(N'2024-09-22T16:16:06.063' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'32586957-6871-4971-b94a-a93c02e52a79', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-22T17:46:40.643' AS DateTime), CAST(N'2024-09-22T17:46:40.643' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'd53793d6-b361-45a4-b094-cd6173823cb9', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-23T13:34:34.300' AS DateTime), CAST(N'2024-09-23T13:34:34.300' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'a2ae560e-2dee-4a53-b03e-cde0c4373a0d', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(30000.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-22T21:36:40.313' AS DateTime), CAST(N'2024-09-22T21:36:40.313' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'e9cebff6-1a2c-43ba-9144-d4a81dda17bf', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(45.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-19T15:08:30.680' AS DateTime), CAST(N'2024-09-19T15:08:30.680' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'd50606b4-4b63-4f51-a4d0-d7e6c57c36de', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(30000.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-23T13:01:53.740' AS DateTime), CAST(N'2024-09-23T13:01:53.740' AS DateTime))
GO
INSERT [dbo].[Payment] ([PaymentID], [UserID], [Amount], [PaymentMethod], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'e2e364c2-c7bc-48e0-93e0-ee7cc69cc531', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(400045.00 AS Decimal(18, 2)), N'PayOS', N'Pending', CAST(N'2024-09-22T16:02:00.917' AS DateTime), CAST(N'2024-09-22T16:02:00.917' AS DateTime))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'061b0116-cf04-45fb-a818-069e00a89138', N'huy16', N'huyyyy', 11, N'Available', N'e93337c2-7eed-4658-9c31-30325baaa233', CAST(N'2024-11-20T20:35:07.817' AS DateTime), CAST(N'2024-11-20T20:35:07.823' AS DateTime), CAST(160000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'da95aeca-217d-4e1a-a667-158eaf0deb5f', N'123123123', N'hihi', 12, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:58:09.447' AS DateTime), CAST(N'2024-11-20T19:58:09.447' AS DateTime), CAST(123 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'3e30c213-db9c-4e96-9897-45d1731d6084', N'ozzzz', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã r? b?t ng? trong v? ki?n ch?ng l?i ông Trump</strong></a></p><p>Phiên tranh lu?n b? h?y b? t?i Georgia cùng m?t th?m phán Arizona rút lui dã làm d?y lên câu h?i v? tuong lai c?a 4 v? ki?n c?p bang ch?ng l?i ông Donald Trump và các d?ng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump ch?n t? phú Lutnick làm b? tru?ng Thuong m?i</strong></a></li></ul><p><br></p>', 11, N'Available', N'42bcc747-7aff-4f43-a6c5-16d518c176a0', CAST(N'2024-11-20T21:08:19.117' AS DateTime), CAST(N'2024-11-20T21:08:19.117' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'cd3a6be2-dbf1-4d5c-8c3a-5cddae96733b', N'tra ngon', N'ngon vch', 11, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:44:54.313' AS DateTime), CAST(N'2024-08-18T14:44:17.853' AS DateTime), CAST(110000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'string', N'string', 7, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-16T23:26:08.877' AS DateTime), CAST(N'2024-08-16T23:26:08.880' AS DateTime), CAST(200000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'5593af7c-431d-4dbe-83f0-75475666c0b0', N'Cà phê hoà tan gói 500gg', N'<h1>Duybpz</h1>', 11, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:51:46.183' AS DateTime), CAST(N'2024-11-20T19:51:46.183' AS DateTime), CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b575b6fa-9183-4ed7-a373-762073e58e35', N'tra xanh', N'hoi bi ngon', 11, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-20T11:13:53.583' AS DateTime), CAST(N'2024-08-20T11:13:53.583' AS DateTime), CAST(150000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'190da44b-c901-4d05-ae74-8900067fbde2', N'1231231231', N'hihi', 12, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:59:42.813' AS DateTime), CAST(N'2024-11-20T19:59:42.813' AS DateTime), CAST(123 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b5e93e04-a704-449e-ab2c-891890d02f2b', N'huy1', N'huy1', 1111, N'Available', N'e93337c2-7eed-4658-9c31-30325baaa233', CAST(N'2024-11-20T20:47:24.253' AS DateTime), CAST(N'2024-11-20T20:47:24.253' AS DateTime), CAST(11111 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'cc8d1ba9-103a-4469-b248-9604178ff639', N'Trà Sữa', N'Trà S?a', 7, N'Unavailable', N'1a15bf30-af44-43c8-a6c9-a4f86af9b7a6', CAST(N'2024-08-20T14:07:19.110' AS DateTime), CAST(N'2024-11-19T21:28:34.143' AS DateTime), CAST(15 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4e66d5c9-ba0d-4617-b1ae-9b9f1734b97a', N'Trà hoa cúc', N'<h1>S?n ph?m có ch?a cafein</h1>', 12, N'Available', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-20T16:40:59.220' AS DateTime), CAST(N'2024-11-20T16:40:59.220' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'21ce7e0f-809a-42c4-80e8-a2063a4b7295', N'huyqq', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã r? b?t ng? trong v? ki?n ch?ng l?i ông Trump</strong></a></p><p>Phiên tranh lu?n b? h?y b? t?i Georgia cùng m?t th?m phán Arizona rút lui dã làm d?y lên câu h?i v? tuong lai c?a 4 v? ki?n c?p bang ch?ng l?i ông Donald Trump và các d?ng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump ch?n t? phú Lutnick làm b? tru?ng Thuong m?i</strong></a></li></ul><p><br></p>', 15, N'Available', N'42bcc747-7aff-4f43-a6c5-16d518c176a0', CAST(N'2024-11-20T21:03:06.060' AS DateTime), CAST(N'2024-11-20T21:03:06.067' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'5ce6d9e9-c0d4-412e-b857-aeab804afcda', N'huynggg', N'huy d?p trai', 11, N'Available', N'42bcc747-7aff-4f43-a6c5-16d518c176a0', CAST(N'2024-11-20T21:09:24.077' AS DateTime), CAST(N'2024-11-20T21:09:24.077' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', N'Trà hoa cúc 500g', N'<h1>S?n ph?m có ch?a Th?ng Cu?ng Lord</h1>', 12, N'Available', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-20T16:42:02.273' AS DateTime), CAST(N'2024-11-20T16:42:02.273' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', N'huy', N'ngon', 10, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:32:42.873' AS DateTime), CAST(N'2024-08-18T01:22:23.527' AS DateTime), CAST(11011 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4999d04b-c30d-488d-b5dd-d5d625e0a838', N'cafe hat', N'ngon vch', 11, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:45:40.077' AS DateTime), CAST(N'2024-08-17T01:45:40.077' AS DateTime), NULL)
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'059a27a3-d1e9-457b-aff6-d94596720a2f', N'Cà phê hoà tan gói 500g', N'<h3>Cà phê ngon nhung d?</h3>', 11, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:45:44.190' AS DateTime), CAST(N'2024-11-20T19:45:44.190' AS DateTime), CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'tran nguyen', N'ngon ngon', 7, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-20T11:19:51.837' AS DateTime), CAST(N'2024-08-20T11:19:51.847' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Service] ([id], [serviceName], [description], [price], [duration], [status], [insDate], [upDate]) VALUES (N'e117cd04-55ee-4ec8-86e1-72e9032ab992', N'WORKSHOP', NULL, CAST(0.00 AS Decimal(10, 2)), 0, N'Available', CAST(N'2024-10-24T15:08:07.817' AS DateTime), CAST(N'2024-10-24T15:08:07.817' AS DateTime))
GO
INSERT [dbo].[Service] ([id], [serviceName], [description], [price], [duration], [status], [insDate], [upDate]) VALUES (N'9884a9d2-aa1e-42dc-a782-7f4647506966', N'FACTORY DESIGN', NULL, CAST(0.00 AS Decimal(10, 2)), 0, N'Available', CAST(N'2024-10-24T15:08:25.617' AS DateTime), CAST(N'2024-10-24T15:08:25.617' AS DateTime))
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'5b9c3699-d94a-44ae-9e79-00004bb96f5f', N'tranquoccuong0179', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'tranquoccuong0179@gmail.com', N'Cu?ng Tr?n', N'Available', NULL, N'Customer', CAST(N'2024-08-30T13:26:36.680' AS DateTime), CAST(N'2024-08-30T13:26:36.680' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'e5a971ba-fd77-4b8e-92fe-1c7737b5ac14', N'bangpham', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'bangphamtpst@gmail.com', N'Ph?m Trúc Bang', N'Unavailable', N'Female', N'Customer', CAST(N'2024-08-29T13:09:32.533' AS DateTime), CAST(N'2024-08-29T13:09:32.533' AS DateTime), N'0336607452', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'b7e29dcf-64d3-4742-ab42-2683158aeb34', N'bangpham123', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'phamtrucbang2008ptb@gmail.com', N'Ph?m Trúc Bang', N'Unavailable', N'Female', N'Customer', CAST(N'2024-08-29T13:14:05.187' AS DateTime), CAST(N'2024-08-29T13:14:05.187' AS DateTime), N'0336607453', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'b108281e-6b43-48b2-a781-49e64d0bb7ab', N'manager1', N'OA+XcdLfhWbOK9W47XcrC7dP1kV/uAOrLSZ8OU2Jx1A=', N'manager1@gmail.com', N'string', NULL, N'Male', N'Manager', CAST(N'2024-08-16T21:48:35.557' AS DateTime), CAST(N'2024-08-16T21:48:35.560' AS DateTime), N'0901234568', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'9bb61de5-af7c-4afa-9a05-4d9578475ed0', N'quoccuong', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'cuongtqse160059@fpt.edu.vn', N'Tr?n Qu?c Cu?ng', N'Available', N'Male', N'Customer', CAST(N'2024-08-30T01:38:33.527' AS DateTime), CAST(N'2024-08-30T01:38:33.527' AS DateTime), N'0363919179', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'e4c9c30b-c2df-46d9-9a88-5600e278695b', N'vhuy16', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Admin', CAST(N'2024-08-14T21:37:48.583' AS DateTime), CAST(N'2024-08-14T21:37:48.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'827fba0f-ccb3-4ce1-b728-7c27e1a48777', N'manager', N'buSkac1OkQU4R/XT/LYdvMkejw7xC+d0jaTEobo4LRc=', N'manager@gmail.com', N'manager', N'Available', N'Male', N'Manager', CAST(N'2024-11-19T20:30:11.857' AS DateTime), CAST(N'2024-11-19T20:30:11.857' AS DateTime), N'0333311232', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'7d7c39b4-acd8-4dc2-83f9-7d89604008f7', N'cuongtq', N'9F69GG+kpIcM0QHspUF2Cmr9DIODeeEJLnebUpskIvQ=', N'tranquoccuong13072002@gmail.com', N'string', N'Unavailable', N'Female', N'Customer', CAST(N'2024-11-19T17:49:45.103' AS DateTime), CAST(N'2024-11-19T17:49:45.103' AS DateTime), N'0987654321', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'59d09a3d-38a6-4133-a4c5-80d07b2bed27', N'vhuy10', N'i7DPbrmxfQ99IrRW8SElfcElTh8BZlNwR2OD6ndt9BQ=', N'voquanghuy56@gmail.com', N'string', N'Unavailable', N'Male', N'Customer', CAST(N'2024-10-11T12:17:39.580' AS DateTime), CAST(N'2024-10-11T12:17:39.580' AS DateTime), N'0901960523', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'6a6f6a03-56ee-48e5-b458-825fd9e7b6c4', N'vhuy1610', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Manager', CAST(N'2024-08-15T13:23:06.377' AS DateTime), CAST(N'2024-08-15T13:23:06.377' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'5aa6b11c-771f-458b-96f2-93c8c593a838', N'vhuy12', N'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=', N'voquanghuy506@gmail.com', N'string', N'Available', N'Male', N'Customer', CAST(N'2024-10-17T22:35:15.480' AS DateTime), CAST(N'2024-10-17T22:38:20.157' AS DateTime), N'0901960501', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'ce419771-09d5-4a6d-8021-a04427dd5d81', N'vhuy16102002', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Customer', CAST(N'2024-08-15T13:25:03.243' AS DateTime), CAST(N'2024-08-15T13:25:03.243' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'f6cdf58c-7388-4db4-be10-ad3e6cf79c9c', N'tangoccu', N'86I39YKhpaYNH+32p/XuNbuXHWQz1mWCKeq0r3fhB/Y=', N'ngocan2003krp@gmail.com', N'T? Ng?c Cu', N'Unavailable', N'Male', N'Customer', CAST(N'2024-09-05T11:19:11.713' AS DateTime), CAST(N'2024-09-05T11:19:11.713' AS DateTime), N'0356638270', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'4260c50a-ff1e-47f2-a312-bfa6df961cd7', N'tangoccu1', N'O6i3JEdRGBho4KYgWSnW4a/qMkyOLntDVR8GYBBnFvE=', N'antnqe170035@fpt.edu.vn', N'T? Ng?c Cu', N'Available', N'Male', N'Customer', CAST(N'2024-09-05T11:21:18.547' AS DateTime), CAST(N'2024-09-05T11:21:18.547' AS DateTime), N'0356638271', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'4487d6fa-9ce6-43f7-87b5-c3810702de12', N'tranquoccuong13072002', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'tranquoccuong13072002@gmail.com', N'Cu?ng Tr?n', N'Available', NULL, N'Customer', CAST(N'2024-08-20T13:56:20.607' AS DateTime), CAST(N'2024-08-20T13:56:20.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'a3d3fba5-f0c2-4716-8a9c-db1754999513', N'duybpz', N'PPPAC7TVWzdd/5xnKBxSi5gGz0o1NfOGkpQwt6AOVTI=', N'duybpz@gmail.com', N'V? Thanh Duy', N'Available', N'Male', N'Customer', CAST(N'2024-08-30T13:02:00.007' AS DateTime), CAST(N'2024-08-30T13:02:00.010' AS DateTime), N'0123456789', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'huyvqse160031', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'huyvqse16001@fpt.edu.vn', N'Vo Quang Huy (K16_HCM)', N'Available', NULL, N'Customer', CAST(N'2024-09-18T22:52:05.853' AS DateTime), CAST(N'2024-09-18T22:52:05.853' AS DateTime), NULL, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_index_0]    Script Date: 11/20/2024 9:17:40 PM ******/
CREATE NONCLUSTERED INDEX [User_index_0] ON [dbo].[User]
(
	[userName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (newid()) FOR [PaymentID]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Service] FOREIGN KEY([serviceId])
REFERENCES [dbo].[Service] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Service]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_User]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY([cartId])
REFERENCES [dbo].[Cart] ([id])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Cart]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Product] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Product]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK__Image__productId__412EB0B6] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK__Image__productId__412EB0B6]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__userId__440B1D61] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__userId__440B1D61]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__order__47DBAE45] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__order__47DBAE45]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__produ__46E78A0C] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__produ__46E78A0C]
GO
ALTER TABLE [dbo].[Otp]  WITH CHECK ADD  CONSTRAINT [FK_Otp_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Otp] CHECK CONSTRAINT [FK_Otp_User]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_User]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK__Product__categor__3E52440B] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK__Product__categor__3E52440B]
GO
ALTER DATABASE [MRC] SET  READ_WRITE 
GO
