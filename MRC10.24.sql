USE [master]
GO
/****** Object:  Database [MRC]    Script Date: 10/24/2024 3:57:23 PM ******/
CREATE DATABASE [MRC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MRC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.QHUY\MSSQL\DATA\MRC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MRC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.QHUY\MSSQL\DATA\MRC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MRC] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MRC].[dbo].[sp_fulltext_database] @action = 'enable'
end
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
ALTER DATABASE [MRC] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MRC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MRC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MRC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MRC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MRC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MRC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MRC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MRC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MRC] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MRC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MRC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MRC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MRC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MRC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MRC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MRC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MRC] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MRC] SET  MULTI_USER 
GO
ALTER DATABASE [MRC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MRC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MRC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MRC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MRC] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MRC] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MRC', N'ON'
GO
ALTER DATABASE [MRC] SET QUERY_STORE = ON
GO
ALTER DATABASE [MRC] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MRC]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10/24/2024 3:57:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NULL,
	[paymentId] [uniqueidentifier] NOT NULL,
	[totalPrice] [decimal](18, 2) NULL,
	[status] [varchar](50) NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
 CONSTRAINT [PK__Order__3213E83F1F39A9ED] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Otp]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/24/2024 3:57:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [uniqueidentifier] NOT NULL,
	[productName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[quantity] [int] NOT NULL,
	[status] [varchar](50) NOT NULL,
	[categoryId] [uniqueidentifier] NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[price] [decimal](18, 0) NULL,
 CONSTRAINT [PK__Product__3213E83F30DB6A16] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 10/24/2024 3:57:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/24/2024 3:57:23 PM ******/
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
 CONSTRAINT [PK__User__3213E83F97239305] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'090d3bb9-c95e-4f15-a509-5e2702395a5e', N'Cafe', CAST(N'2024-08-16T23:00:28.130' AS DateTime), CAST(N'2024-08-16T23:00:28.130' AS DateTime), NULL)
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'1a15bf30-af44-43c8-a6c9-a4f86af9b7a6', N'Trà', CAST(N'2024-08-20T14:07:06.227' AS DateTime), CAST(N'2024-08-20T14:07:06.230' AS DateTime), N'Available')
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'20c51703-3f17-4e8b-a590-74fd83e87ae9', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F9533e56e-05db-4ad2-803b-37ab6dbe578e_Lee%20Men%27s%20Relaxed%20Jeans.jpg?alt=media', N'4999d04b-c30d-488d-b5dd-d5d625e0a838', CAST(N'2024-08-17T01:45:40.997' AS DateTime), CAST(N'2024-08-17T01:45:40.997' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8bc10672-cb98-427f-8146-80fd930948d7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ffa05365d-c5f5-4d67-8388-69c6d89efb1d_Mountain%20Hardwear%20Insulated%20Jacke.jpg?alt=media', N'4999d04b-c30d-488d-b5dd-d5d625e0a838', CAST(N'2024-08-17T01:45:40.997' AS DateTime), CAST(N'2024-08-17T01:45:40.997' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'95d0dc2f-3b5d-485b-8ece-9fb0b96fb373', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F8b6c58dd-fbe1-4b3c-a78a-2edf9695601e_Fila%20Sports%20Hoodie.jpg?alt=media', N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', CAST(N'2024-08-17T01:32:44.430' AS DateTime), CAST(N'2024-08-17T01:32:44.430' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'27d63930-617a-461e-a9bc-a75962c419f2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa41024bf-e0c5-4a7e-a0c4-116a28ca0a1a_Mountain%20Hardwear%20Insulated%20Jacke.jpg?alt=media', N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', CAST(N'2024-08-17T01:32:44.430' AS DateTime), CAST(N'2024-08-17T01:32:44.430' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'b917e143-4347-46aa-9919-b2c3e3c175ae', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F7af926f5-98a0-4122-a648-f2b90eee562c_hong-tra-la-gi-cong-dung-cua-hong-tra-voi-suc-kho-3.jpg?alt=media', N'cc8d1ba9-103a-4469-b248-9604178ff639', CAST(N'2024-08-20T14:07:21.803' AS DateTime), CAST(N'2024-08-20T14:07:21.803' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'82428415-6b17-4538-b1f2-b42e4c8765ef', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F375169f3-3a3d-40fa-b0a9-087d6994fd71_Zara%20Wrap%20Dress.jpg?alt=media', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', CAST(N'2024-08-20T11:21:11.690' AS DateTime), CAST(N'2024-08-20T11:21:11.700' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'9febb893-8a66-445e-8810-cf261a3bc972', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb8553d90-5c66-402b-b7dd-89df5dce066c_Zara%20Wrap%20Dress.jpg?alt=media', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', CAST(N'2024-08-20T11:21:14.317' AS DateTime), CAST(N'2024-08-20T11:21:14.327' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'ab8d937e-4c24-4c14-a5eb-e27da324787f', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F483cd35f-5f0c-4985-a0fd-8bb4ec59f097_hong-tra-la-gi.jpg?alt=media', N'cc8d1ba9-103a-4469-b248-9604178ff639', CAST(N'2024-08-20T14:07:21.803' AS DateTime), CAST(N'2024-08-20T14:07:21.803' AS DateTime))
GO
INSERT [dbo].[Order] ([id], [userId], [paymentId], [totalPrice], [status], [insDate], [upDate]) VALUES (N'4858dbf3-3aaa-4505-851b-1a6427e7f16a', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'cfc29ea4-e90e-40a7-b2b0-5617f125b252', CAST(15000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T13:42:18.947' AS DateTime), NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [paymentId], [totalPrice], [status], [insDate], [upDate]) VALUES (N'066fcba6-b5d2-4b18-8e08-4d62327a4900', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'd4d36859-52ee-427b-a4ff-87ac8cbe0db2', CAST(630015.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T13:05:43.097' AS DateTime), NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [paymentId], [totalPrice], [status], [insDate], [upDate]) VALUES (N'902f82cb-c8bc-4386-96e4-5df2b545f33b', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'32586957-6871-4971-b94a-a93c02e52a79', CAST(715045.00 AS Decimal(18, 2)), N'PENDING', CAST(N'2024-09-22T17:49:55.817' AS DateTime), NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [paymentId], [totalPrice], [status], [insDate], [upDate]) VALUES (N'9fe5f6a7-2f48-4551-8373-68a9d5a64ad0', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'cfc29ea4-e90e-40a7-b2b0-5617f125b252', CAST(15000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T14:07:32.517' AS DateTime), NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [paymentId], [totalPrice], [status], [insDate], [upDate]) VALUES (N'b4f23e0a-8de2-4095-90c1-7b20d179392e', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'd4d36859-52ee-427b-a4ff-87ac8cbe0db2', CAST(0.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-22T21:37:22.480' AS DateTime), NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [paymentId], [totalPrice], [status], [insDate], [upDate]) VALUES (N'6415bf4d-ab4c-4505-b7cb-95c66169d255', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'e2e364c2-c7bc-48e0-93e0-ee7cc69cc531', CAST(700045.00 AS Decimal(18, 2)), N'PENDING', CAST(N'2024-09-22T16:16:47.510' AS DateTime), NULL)
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
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'cd3a6be2-dbf1-4d5c-8c3a-5cddae96733b', N'tra ngon', N'ngon vch', 11, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:44:54.313' AS DateTime), CAST(N'2024-08-18T14:44:17.853' AS DateTime), CAST(110000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'string', N'string', 7, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-16T23:26:08.877' AS DateTime), CAST(N'2024-08-16T23:26:08.880' AS DateTime), CAST(200000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b575b6fa-9183-4ed7-a373-762073e58e35', N'tra xanh', N'hoi bi ngon', 11, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-20T11:13:53.583' AS DateTime), CAST(N'2024-08-20T11:13:53.583' AS DateTime), CAST(150000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'cc8d1ba9-103a-4469-b248-9604178ff639', N'Trà Sữa', N'Trà Sữa', 7, N'Available', N'1a15bf30-af44-43c8-a6c9-a4f86af9b7a6', CAST(N'2024-08-20T14:07:19.110' AS DateTime), CAST(N'2024-08-20T14:07:19.110' AS DateTime), CAST(15 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', N'huy', N'ngon', 10, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:32:42.873' AS DateTime), CAST(N'2024-08-18T01:22:23.527' AS DateTime), CAST(11011 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4999d04b-c30d-488d-b5dd-d5d625e0a838', N'cafe hat', N'ngon vch', 11, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:45:40.077' AS DateTime), CAST(N'2024-08-17T01:45:40.077' AS DateTime), NULL)
GO
INSERT [dbo].[Product] ([id], [productName], [description], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'tran nguyen', N'ngon ngon', 7, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-20T11:19:51.837' AS DateTime), CAST(N'2024-08-20T11:19:51.847' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Service] ([id], [serviceName], [description], [price], [duration], [status], [insDate], [upDate]) VALUES (N'e117cd04-55ee-4ec8-86e1-72e9032ab992', N'WORKSHOP', NULL, CAST(0.00 AS Decimal(10, 2)), 0, N'Available', CAST(N'2024-10-24T15:08:07.817' AS DateTime), CAST(N'2024-10-24T15:08:07.817' AS DateTime))
GO
INSERT [dbo].[Service] ([id], [serviceName], [description], [price], [duration], [status], [insDate], [upDate]) VALUES (N'9884a9d2-aa1e-42dc-a782-7f4647506966', N'FACTORY DESIGN', NULL, CAST(0.00 AS Decimal(10, 2)), 0, N'Available', CAST(N'2024-10-24T15:08:25.617' AS DateTime), CAST(N'2024-10-24T15:08:25.617' AS DateTime))
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'5b9c3699-d94a-44ae-9e79-00004bb96f5f', N'tranquoccuong0179', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'tranquoccuong0179@gmail.com', N'Cu?ng Tr?n', N'Available', NULL, N'Customer', CAST(N'2024-08-30T13:26:36.680' AS DateTime), CAST(N'2024-08-30T13:26:36.680' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'e5a971ba-fd77-4b8e-92fe-1c7737b5ac14', N'bangpham', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'bangphamtpst@gmail.com', N'Ph?m Trúc Bang', N'Unavailable', N'Female', N'Customer', CAST(N'2024-08-29T13:09:32.533' AS DateTime), CAST(N'2024-08-29T13:09:32.533' AS DateTime), N'0336607452')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'b7e29dcf-64d3-4742-ab42-2683158aeb34', N'bangpham123', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'phamtrucbang2008ptb@gmail.com', N'Ph?m Trúc Bang', N'Unavailable', N'Female', N'Customer', CAST(N'2024-08-29T13:14:05.187' AS DateTime), CAST(N'2024-08-29T13:14:05.187' AS DateTime), N'0336607453')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'b108281e-6b43-48b2-a781-49e64d0bb7ab', N'manager1', N'OA+XcdLfhWbOK9W47XcrC7dP1kV/uAOrLSZ8OU2Jx1A=', N'manager1@gmail.com', N'string', NULL, N'Male', N'Manager', CAST(N'2024-08-16T21:48:35.557' AS DateTime), CAST(N'2024-08-16T21:48:35.560' AS DateTime), N'0901234568')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'9bb61de5-af7c-4afa-9a05-4d9578475ed0', N'quoccuong', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'cuongtqse160059@fpt.edu.vn', N'Tr?n Qu?c Cu?ng', N'Available', N'Male', N'Customer', CAST(N'2024-08-30T01:38:33.527' AS DateTime), CAST(N'2024-08-30T01:38:33.527' AS DateTime), N'0363919179')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'e4c9c30b-c2df-46d9-9a88-5600e278695b', N'vhuy16', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Admin', CAST(N'2024-08-14T21:37:48.583' AS DateTime), CAST(N'2024-08-14T21:37:48.583' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'59d09a3d-38a6-4133-a4c5-80d07b2bed27', N'vhuy10', N'i7DPbrmxfQ99IrRW8SElfcElTh8BZlNwR2OD6ndt9BQ=', N'voquanghuy56@gmail.com', N'string', N'Unavailable', N'Male', N'Customer', CAST(N'2024-10-11T12:17:39.580' AS DateTime), CAST(N'2024-10-11T12:17:39.580' AS DateTime), N'0901960523')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'6a6f6a03-56ee-48e5-b458-825fd9e7b6c4', N'vhuy1610', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Manager', CAST(N'2024-08-15T13:23:06.377' AS DateTime), CAST(N'2024-08-15T13:23:06.377' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'5aa6b11c-771f-458b-96f2-93c8c593a838', N'vhuy12', N'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=', N'voquanghuy506@gmail.com', N'string', N'Available', N'Male', N'Customer', CAST(N'2024-10-17T22:35:15.480' AS DateTime), CAST(N'2024-10-17T22:38:20.157' AS DateTime), N'0901960501')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'ce419771-09d5-4a6d-8021-a04427dd5d81', N'vhuy16102002', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Customer', CAST(N'2024-08-15T13:25:03.243' AS DateTime), CAST(N'2024-08-15T13:25:03.243' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'f6cdf58c-7388-4db4-be10-ad3e6cf79c9c', N'tangoccu', N'86I39YKhpaYNH+32p/XuNbuXHWQz1mWCKeq0r3fhB/Y=', N'ngocan2003krp@gmail.com', N'T? Ng?c Cu', N'Unavailable', N'Male', N'Customer', CAST(N'2024-09-05T11:19:11.713' AS DateTime), CAST(N'2024-09-05T11:19:11.713' AS DateTime), N'0356638270')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'4260c50a-ff1e-47f2-a312-bfa6df961cd7', N'tangoccu1', N'O6i3JEdRGBho4KYgWSnW4a/qMkyOLntDVR8GYBBnFvE=', N'antnqe170035@fpt.edu.vn', N'T? Ng?c Cu', N'Available', N'Male', N'Customer', CAST(N'2024-09-05T11:21:18.547' AS DateTime), CAST(N'2024-09-05T11:21:18.547' AS DateTime), N'0356638271')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'4487d6fa-9ce6-43f7-87b5-c3810702de12', N'tranquoccuong13072002', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'tranquoccuong13072002@gmail.com', N'Cu?ng Tr?n', N'Available', NULL, N'Customer', CAST(N'2024-08-20T13:56:20.607' AS DateTime), CAST(N'2024-08-20T13:56:20.607' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'a3d3fba5-f0c2-4716-8a9c-db1754999513', N'duybpz', N'PPPAC7TVWzdd/5xnKBxSi5gGz0o1NfOGkpQwt6AOVTI=', N'duybpz@gmail.com', N'V? Thanh Duy', N'Available', N'Male', N'Customer', CAST(N'2024-08-30T13:02:00.007' AS DateTime), CAST(N'2024-08-30T13:02:00.010' AS DateTime), N'0123456789')
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber]) VALUES (N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'huyvqse160031', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'huyvqse16001@fpt.edu.vn', N'Vo Quang Huy (K16_HCM)', N'Available', NULL, N'Customer', CAST(N'2024-09-18T22:52:05.853' AS DateTime), CAST(N'2024-09-18T22:52:05.853' AS DateTime), NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_index_0]    Script Date: 10/24/2024 3:57:23 PM ******/
CREATE NONCLUSTERED INDEX [User_index_0] ON [dbo].[User]
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([paymentId])
REFERENCES [dbo].[Payment] ([PaymentID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
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
USE [master]
GO
ALTER DATABASE [MRC] SET  READ_WRITE 
GO
