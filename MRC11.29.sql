USE [master]
GO
/****** Object:  Database [MRC]    Script Date: 11/29/2024 7:56:07 PM ******/
CREATE DATABASE [MRC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MRC', FILENAME = N'C:\Program Files (x86)\Plesk\Databases\MSSQL\MSSQL16.MSSQLSERVER2022\MSSQL\DATA\MRC.mdf' , SIZE = 8192KB , MAXSIZE = 6144000KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MRC_log', FILENAME = N'C:\Program Files (x86)\Plesk\Databases\MSSQL\MSSQL16.MSSQLSERVER2022\MSSQL\DATA\MRC_log.ldf' , SIZE = 8192KB , MAXSIZE = 6144000KB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [MRC] SET QUERY_STORE = ON
GO
ALTER DATABASE [MRC] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MRC]
GO
/****** Object:  User [mrcadmin]    Script Date: 11/29/2024 7:56:07 PM ******/
CREATE USER [mrcadmin] FOR LOGIN [mrcadmin] WITH DEFAULT_SCHEMA=[mrcadmin]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [mrcadmin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [mrcadmin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [mrcadmin]
GO
/****** Object:  Schema [mrcadmin]    Script Date: 11/29/2024 7:56:07 PM ******/
CREATE SCHEMA [mrcadmin]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 11/29/2024 7:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[id] [uniqueidentifier] NOT NULL,
	[serviceId] [uniqueidentifier] NULL,
	[bookingDate] [datetime] NULL,
	[status] [varchar](255) NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[content] [nvarchar](max) NOT NULL,
	[tile] [nvarchar](max) NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[CartItem]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[Image]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 11/29/2024 7:56:07 PM ******/
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
	[address] [nvarchar](max) NULL,
 CONSTRAINT [PK__Order__3213E83F1F39A9ED] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[Otp]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 11/29/2024 7:56:07 PM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 11/29/2024 7:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [uniqueidentifier] NOT NULL,
	[productName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[message] [nvarchar](max) NULL,
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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/29/2024 7:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[id] [uniqueidentifier] NOT NULL,
	[serviceName] [nvarchar](255) NOT NULL,
	[status] [varchar](255) NOT NULL,
	[insDate] [datetime] NULL,
	[upDate] [datetime] NULL,
	[deleteAt] [datetime] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/29/2024 7:56:07 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Booking] ([id], [serviceId], [bookingDate], [status], [insDate], [upDate], [content], [tile]) VALUES (N'8f46222a-6072-4e5a-8409-972f0feb5716', N'8a179dff-a59c-4452-96ca-2d2f1eaecf44', CAST(N'2024-12-17T08:00:00.000' AS DateTime), N'Available', CAST(N'2024-11-29T00:45:54.840' AS DateTime), CAST(N'2024-11-29T00:45:54.840' AS DateTime), N'<p><a href="https://thecoffeehouse.com.vn/workshop-nghe-ca-phe-ke-chuyen-chapter-1-cupping-for-beginners/" rel="noopener noreferrer" target="_blank" style="color: rgba(25, 25, 25, 1)"><img src="https://thecoffeehouse.com.vn/wp-content/uploads/2019/05/4b842d09611a8244db0b_grande.jpg"></a></p><h6><a href="https://thecoffeehouse.com.vn/category/pass-events/" rel="noopener noreferrer" target="_blank" style="color: rgba(119, 119, 119, 1)"><strong>Pass Events</strong></a></h6><p><strong>WORKSHOP THƯỞNG THỨC CÀ PHÊ VÀ GIỚI THIỆU KỸ NĂNG CẢM NHẬN HƯƠNG VỊ</strong></p><p><strong>[Sự kiện đã qua]</strong></p><p>Cà phê hiện nay là một món nước uống quen thuộc được dùng mỗi ngày, nhưng bạn có biết Việt Nam xuất khẩu cà phê thứ hai trên thế giới và riêng cà phê Robusta thì đứng thứ nhất. Và liệu rằng bạn có biết mỗi tách cà phê lại có một mùi hương rất khác nhau mà bạn sẽ tự cảm nhận được.</p><p><img src="https://file.hstatic.net/1000059704/file/c0ebb4be4be01daadfea24ed9efa19df_grande.jpg"></p><p>Với mong muốn chia sẻ và giúp mọi người hiểu hơn về mùi hương The Coffee House để tổ chức buổi Workshop về thưởng thức cà phê và giới thiệu kỹ năng cảm nhận hương vị của mỗi người.</p><p><img src="https://thecoffeehouse.com.vn/wp-content/uploads/2019/05/4b842d09611a8244db0b_grande.jpg"></p><p>Với mong muốn chia sẽ, học hỏi và trao đổi kinh nghiệm về cà phê nói chung cũng như pha chế cà phê nói riêng, tham gia buổi Workshop còn có sự góp mặt của các chú nông dân trồng cà phê và các bạn Barista đến từ The Coffee House.</p><p><br></p>', N'fixcung')
GO
INSERT [dbo].[Booking] ([id], [serviceId], [bookingDate], [status], [insDate], [upDate], [content], [tile]) VALUES (N'5689b90e-1011-4622-b426-98440cff40bc', N'6fb695e2-8464-4dd7-b0c2-c962c5ed3a7e', CAST(N'2024-11-13T17:00:00.000' AS DateTime), N'Available', CAST(N'2024-11-28T21:57:26.567' AS DateTime), CAST(N'2024-11-28T21:57:26.567' AS DateTime), N'<p>213123</p>', NULL)
GO
INSERT [dbo].[Booking] ([id], [serviceId], [bookingDate], [status], [insDate], [upDate], [content], [tile]) VALUES (N'4c15df40-56c7-4423-a37f-a00eb4e8ec87', N'6fb695e2-8464-4dd7-b0c2-c962c5ed3a7e', CAST(N'2024-12-15T14:15:00.000' AS DateTime), N'Available', CAST(N'2024-11-28T21:19:00.887' AS DateTime), CAST(N'2024-11-28T21:19:00.887' AS DateTime), N'content', NULL)
GO
INSERT [dbo].[Booking] ([id], [serviceId], [bookingDate], [status], [insDate], [upDate], [content], [tile]) VALUES (N'9fee16f1-2ad2-46b9-8a5e-ca718436756e', N'6fb695e2-8464-4dd7-b0c2-c962c5ed3a7e', CAST(N'2024-11-28T16:05:53.717' AS DateTime), N'Available', CAST(N'2024-11-28T23:06:45.567' AS DateTime), CAST(N'2024-11-28T23:06:45.567' AS DateTime), N'cai nai moi tao', N'mot cai tile that hayyy')
GO
INSERT [dbo].[Booking] ([id], [serviceId], [bookingDate], [status], [insDate], [upDate], [content], [tile]) VALUES (N'b1c01809-701b-450b-8273-d48e82c389ee', N'6fb695e2-8464-4dd7-b0c2-c962c5ed3a7e', CAST(N'2024-11-28T16:13:36.683' AS DateTime), N'Available', CAST(N'2024-11-28T23:14:20.077' AS DateTime), CAST(N'2024-11-28T23:14:20.077' AS DateTime), N'cai nay hoi bi xin', N'Cai nay tao tren server ne')
GO
INSERT [dbo].[Booking] ([id], [serviceId], [bookingDate], [status], [insDate], [upDate], [content], [tile]) VALUES (N'52684cb1-7608-4c86-b73f-d90b4bcb17fe', N'6fb695e2-8464-4dd7-b0c2-c962c5ed3a7e', NULL, N'Cancelled', CAST(N'2024-11-28T21:52:34.753' AS DateTime), CAST(N'2024-11-29T00:38:05.267' AS DateTime), N'S?n ph?m cà phê rang xay', NULL)
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
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'eac00b10-fdef-4903-93d8-531db9f79a21', N'37296d41-6271-44fa-8a8e-0300197be8cd', CAST(N'2024-11-22T11:07:10.577' AS DateTime), CAST(N'2024-11-22T11:07:10.577' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'35eb23bf-d548-4876-b119-7358365c4805', N'e5a971ba-fd77-4b8e-92fe-1c7737b5ac14', CAST(N'2024-08-29T13:09:32.657' AS DateTime), CAST(N'2024-08-29T13:09:32.657' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'6acc99bc-d925-4f1e-bd75-8ad8f150a048', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(N'2024-08-30T13:02:00.163' AS DateTime), CAST(N'2024-08-30T13:02:00.163' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'8c393bc8-5593-4cf7-a3f0-990c8b481191', N'5b9c3699-d94a-44ae-9e79-00004bb96f5f', CAST(N'2024-08-30T13:26:36.800' AS DateTime), CAST(N'2024-08-30T13:26:36.800' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'dbfe55ed-7ecb-40ae-aadf-cd69d14f2779', N'd86e36be-9050-49db-a1f9-1c836cd672b0', CAST(N'2024-11-21T19:40:51.030' AS DateTime), CAST(N'2024-11-21T19:40:51.030' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'df47d91a-1a77-41a6-b136-e9fab7893798', N'9bb61de5-af7c-4afa-9a05-4d9578475ed0', CAST(N'2024-08-30T01:38:33.610' AS DateTime), CAST(N'2024-08-30T01:38:33.610' AS DateTime), N'Available')
GO
INSERT [dbo].[Cart] ([id], [userId], [insDate], [upDate], [status]) VALUES (N'fa8d4f0d-6bd1-4c7c-a235-eb22f463c9a4', N'3e7bea9b-0c71-4f87-b445-2cb6584dd531', CAST(N'2024-11-22T09:44:45.003' AS DateTime), CAST(N'2024-11-22T09:44:45.003' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'85ba54c2-d81e-4f7e-b492-133ec2fb9082', N'6912dd86-83f1-4360-b73c-0277277279c4', N'883bd921-351c-4727-ab42-e412d965d741', 1, CAST(N'2024-11-22T10:45:18.767' AS DateTime), CAST(N'2024-11-22T10:45:18.767' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'2b0a3580-97d5-4313-834e-213cc2701e93', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'03af42ee-4512-4bac-add5-a43b29e0278f', 3, CAST(N'2024-11-29T12:15:41.850' AS DateTime), CAST(N'2024-11-29T12:15:41.850' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'd3cadd30-be68-4624-a5be-286fba909c41', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'83c97871-7fda-49a6-847a-1c26d5a86e88', 5, CAST(N'2024-11-29T16:12:05.343' AS DateTime), CAST(N'2024-11-29T16:12:05.343' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'92d8f0a2-1051-4f50-b925-331087048a7c', N'6912dd86-83f1-4360-b73c-0277277279c4', N'36df0437-f654-4ae2-8f11-ceef3c7d8bc8', 1, CAST(N'2024-11-22T10:44:27.390' AS DateTime), CAST(N'2024-11-22T10:44:27.390' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'04d31ddf-808f-4889-8b1b-34ebbf18b534', N'6acc99bc-d925-4f1e-bd75-8ad8f150a048', N'081de215-edd7-4af2-8bcb-c0cc6898a236', 1, CAST(N'2024-11-29T16:01:24.930' AS DateTime), CAST(N'2024-11-29T16:01:24.930' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'c76c79a1-3419-4b55-8812-733fc1c5feed', N'6912dd86-83f1-4360-b73c-0277277279c4', N'd0c5bd98-d1df-43af-9c5f-51b0a9c10fcf', 2, CAST(N'2024-11-23T22:58:40.833' AS DateTime), CAST(N'2024-11-23T22:58:40.833' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'761bd02f-5745-486b-899b-89f2d0fb4f3c', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'737c0fbd-0f8f-4fce-8aa9-1255e4736ee7', 9, CAST(N'2024-11-29T12:15:56.220' AS DateTime), CAST(N'2024-11-29T16:40:12.373' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'b0056c89-9479-4e7c-a9d3-979157189f02', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'dc87c91f-3085-4b35-b4ce-0fcecc30f017', 18, CAST(N'2024-11-29T12:15:48.597' AS DateTime), CAST(N'2024-11-29T16:38:04.220' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'f7df164d-7930-4a31-96c4-99e4808542c3', N'6acc99bc-d925-4f1e-bd75-8ad8f150a048', N'36df0437-f654-4ae2-8f11-ceef3c7d8bc8', 11, CAST(N'2024-11-22T00:16:26.427' AS DateTime), CAST(N'2024-11-22T02:19:14.970' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'8bf096e0-e636-4762-9557-abc96f45bd12', N'6912dd86-83f1-4360-b73c-0277277279c4', N'83c97871-7fda-49a6-847a-1c26d5a86e88', 1, CAST(N'2024-11-22T10:56:25.987' AS DateTime), CAST(N'2024-11-22T10:56:25.987' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'7cee656b-4603-46e4-a8ed-b90b540f1f72', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'97263641-0c77-4b94-b45e-e4d13989acf4', 3, CAST(N'2024-11-29T16:14:38.067' AS DateTime), CAST(N'2024-11-29T16:37:35.853' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'283f46b7-bfaa-4dbd-b20f-c3473ba2997e', N'b27e3f91-9026-4298-9eaa-39bfb364e595', N'062e5b9a-d3e4-4903-ae55-4f96d7278cdd', 5, CAST(N'2024-11-29T16:26:26.740' AS DateTime), CAST(N'2024-11-29T16:34:08.850' AS DateTime), N'Available')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'265690c1-8244-42ed-8d6a-e064229f924b', N'6acc99bc-d925-4f1e-bd75-8ad8f150a048', N'0c36c4d4-0ccc-4143-b88f-e2025f7e0725', 5, CAST(N'2024-11-22T00:18:33.843' AS DateTime), CAST(N'2024-11-22T02:19:16.093' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[CartItem] ([id], [cartId], [productId], [quantity], [insDate], [upDate], [status]) VALUES (N'a5b5dc50-65b8-4d3c-bab8-e7ac4acae9aa', N'6912dd86-83f1-4360-b73c-0277277279c4', N'ac19a183-d5b0-4c01-8460-eb7cc4780822', 2, CAST(N'2024-11-23T22:58:15.840' AS DateTime), CAST(N'2024-11-23T22:58:15.840' AS DateTime), N'Available')
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
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'53da0621-bd30-49c1-b732-32e1a101c3ed', N'123123', CAST(N'2024-11-28T14:42:44.013' AS DateTime), CAST(N'2024-11-28T14:42:44.013' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'051a00ba-e7ef-4edb-a9c0-334527d36aff', N'tạo sau nữa', CAST(N'2024-11-23T23:25:28.107' AS DateTime), CAST(N'2024-11-23T23:25:28.107' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'fecd55e7-c87a-44a0-89da-38b6e6f7ae14', N'123123123123123', CAST(N'2024-11-19T21:14:20.427' AS DateTime), CAST(N'2024-11-19T21:14:22.067' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'2bb0cf5e-87bb-4d02-8e28-3c7a44964514', N'2141231', CAST(N'2024-11-19T22:03:45.753' AS DateTime), CAST(N'2024-11-19T22:43:22.663' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'18edcbe1-8d6d-407c-91b5-3e4e52baa056', N'Bánh', CAST(N'2024-11-19T22:44:11.820' AS DateTime), CAST(N'2024-11-20T01:09:27.023' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'623620de-9da5-4e12-93b0-3f2b2b54bf3a', N'123123331231233', CAST(N'2024-11-28T14:42:53.217' AS DateTime), CAST(N'2024-11-28T16:14:42.990' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'907bab88-6b53-496e-9f61-433f8cb4c1ef', N'tra an than2', CAST(N'2024-11-19T22:22:19.283' AS DateTime), CAST(N'2024-11-19T22:43:22.980' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'bae385df-433c-4438-b53a-4e56c84335ae', N'Rau Củ', CAST(N'2024-11-22T10:54:32.230' AS DateTime), CAST(N'2024-11-22T10:54:32.230' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'090d3bb9-c95e-4f15-a509-5e2702395a5e', N'Cafe', CAST(N'2024-08-16T23:00:28.130' AS DateTime), CAST(N'2024-08-16T23:00:28.130' AS DateTime), NULL)
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'005ffd05-6901-4f5a-8d5f-6212bffccf5e', N'123123123123123123', CAST(N'2024-11-19T21:20:32.583' AS DateTime), CAST(N'2024-11-19T21:20:40.790' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'650955c7-dd33-413f-ad69-6544555b4897', N'Cà phê', CAST(N'2024-11-20T15:27:39.953' AS DateTime), CAST(N'2024-11-20T15:27:39.953' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'7b4032ac-70dc-4414-a01b-657d28bb6edf', N'12312333123123', CAST(N'2024-11-28T14:42:49.317' AS DateTime), CAST(N'2024-11-28T16:15:19.833' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'b66237a4-6785-4a7d-9cfb-6800a123eb85', N'12312333', CAST(N'2024-11-28T14:42:47.020' AS DateTime), CAST(N'2024-11-28T14:42:47.020' AS DateTime), N'Available')
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
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'd5f62382-abf4-4775-97d0-98f729080f4a', N'DMM th Huy', CAST(N'2024-11-23T23:25:24.447' AS DateTime), CAST(N'2024-11-28T21:05:44.397' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'f414fe97-c731-498f-9f8f-9fea1934df2e', N'123123', CAST(N'2024-11-19T22:03:01.240' AS DateTime), CAST(N'2024-11-19T22:46:04.140' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'1a15bf30-af44-43c8-a6c9-a4f86af9b7a6', N'Trà', CAST(N'2024-08-20T14:07:06.227' AS DateTime), CAST(N'2024-11-19T21:28:34.163' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'4a8169ce-a99c-46b6-841f-a83b24fbc7e1', N'tạo đầu tiên', CAST(N'2024-11-23T23:25:16.747' AS DateTime), CAST(N'2024-11-23T23:25:16.750' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'6064f2be-36e4-47d7-85fb-aa00b438602c', N'1231233312312233', CAST(N'2024-11-28T14:43:10.180' AS DateTime), CAST(N'2024-11-28T14:43:10.180' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'f4050b6c-c1a6-4d25-ae07-b3d821e04dad', N'Thảo dược', CAST(N'2024-11-19T20:33:03.303' AS DateTime), CAST(N'2024-11-19T20:42:33.530' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'33a69546-25de-4cf4-899f-bbc2196a51b7', N'D123', CAST(N'2024-11-20T01:09:19.570' AS DateTime), CAST(N'2024-11-20T01:09:30.913' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'e8b3fd1f-47bf-4741-a8cf-bf3611cbff83', N'123123123123123', CAST(N'2024-11-19T21:20:31.587' AS DateTime), CAST(N'2024-11-19T21:20:38.233' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'c1d5e8bb-f384-4c4f-9fe4-ca0b9e9f97d7', N'string', CAST(N'2024-11-28T15:32:06.657' AS DateTime), CAST(N'2024-11-28T16:35:07.300' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'78564bf0-e7b5-4f22-8af2-dd0360fdbae3', N'123123', CAST(N'2024-11-19T21:20:28.070' AS DateTime), CAST(N'2024-11-19T21:20:39.217' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'38d28eec-b8f4-4fc1-9612-de1b8d51ef36', N'Thuốc', CAST(N'2024-11-20T15:28:08.300' AS DateTime), CAST(N'2024-11-20T15:28:08.300' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', N'Trà', CAST(N'2024-11-20T01:09:42.870' AS DateTime), CAST(N'2024-11-20T01:09:42.870' AS DateTime), N'Available')
GO
INSERT [dbo].[Category] ([id], [categoryName], [insDate], [upDate], [Status]) VALUES (N'610c70b9-d473-46ac-bfa9-f87bab739e5a', N'trà', CAST(N'2024-11-19T22:44:05.707' AS DateTime), CAST(N'2024-11-20T01:09:34.483' AS DateTime), N'Unavailable')
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'0c042dcf-be36-415c-8152-05d6842faa55', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F6cd0450c-8334-4cd8-9103-695c320515be_close-up-cup-black-coffee%201.png?alt=media', N'883bd921-351c-4727-ab42-e412d965d741', CAST(N'2024-11-21T22:32:27.913' AS DateTime), CAST(N'2024-11-21T22:32:27.913' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'eabd5c27-e962-495d-a8e8-0c7a05b4241f', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F736108cf-4654-415e-ab5c-fced706f7c81_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'847592c9-aa01-47db-9e2c-86397d29b2d0', CAST(N'2024-11-21T15:18:55.590' AS DateTime), CAST(N'2024-11-21T15:18:55.590' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'9073c377-36aa-4e14-a482-12dc382e3a84', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F38e7eae8-85de-4d3e-9bca-051c4979347d_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'bc14c4e3-665d-40c1-84e0-26b8de1dc36e', CAST(N'2024-11-20T21:25:28.213' AS DateTime), CAST(N'2024-11-20T21:25:28.213' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'529a180b-932a-453c-af70-1be6cb23661e', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5a3b5960-f738-404e-97e3-3f873f4314ad_e367bdfe-d0ac-49c4-ba18-c1284464a6f2.jpg?alt=media', N'062e5b9a-d3e4-4903-ae55-4f96d7278cdd', CAST(N'2024-11-29T16:25:50.907' AS DateTime), CAST(N'2024-11-29T16:25:50.907' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'c50e49c3-b6b7-420f-b09c-1c5b9622b49a', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F6c21c505-2f39-4df5-a086-0e5881d77ac4_hinh-ly-cafe_3.jpg?alt=media', N'97263641-0c77-4b94-b45e-e4d13989acf4', CAST(N'2024-11-22T10:24:04.163' AS DateTime), CAST(N'2024-11-22T10:24:04.163' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8648a759-1bca-498f-9751-20642f303db9', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5091bb50-a7f7-4307-90f7-9e7d561c6e7b_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'061b0116-cf04-45fb-a818-069e00a89138', CAST(N'2024-11-20T20:35:14.203' AS DateTime), CAST(N'2024-11-20T20:35:14.213' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'71dc9b63-fdf1-4e2d-9ef1-212cf9c6c5d2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5146b3e4-1384-4822-bb4a-fa884ab81698_icons8-question-48.png?alt=media', N'062e5b9a-d3e4-4903-ae55-4f96d7278cdd', CAST(N'2024-11-29T16:25:50.907' AS DateTime), CAST(N'2024-11-29T16:25:50.907' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8b6e1630-803a-4360-9887-2728a9744c12', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa0b4192f-b044-439b-b294-68ee832a4fe6_mrchi-coffee-1.jpg?alt=media', N'85fe57ed-8ebb-4cc1-b2f8-3777b3d18ca9', CAST(N'2024-11-21T00:54:43.330' AS DateTime), CAST(N'2024-11-21T00:54:43.330' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'5a7d052e-3b6c-4748-8989-277ed1a3bd50', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F177d9ddc-00a6-4b5d-8b18-56ebc59aa72b_certification1.png?alt=media', N'36df0437-f654-4ae2-8f11-ceef3c7d8bc8', CAST(N'2024-11-21T22:31:44.363' AS DateTime), CAST(N'2024-11-21T22:31:44.363' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'609600d8-6b9e-44e8-986f-27b12f628733', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F77f2f474-146f-4429-93bf-77376dfd7d1d_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.17.03.png?alt=media', N'da95aeca-217d-4e1a-a667-158eaf0deb5f', CAST(N'2024-11-20T19:58:09.753' AS DateTime), CAST(N'2024-11-20T19:58:09.753' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'418de2e1-f1bb-4d7e-90d9-2babe8e42ec1', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F171afcaf-bb13-4e8c-acc3-f3170c8a756e_04.37bmdtFE10-Don%20de%20nghi%20phuc%20tra.doc?alt=media', N'a1f9b991-03d3-4172-bfad-c17009f411e6', CAST(N'2024-11-21T01:23:49.977' AS DateTime), CAST(N'2024-11-21T01:23:49.977' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8c175e86-fac5-409c-94d4-2bd5facfdabf', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F37e499da-ad62-43a7-80e2-0f26e802c68b_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2016.06.33.png?alt=media', N'059a27a3-d1e9-457b-aff6-d94596720a2f', CAST(N'2024-11-20T19:45:44.507' AS DateTime), CAST(N'2024-11-20T19:45:44.507' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'91247dee-6cac-49cf-b70c-2bff98e44039', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb37d27cd-1c2d-45a4-9ee8-91e2982cc5a1_hinh-ly-cafe_3.jpg?alt=media', N'753cd102-d202-4061-97d0-b52bd48d19e4', CAST(N'2024-11-22T10:59:06.303' AS DateTime), CAST(N'2024-11-22T10:59:06.303' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e29a7712-4e53-41e8-824d-37a97976b812', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fe771bd34-b5ba-4aa1-ab90-ffd567d17210_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.17.03.png?alt=media', N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', CAST(N'2024-11-20T16:42:04.053' AS DateTime), CAST(N'2024-11-20T16:42:04.053' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e10a128b-3af9-45c1-aaed-39cb28808bdf', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fe95e2474-4467-45bf-9921-5a211e2df6eb_mrchi-coffee-1.jpg?alt=media', N'bcedd3ab-b845-46a8-9d10-9351df7bddc2', CAST(N'2024-11-21T01:18:06.050' AS DateTime), CAST(N'2024-11-21T01:18:06.050' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'7558a91c-a427-42e7-9770-440c6d2c328a', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F44bd827a-c657-45f9-a6c4-59c1d1f9268e_cf4.jpeg?alt=media', N'c9dff179-4e6a-429c-886f-58180c494b75', CAST(N'2024-11-28T23:05:27.693' AS DateTime), CAST(N'2024-11-28T23:05:27.693' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e54b94c5-8360-4d52-b667-46ba14a2faa2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5095288f-0880-4137-a834-884e175881e5_player-icon.png?alt=media', N'737c0fbd-0f8f-4fce-8aa9-1255e4736ee7', CAST(N'2024-11-28T13:44:29.993' AS DateTime), CAST(N'2024-11-28T13:44:29.993' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'0d42b3a2-261a-4be7-831c-4965474821d7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F0900044d-70b8-431a-9471-4256a5608a30_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2023.43.12.png?alt=media', N'74245727-371e-4700-99c0-c0b0d56c95f3', CAST(N'2024-11-21T00:49:07.863' AS DateTime), CAST(N'2024-11-21T00:49:07.863' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'a1049d0b-86ef-4974-ac92-523de7d739e1', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F43124184-bf5b-4cbc-8bee-bce8d1e31435_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-03%20lu%CC%81c%2022.34.29.png?alt=media', N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', CAST(N'2024-11-20T16:42:04.053' AS DateTime), CAST(N'2024-11-20T16:42:04.053' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'5fc6523a-5f28-4333-8a39-59786cb84d5e', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F37bdd3b5-27eb-4c59-b6c9-280aee658e2d_hinh-ly-cafe_2.jpg?alt=media', N'd0c5bd98-d1df-43af-9c5f-51b0a9c10fcf', CAST(N'2024-11-22T10:55:57.690' AS DateTime), CAST(N'2024-11-22T10:55:57.690' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'ea6e3ab3-1f89-4495-8142-5f0eb4d53448', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fdaf9d0e9-9122-48f5-acb8-f147f8982546_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'5ce6d9e9-c0d4-412e-b857-aeab804afcda', CAST(N'2024-11-20T21:09:24.587' AS DateTime), CAST(N'2024-11-20T21:09:24.587' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'829a22d2-6bca-494a-aeae-626f8a2726db', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F4b90bf48-6a27-4716-ac7f-e5ecd7f52f76_check.png?alt=media', N'7d04ca3e-22db-4784-b0bc-f55dfab67e36', CAST(N'2024-11-21T15:06:49.810' AS DateTime), CAST(N'2024-11-21T15:06:49.810' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'ce86e4ed-2dd9-48e7-9032-631f265a0789', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ffe48db67-ae70-4074-abdd-a0fd930f5103_love-banh-pizza-hai-san.jpeg?alt=media', N'80ec4ad6-06d0-43d9-8883-1c3ed736bf7c', CAST(N'2024-11-21T14:39:03.390' AS DateTime), CAST(N'2024-11-21T14:39:03.390' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'b1551850-2a83-4df7-94de-66fcd1e8b2b7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F9f65c86e-adc2-4997-b0c1-633e06c84277_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'044f474e-0864-4b53-a310-7ae5d16e834e', CAST(N'2024-11-21T15:16:04.627' AS DateTime), CAST(N'2024-11-21T15:16:04.627' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'072f9a2e-65b7-4c7c-a1ca-699dca79493f', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb9c70f4e-fcca-4900-a13f-f87c2ce9d0f3_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'e33e79b2-9ec1-4134-b406-2002ef23e24a', CAST(N'2024-11-20T21:21:42.283' AS DateTime), CAST(N'2024-11-20T21:21:42.283' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'30d51cb2-089b-4a42-bd9e-6f3da8191708', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fd25ce0c9-371b-4175-bb47-1267c1648833_adaptive-icon.png?alt=media', N'ac19a183-d5b0-4c01-8460-eb7cc4780822', CAST(N'2024-11-23T22:26:41.743' AS DateTime), CAST(N'2024-11-23T22:26:41.743' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'f0032d7a-506f-4528-82f7-73353c53019a', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F575f84f1-5315-4269-9c88-a6721162543a_MrChi.png?alt=media', N'34cf56e5-ae8a-4cad-8436-667f5a3a6762', CAST(N'2024-11-21T02:40:59.647' AS DateTime), CAST(N'2024-11-21T02:40:59.647' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'20c51703-3f17-4e8b-a590-74fd83e87ae9', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F9533e56e-05db-4ad2-803b-37ab6dbe578e_Lee%20Men%27s%20Relaxed%20Jeans.jpg?alt=media', N'4999d04b-c30d-488d-b5dd-d5d625e0a838', CAST(N'2024-08-17T01:45:40.997' AS DateTime), CAST(N'2024-08-17T01:45:40.997' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'63c7b0f1-dae4-4478-a34b-77a62d6dc44c', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa3ee6f3f-4ecf-4002-a425-05b2e5895591_mrchi-coffee-1.jpg?alt=media', N'dc87c91f-3085-4b35-b4ce-0fcecc30f017', CAST(N'2024-11-21T00:06:49.140' AS DateTime), CAST(N'2024-11-21T00:06:49.140' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'8bc10672-cb98-427f-8146-80fd930948d7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ffa05365d-c5f5-4d67-8388-69c6d89efb1d_Mountain%20Hardwear%20Insulated%20Jacke.jpg?alt=media', N'4999d04b-c30d-488d-b5dd-d5d625e0a838', CAST(N'2024-08-17T01:45:40.997' AS DateTime), CAST(N'2024-08-17T01:45:40.997' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'016be242-8f84-4e18-b847-81249e7719c8', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fe92b4764-37a0-4d98-8f68-98360cecafb7_certification1.png?alt=media', N'0c36c4d4-0ccc-4143-b88f-e2025f7e0725', CAST(N'2024-11-21T22:31:15.530' AS DateTime), CAST(N'2024-11-21T22:31:15.530' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'1a4e200a-42fc-4f34-8f3e-818a366e1cd9', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb8cb092b-2e76-4398-b471-24aee7003ac1_KOL.jpg?alt=media', N'c6066ada-def9-47cb-a6ed-3826178eeb1e', CAST(N'2024-11-28T13:40:38.463' AS DateTime), CAST(N'2024-11-28T13:40:38.463' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'95dd924b-3665-4c79-9999-8ab56f9ac7aa', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F1010e0ec-f93c-4a58-9831-c7ca65d75a95_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'3e30c213-db9c-4e96-9897-45d1731d6084', CAST(N'2024-11-20T21:08:19.723' AS DateTime), CAST(N'2024-11-20T21:08:19.723' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'25649a98-0f9e-4b29-aad3-8ab903d0a91a', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ffb277fad-6b64-413f-a7c4-464e413b8a25_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'd48c02ee-0465-44dc-ade9-3e70b58104d6', CAST(N'2024-11-20T21:23:24.963' AS DateTime), CAST(N'2024-11-20T21:23:24.963' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'95d0dc2f-3b5d-485b-8ece-9fb0b96fb373', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F8b6c58dd-fbe1-4b3c-a78a-2edf9695601e_Fila%20Sports%20Hoodie.jpg?alt=media', N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', CAST(N'2024-08-17T01:32:44.430' AS DateTime), CAST(N'2024-08-17T01:32:44.430' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'3ec862ba-bb67-44f0-8f1e-a228ca720a32', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa2c4b410-97c8-467d-b80f-be3ee73055a3_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'3b22ec0a-0633-4a42-bc3d-a04525b4eb52', CAST(N'2024-11-21T15:20:02.410' AS DateTime), CAST(N'2024-11-21T15:20:02.410' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'6b4b7b8d-dc55-462c-8733-a7425c692705', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Febc17ff7-d968-4278-94c9-33a21913c212_check.png?alt=media', N'e35e3cc2-51ad-4657-afe1-e0038d50845e', CAST(N'2024-11-21T15:06:22.060' AS DateTime), CAST(N'2024-11-21T15:06:22.060' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'27d63930-617a-461e-a9bc-a75962c419f2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa41024bf-e0c5-4a7e-a0c4-116a28ca0a1a_Mountain%20Hardwear%20Insulated%20Jacke.jpg?alt=media', N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', CAST(N'2024-08-17T01:32:44.430' AS DateTime), CAST(N'2024-08-17T01:32:44.430' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'c0563950-0b48-428d-81b4-b3c9cbc2d7a4', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fc482e146-8392-4c7a-bc29-7d9b200b1a5a_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2023.43.12.png?alt=media', N'4e66d5c9-ba0d-4617-b1ae-9b9f1734b97a', CAST(N'2024-11-20T16:40:59.833' AS DateTime), CAST(N'2024-11-20T16:40:59.833' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'82428415-6b17-4538-b1f2-b42e4c8765ef', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F375169f3-3a3d-40fa-b0a9-087d6994fd71_Zara%20Wrap%20Dress.jpg?alt=media', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', CAST(N'2024-08-20T11:21:11.690' AS DateTime), CAST(N'2024-08-20T11:21:11.700' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e8590df0-97e0-4119-a12a-bbbd31025f11', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Ff3dce429-4c55-4762-9a39-97d9fb2d2b30_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'21ce7e0f-809a-42c4-80e8-a2063a4b7295', CAST(N'2024-11-20T21:03:12.370' AS DateTime), CAST(N'2024-11-20T21:03:12.380' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'056daf78-549e-4f30-b1d1-bd8ab33e6a30', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fdebd3ac3-26ec-46f3-8905-bbeaed37b30c_player-icon.png?alt=media', N'081de215-edd7-4af2-8bcb-c0cc6898a236', CAST(N'2024-11-28T19:46:03.620' AS DateTime), CAST(N'2024-11-28T19:46:03.620' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'9febb893-8a66-445e-8810-cf261a3bc972', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb8553d90-5c66-402b-b7dd-89df5dce066c_Zara%20Wrap%20Dress.jpg?alt=media', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', CAST(N'2024-08-20T11:21:14.317' AS DateTime), CAST(N'2024-08-20T11:21:14.327' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'5f63f66d-1fcb-46c5-b556-d079b5f8ca7c', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F3e7bc4b6-c726-4e7e-b270-07c2cfeeb3a4_mrchi-coffee-1.jpg?alt=media', N'a1f9b991-03d3-4172-bfad-c17009f411e6', CAST(N'2024-11-21T01:23:49.977' AS DateTime), CAST(N'2024-11-21T01:23:49.977' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'2acc51cf-8fec-4342-b82f-d8466b7ee58c', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F4a26b3aa-85b8-4f66-b0ab-db0206c4e9f7_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.59.23.png?alt=media', N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', CAST(N'2024-11-20T16:42:04.053' AS DateTime), CAST(N'2024-11-20T16:42:04.053' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'0ce80ea0-5850-4038-8223-daa22d9b9454', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F3369fa75-3f4b-4675-b496-2382918cab19_mrchi-coffee-1.jpg?alt=media', N'2d0b1a25-0e97-48b8-b8d7-34769146923e', CAST(N'2024-11-21T00:53:05.400' AS DateTime), CAST(N'2024-11-21T00:53:05.400' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'75c16823-ae35-46d4-b636-e22630488ab2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F18f6c3d3-15d3-44db-8645-f02f3a8848c5_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-09-26%20lu%CC%81c%2022.17.03.png?alt=media', N'190da44b-c901-4d05-ae74-8900067fbde2', CAST(N'2024-11-20T19:59:43.120' AS DateTime), CAST(N'2024-11-20T19:59:43.120' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'70178acb-04b6-4fce-82d4-e5f0ddc28034', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fadaab94d-cee2-4f55-9310-d50e6b407e87_mrchi-coffee-1.jpg?alt=media', N'03af42ee-4512-4bac-add5-a43b29e0278f', CAST(N'2024-11-21T15:18:06.090' AS DateTime), CAST(N'2024-11-21T15:18:06.090' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'c1b03e62-7a0e-4138-8904-ef352ab7c127', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F1f3f2d9d-d09d-43fe-8d26-b88befd08fb5_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'b4039f2a-5a93-49f2-9479-900d78ea429e', CAST(N'2024-11-21T18:27:31.357' AS DateTime), CAST(N'2024-11-21T18:27:31.357' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'dd8d5136-f30c-43c5-968c-efd44193939f', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F60f0dbd7-b441-44bb-8a2c-b4cd6d7c3ffe_check.png?alt=media', N'0526d5c3-012f-4b6a-af6c-b64dab03caf4', CAST(N'2024-11-21T15:05:01.170' AS DateTime), CAST(N'2024-11-21T15:05:01.170' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'2892ca6b-e952-4f55-bd79-f30b6963e0f2', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fa45295de-1ded-4d87-8ed8-052a89ff506d_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'062e5b9a-d3e4-4903-ae55-4f96d7278cdd', CAST(N'2024-11-29T16:25:50.907' AS DateTime), CAST(N'2024-11-29T16:25:50.907' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'e15a167c-23f7-41d6-8550-f615d1f9ccce', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F71989f85-4143-4967-a2b3-9bcf91f7d7ec_hinh-ly-cafe_3.jpg?alt=media', N'83c97871-7fda-49a6-847a-1c26d5a86e88', CAST(N'2024-11-22T10:55:35.963' AS DateTime), CAST(N'2024-11-22T10:55:35.963' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'55d03299-2801-42c2-acf7-f7ff7c18efe7', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fe5abeef6-fedd-4387-8583-7d05cc565351_z6046707871450_d6d992e273c33c528e08edb0dee7f1d7.jpg?alt=media', N'b5e93e04-a704-449e-ab2c-891890d02f2b', CAST(N'2024-11-20T20:47:24.813' AS DateTime), CAST(N'2024-11-20T20:47:24.813' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'98c0d8d0-8a10-4d9b-870c-fa910c548f98', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F5c6aa4ac-0eeb-4ef8-8d9e-d61d20340416_A%CC%89nh%20ma%CC%80n%20hi%CC%80nh%202023-10-02%20lu%CC%81c%2016.06.33.png?alt=media', N'5593af7c-431d-4dbe-83f0-75475666c0b0', CAST(N'2024-11-20T19:51:46.513' AS DateTime), CAST(N'2024-11-20T19:51:46.513' AS DateTime))
GO
INSERT [dbo].[Image] ([id], [linkImage], [productId], [insDate], [upDate]) VALUES (N'83f89340-36a1-4763-b7bd-fed8983aea70', N'https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fc5c18a37-13af-4f8a-bf49-b5ea92651dee_MrChi.png?alt=media', N'9002b72e-ed41-4881-9e85-ab3cb8a1a4b7', CAST(N'2024-11-21T02:39:38.330' AS DateTime), CAST(N'2024-11-21T02:39:38.330' AS DateTime))
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'6aa3757c-0535-4630-8c8c-0b9bdf9198c4', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(163000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T15:40:40.397' AS DateTime), NULL, 900, 30000, N'Số 15 đường Man Thiện, Phường Tăng Nhơn Phú A, Thành Phố Thủ Đức, Hồ Chí Minh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'7f309c9b-2c92-4780-b697-1619f91ec46e', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(1365000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T09:40:22.163' AS DateTime), NULL, 900, 45000, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'4858dbf3-3aaa-4505-851b-1a6427e7f16a', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T13:42:18.947' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'a79a8b93-2582-4d6f-b4fd-1f998e3e604d', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(522000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T15:55:27.560' AS DateTime), NULL, 900, 30000, N'Số 2 đường Tăng Nhơn Nhơn, Phường 5, Quận 10, Hồ Chí Minh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'a75e4e91-8919-4614-a633-3c0f789bc42e', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(30003.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T12:04:18.680' AS DateTime), NULL, 900, 30000, N'Nguyen Cong Hoan')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'acf30b95-6bec-4b1c-8b37-3d095d84fc81', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(630000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T11:51:17.653' AS DateTime), NULL, 900, 30000, N'Ho Chi MInh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'066fcba6-b5d2-4b18-8e08-4d62327a4900', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(630015.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T13:05:43.097' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'219dd3d5-dc81-44bf-b8ec-597c499c219c', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(390000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T11:58:07.303' AS DateTime), NULL, 900, 30000, N'Ho Chi MInh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'902f82cb-c8bc-4386-96e4-5df2b545f33b', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(715045.00 AS Decimal(18, 2)), N'PENDING', CAST(N'2024-09-22T17:49:55.817' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'07989b34-8889-4370-8af4-66892ebc094d', N'7d7c39b4-acd8-4dc2-83f9-7d89604008f7', CAST(35004.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-23T22:49:19.407' AS DateTime), NULL, 900, 35000, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'9fe5f6a7-2f48-4551-8373-68a9d5a64ad0', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(15000.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-23T14:07:32.517' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'eec8afb9-6aef-4ea2-b94e-6bd0bba6eb82', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(30036.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T12:12:35.613' AS DateTime), NULL, 900, 30000, N'Ho Chi MInh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'9b1ac828-f9a8-4efb-b32b-78b27521870d', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(168000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T14:30:56.797' AS DateTime), NULL, 900, 45000, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'b4f23e0a-8de2-4095-90c1-7b20d179392e', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(0.00 AS Decimal(18, 2)), N'Pending', CAST(N'2024-09-22T21:37:22.480' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'6415bf4d-ab4c-4505-b7cb-95c66169d255', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(700045.00 AS Decimal(18, 2)), N'PENDING', CAST(N'2024-09-22T16:16:47.510' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'cc76defd-d73e-41ed-aad7-966149581474', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(3513705.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T11:46:45.417' AS DateTime), NULL, 900, 30000, N'Ho Chi MInh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'88d4f464-c43e-408e-8023-a7b3b03759f8', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(153000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T15:48:54.790' AS DateTime), NULL, 900, 30000, N'Số 15 đường Phan Chu Du, Xã tân lập, Huyện Đan Phượng, Hà Nội')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'e0d409d4-1988-4be1-8cb0-cee82d8ecea9', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(30369.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T12:13:20.153' AS DateTime), NULL, 900, 30000, N'Ho Chi MInh')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'939592a8-f867-475b-bbba-cf42f46773da', N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', CAST(30369.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T12:16:57.560' AS DateTime), NULL, 900, 30000, N'Nguyen Cong Hoan')
GO
INSERT [dbo].[Order] ([id], [userId], [totalPrice], [status], [insDate], [upDate], [shipStatus], [shipCost], [address]) VALUES (N'f7f4e5fd-9961-44c2-b4ce-fb48d9d01c1d', N'a3d3fba5-f0c2-4716-8a9c-db1754999513', CAST(537000.00 AS Decimal(18, 2)), N'Available', CAST(N'2024-11-29T15:53:15.273' AS DateTime), NULL, 900, 45000, N'Dsố 2 đường Tăng Nhơn Trạch, Phường thắng tam, Thành phố Vũng Tàu, Bà Rịa - Vũng Tàu')
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'01cd7dd8-121e-4093-9613-08900afed08a', N'b575b6fa-9183-4ed7-a373-762073e58e35', N'6415bf4d-ab4c-4505-b7cb-95c66169d255', CAST(150000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T16:17:06.290' AS DateTime), CAST(N'2024-09-22T16:17:06.293' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9705f6dc-8ee5-4d42-b191-123b69af10d9', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'9fe5f6a7-2f48-4551-8373-68a9d5a64ad0', CAST(15000.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-23T14:07:32.523' AS DateTime), CAST(N'2024-09-23T14:07:32.523' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'bf5173d0-1aa9-40ee-87d8-207e51cfcde4', N'cc8d1ba9-103a-4469-b248-9604178ff639', N'066fcba6-b5d2-4b18-8e08-4d62327a4900', CAST(15.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-23T13:05:43.127' AS DateTime), CAST(N'2024-09-23T13:05:43.127' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'1dac6bdc-aa81-4c59-8eba-2c1c791e0695', N'03af42ee-4512-4bac-add5-a43b29e0278f', N'219dd3d5-dc81-44bf-b8ec-597c499c219c', CAST(120000.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T11:58:07.340' AS DateTime), CAST(N'2024-11-29T11:58:07.340' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'd5489db3-0d73-4aad-84c6-2ff192975470', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(200000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T17:49:55.823' AS DateTime), CAST(N'2024-09-22T17:49:55.823' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'871e8031-f75e-45b5-922e-480d7342d24d', N'97263641-0c77-4b94-b45e-e4d13989acf4', N'eec8afb9-6aef-4ea2-b94e-6bd0bba6eb82', CAST(12.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T12:12:35.647' AS DateTime), CAST(N'2024-11-29T12:12:35.647' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9fe7e864-66bb-4eeb-997f-4945ce675fb2', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'6415bf4d-ab4c-4505-b7cb-95c66169d255', CAST(200000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T16:16:52.920' AS DateTime), CAST(N'2024-09-22T16:16:52.927' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'0b995178-2342-42ba-8cd0-507b2ec83027', N'ac19a183-d5b0-4c01-8460-eb7cc4780822', N'a75e4e91-8919-4614-a633-3c0f789bc42e', CAST(1.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T12:04:18.723' AS DateTime), CAST(N'2024-11-29T12:04:18.723' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'cb017b8c-21c6-41e9-880f-60c357f83534', N'b575b6fa-9183-4ed7-a373-762073e58e35', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(150000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-22T17:49:55.850' AS DateTime), CAST(N'2024-09-22T17:49:55.850' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9b625c2f-142c-4671-9313-6371290702bb', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'066fcba6-b5d2-4b18-8e08-4d62327a4900', CAST(200000.00 AS Decimal(18, 2)), 3, CAST(N'2024-09-23T13:05:43.103' AS DateTime), CAST(N'2024-09-23T13:05:43.107' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'b4039f26-ddbe-4950-bc00-66d8e4bcdc41', N'c9dff179-4e6a-429c-886f-58180c494b75', N'6aa3757c-0535-4630-8c8c-0b9bdf9198c4', CAST(123000.00 AS Decimal(18, 2)), 1, CAST(N'2024-11-29T15:40:40.403' AS DateTime), CAST(N'2024-11-29T15:40:40.403' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'5774d31e-1c78-4ca7-9908-6c1b1183c67f', N'da95aeca-217d-4e1a-a667-158eaf0deb5f', N'e0d409d4-1988-4be1-8cb0-cee82d8ecea9', CAST(123.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T12:13:26.140' AS DateTime), CAST(N'2024-11-29T12:13:26.147' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'ec9c9a1a-9b4d-43ff-bf73-7e64d8e94c8a', N'737c0fbd-0f8f-4fce-8aa9-1255e4736ee7', N'cc76defd-d73e-41ed-aad7-966149581474', CAST(1111112.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T11:46:45.497' AS DateTime), CAST(N'2024-11-29T11:46:45.497' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'09e868ea-0e7f-4a6b-b609-7f166624525b', N'b575b6fa-9183-4ed7-a373-762073e58e35', N'cc76defd-d73e-41ed-aad7-966149581474', CAST(150000.00 AS Decimal(18, 2)), 1, CAST(N'2024-11-29T11:46:45.523' AS DateTime), CAST(N'2024-11-29T11:46:45.523' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'32a6573a-af5d-4d53-afd6-81336bef9974', N'c9dff179-4e6a-429c-886f-58180c494b75', N'9b1ac828-f9a8-4efb-b32b-78b27521870d', CAST(123000.00 AS Decimal(18, 2)), 1, CAST(N'2024-11-29T14:30:56.807' AS DateTime), CAST(N'2024-11-29T14:30:56.807' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'8fae577c-3d1c-444a-a61f-8ffe15765aa2', N'cc8d1ba9-103a-4469-b248-9604178ff639', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(15.00 AS Decimal(18, 2)), 3, CAST(N'2024-09-22T17:49:55.847' AS DateTime), CAST(N'2024-09-22T17:49:55.847' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'5fb1064b-e646-4a30-b89a-945f977aab09', N'c9dff179-4e6a-429c-886f-58180c494b75', N'a79a8b93-2582-4d6f-b4fd-1f998e3e604d', CAST(123000.00 AS Decimal(18, 2)), 4, CAST(N'2024-11-29T15:55:27.567' AS DateTime), CAST(N'2024-11-29T15:55:27.567' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'bf17f01e-020a-4729-b356-950906f33184', N'da95aeca-217d-4e1a-a667-158eaf0deb5f', N'cc76defd-d73e-41ed-aad7-966149581474', CAST(123.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T11:46:45.453' AS DateTime), CAST(N'2024-11-29T11:46:45.453' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'9d9f12ec-996b-4d90-b1f0-9f1244b7acec', N'da95aeca-217d-4e1a-a667-158eaf0deb5f', N'939592a8-f867-475b-bbba-cf42f46773da', CAST(123.00 AS Decimal(18, 2)), 3, CAST(N'2024-11-29T12:17:03.190' AS DateTime), CAST(N'2024-11-29T12:17:03.200' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'b6dfa558-a3b9-48fb-9f15-a100e2c513ed', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'902f82cb-c8bc-4386-96e4-5df2b545f33b', CAST(15000.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-22T17:49:55.850' AS DateTime), CAST(N'2024-09-22T17:49:55.850' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'fd88ce0d-8538-42fb-b6ae-a5733d269e34', N'cc8d1ba9-103a-4469-b248-9604178ff639', N'6415bf4d-ab4c-4505-b7cb-95c66169d255', CAST(15.00 AS Decimal(18, 2)), 3, CAST(N'2024-09-22T16:16:59.590' AS DateTime), CAST(N'2024-09-22T16:16:59.597' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'ddb18cdb-01a2-4e79-9838-b1ae84c115c5', N'c9dff179-4e6a-429c-886f-58180c494b75', N'f7f4e5fd-9961-44c2-b4ce-fb48d9d01c1d', CAST(123000.00 AS Decimal(18, 2)), 4, CAST(N'2024-11-29T15:53:15.280' AS DateTime), CAST(N'2024-11-29T15:53:15.280' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'709bf9c8-e5ee-471c-850b-b37f4e02a75d', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'4858dbf3-3aaa-4505-851b-1a6427e7f16a', CAST(15000.00 AS Decimal(18, 2)), 1, CAST(N'2024-09-23T13:42:18.953' AS DateTime), CAST(N'2024-09-23T13:42:18.953' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'1abd8ac7-49df-4712-9419-b89b3d916d9c', N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'acf30b95-6bec-4b1c-8b37-3d095d84fc81', CAST(200000.00 AS Decimal(18, 2)), 1, CAST(N'2024-11-29T11:51:17.707' AS DateTime), CAST(N'2024-11-29T11:51:17.707' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'30df971c-8c6b-482c-84b3-bdc4cc9391a5', N'c9dff179-4e6a-429c-886f-58180c494b75', N'88d4f464-c43e-408e-8023-a7b3b03759f8', CAST(123000.00 AS Decimal(18, 2)), 1, CAST(N'2024-11-29T15:48:54.800' AS DateTime), CAST(N'2024-11-29T15:48:54.800' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'283c20fd-10de-496b-ad8f-c04a7617804e', N'081de215-edd7-4af2-8bcb-c0cc6898a236', N'6aa3757c-0535-4630-8c8c-0b9bdf9198c4', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(N'2024-11-29T15:40:40.477' AS DateTime), CAST(N'2024-11-29T15:40:40.477' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'97978e8e-3744-4278-a0ae-cbd2ee4e3311', N'0c36c4d4-0ccc-4143-b88f-e2025f7e0725', N'7f309c9b-2c92-4780-b697-1619f91ec46e', CAST(120000.00 AS Decimal(18, 2)), 11, CAST(N'2024-11-29T09:40:22.177' AS DateTime), CAST(N'2024-11-29T09:40:22.177' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'fe600432-b34b-4ad4-aea1-d6bf8be2230c', N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'066fcba6-b5d2-4b18-8e08-4d62327a4900', CAST(15000.00 AS Decimal(18, 2)), 2, CAST(N'2024-09-23T13:05:43.127' AS DateTime), CAST(N'2024-09-23T13:05:43.127' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'df390a81-8671-4c21-a0ea-d890ade6b39e', N'dc87c91f-3085-4b35-b4ce-0fcecc30f017', N'acf30b95-6bec-4b1c-8b37-3d095d84fc81', CAST(200000.00 AS Decimal(18, 2)), 2, CAST(N'2024-11-29T11:51:17.680' AS DateTime), CAST(N'2024-11-29T11:51:17.680' AS DateTime))
GO
INSERT [dbo].[OrderDetails] ([id], [productId], [orderId], [price], [quantity], [insDate], [upDate]) VALUES (N'93b27741-6125-4435-a124-dd163c75eb79', N'ac19a183-d5b0-4c01-8460-eb7cc4780822', N'07989b34-8889-4370-8af4-66892ebc094d', CAST(1.00 AS Decimal(18, 2)), 4, CAST(N'2024-11-23T22:49:19.443' AS DateTime), CAST(N'2024-11-23T22:49:19.443' AS DateTime))
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
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'061b0116-cf04-45fb-a818-069e00a89138', N'bao nghi', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã rẽ bất ngờ trong vụ kiện chống lại ông Trump</strong></a></p><p>Phiên tranh luận bị hủy bỏ tại Georgia cùng một thẩm phán Arizona rút lui đã làm dấy lên câu hỏi về tương lai của 4 vụ kiện cấp bang chống lại ông Donald Trump và các đồng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump chọn tỷ phú Lutnick làm bộ trưởng Thương mại</strong></a></li></ul><p><br></p>', NULL, 11, N'Unavailable', N'e93337c2-7eed-4658-9c31-30325baaa233', CAST(N'2024-11-20T20:35:07.817' AS DateTime), CAST(N'2024-11-20T20:35:07.823' AS DateTime), CAST(160000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'dc87c91f-3085-4b35-b4ce-0fcecc30f017', N'Cà phê hoà tan gói 1kg', N'<p><img src="https://i1.wp.com/mrc.com.vn/wp-content/uploads/2022/09/image_2022-09-28_191922276.png?resize=1020%2C1235"></p><h4>“Vị cà phê đậm đà hương vị Việt, uống một lần thôi mà nghiền quá trời nghiền, mong lần sau tới hương vị vẫn như lần đầu được thưởng thức” –&nbsp;<strong>Bạn khách Minh Anh chia sẻ</strong></h4><p><img src="https://i0.wp.com/mrc.com.vn/wp-content/uploads/2022/07/ha-nguy-n-OFSQ6bHjFjs-unsplash-scaled.jpg?resize=1020%2C1529"></p><h4>“Được người bạn mua tặng gói cà phê ở đây, tôi uống thử và cảm nhận rất Good nên quyết định đặt mua thường xuyên để bán tại quán của gia đình” –&nbsp;<strong>Phạm Tuấn Anh</strong></h4><p><img src="https://i2.wp.com/mrc.com.vn/wp-content/uploads/2020/05/Screenshot_20200529-113821_Instagram.jpg?resize=1020%2C760"></p><h4>”Xa quê hương Đaklak đã lâu, giờ về VN lại có hình ảnh quê hương giữa Sài Gòn, nhâm nhi ly cà phê của Mr Chí cho tôi cảm giác thật tuyệt vời” –&nbsp;</h4><h4>Trần Thanh T<strong>hảo Ly</strong></h4><p><br></p>', NULL, 100, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T00:06:48.200' AS DateTime), CAST(N'2024-11-21T00:06:48.200' AS DateTime), CAST(200000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'737c0fbd-0f8f-4fce-8aa9-1255e4736ee7', N'Dưa hấu đà lạt', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F0725d28c-5b5d-4d0f-baf7-bd04ae1be63a_KOL.jpg?alt=media" width="1377" style="cursor: nwse-resize"></p>', NULL, 1231, N'Available', N'bae385df-433c-4438-b53a-4e56c84335ae', CAST(N'2024-11-28T13:44:29.113' AS DateTime), CAST(N'2024-11-28T13:44:29.113' AS DateTime), CAST(1111112 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'da95aeca-217d-4e1a-a667-158eaf0deb5f', N'123123123', N'hihi', NULL, 12, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:58:09.447' AS DateTime), CAST(N'2024-11-20T19:58:09.447' AS DateTime), CAST(123 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'83c97871-7fda-49a6-847a-1c26d5a86e88', N'Cà rốt', N'<p>Ngon<img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F6a7b63cc-bd30-4bad-b69e-0c2f41977016_hinh-ly-cafe.jpg?alt=media"></p>', NULL, 100, N'Available', N'bae385df-433c-4438-b53a-4e56c84335ae', CAST(N'2024-11-22T10:55:35.590' AS DateTime), CAST(N'2024-11-22T10:55:35.590' AS DateTime), CAST(5000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'80ec4ad6-06d0-43d9-8883-1c3ed736bf7c', N'Bánh Pizaa thập cẩm đóng thùng xốp 2kg', N'<h2><em><s>Bánh Pizza ngon nhưng không đủ dinh dưỡng</s></em></h2><h2><em>Bánh Pizza ngon nhưng vẫn đầy đủ dinh dưỡng</em></h2><p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2Fb98b5737-f82b-4bef-b60f-5faa7a04de2e_love-banh-pizza-hai-san.jpeg?alt=media" width="1508" style="cursor: nwse-resize"></p><p>Thông tin sản phẩm như này nè</p>', NULL, 120, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T14:39:02.247' AS DateTime), CAST(N'2024-11-21T14:39:02.247' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'e33e79b2-9ec1-4134-b406-2002ef23e24a', N'Cà phê muối', N'<p><img src="https://i1.wp.com/mrc.com.vn/wp-content/uploads/2022/09/image_2022-09-28_191922276.png?resize=1020%2C1235"></p><h4>“Vị cà phê đậm đà hương vị Việt, uống một lần thôi mà nghiền quá trời nghiền, mong lần sau tới hương vị vẫn như lần đầu được thưởng thức” –&nbsp;<strong>Bạn khách Minh Anh chia sẻ</strong></h4><p><img src="https://i0.wp.com/mrc.com.vn/wp-content/uploads/2022/07/ha-nguy-n-OFSQ6bHjFjs-unsplash-scaled.jpg?resize=1020%2C1529"></p><h4>“Được người bạn mua tặng gói cà phê ở đây, tôi uống thử và cảm nhận rất Good nên quyết định đặt mua thường xuyên để bán tại quán của gia đình” –&nbsp;<strong>Phạm Tuấn Anh</strong></h4><p><img src="https://i2.wp.com/mrc.com.vn/wp-content/uploads/2020/05/Screenshot_20200529-113821_Instagram.jpg?resize=1020%2C760"></p><h4>”Xa quê hương Đaklak đã lâu, giờ về VN lại có hình ảnh quê hương giữa Sài Gòn, nhâm nhi ly cà phê của Mr Chí cho tôi cảm giác thật tuyệt vời” –&nbsp;</h4><h4>Trần Thanh T<strong>hảo Ly</strong></h4><p><br></p>', NULL, 12, N'Available', N'867e7a70-e209-4d05-8da3-82de4ac901fb', CAST(N'2024-11-20T21:21:41.733' AS DateTime), CAST(N'2024-11-20T21:21:41.733' AS DateTime), CAST(150000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'bc14c4e3-665d-40c1-84e0-26b8de1dc36e', N'Huy đẹp trai 1111111', N'hoi b? ngon', NULL, 1, N'Unavailable', N'867e7a70-e209-4d05-8da3-82de4ac901fb', CAST(N'2024-11-20T21:25:27.670' AS DateTime), CAST(N'2024-11-20T21:25:27.670' AS DateTime), CAST(150000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'2d0b1a25-0e97-48b8-b8d7-34769146923e', N'Cà phê hoà tan gói 1000kg', N'<p><img></p>', NULL, 120, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T00:53:04.433' AS DateTime), CAST(N'2024-11-21T00:53:04.433' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'85fe57ed-8ebb-4cc1-b2f8-3777b3d18ca9', N'Cà phê hoà tan gói 10000kg', N'<p><br></p><p><span style="background-color: rgba(255, 255, 255, 1); color: rgba(241, 241, 241, 1)"><img src="https://i2.wp.com/mrc.com.vn/wp-content/uploads/2022/07/ashkan-forouzani-WpiK-dfn6Vg-unsplash.jpg?fit=1020%2C1530" height="1530" width="1020"></span></p><p><span style="background-color: rgba(255, 255, 255, 1); color: rgba(241, 241, 241, 1)"><img src="https://i2.wp.com/mrc.com.vn/wp-content/uploads/2022/07/gc-libraries-creative-tech-lab-wiOEVPVRfW4-unsplash-scaled.jpg?fit=1020%2C680" height="680" width="1020"></span></p><p><span style="background-color: rgba(255, 255, 255, 1); color: rgba(241, 241, 241, 1)"><img src="https://i1.wp.com/mrc.com.vn/wp-content/uploads/2022/07/daniel-tafjord-7GTxjNejlwg-unsplash.jpg?fit=1020%2C1529" height="1529" width="1020"></span></p><p><br></p><p>Sản phẩm có chứa Huy Hói và Cường Lord</p>', NULL, 120, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T00:54:42.457' AS DateTime), CAST(N'2024-11-21T00:54:42.457' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'c6066ada-def9-47cb-a6ed-3826178eeb1e', N'Dưa leo đà lạt', N'<p>aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa</p>', NULL, 123123, N'Available', N'bae385df-433c-4438-b53a-4e56c84335ae', CAST(N'2024-11-28T13:40:37.450' AS DateTime), CAST(N'2024-11-28T13:40:37.450' AS DateTime), CAST(1000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'd48c02ee-0465-44dc-ade9-3e70b58104d6', N'Huy đẹp trai 1111', N'hoi bi ngon', NULL, 1, N'Available', N'867e7a70-e209-4d05-8da3-82de4ac901fb', CAST(N'2024-11-20T21:23:24.403' AS DateTime), CAST(N'2024-11-20T21:23:24.403' AS DateTime), CAST(150000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'3e30c213-db9c-4e96-9897-45d1731d6084', N'ozzzz', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã r? b?t ng? trong v? ki?n ch?ng l?i ông Trump</strong></a></p><p>Phiên tranh lu?n b? h?y b? t?i Georgia cùng m?t th?m phán Arizona rút lui dã làm d?y lên câu h?i v? tuong lai c?a 4 v? ki?n c?p bang ch?ng l?i ông Donald Trump và các d?ng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump ch?n t? phú Lutnick làm b? tru?ng Thuong m?i</strong></a></li></ul><p><br></p>', NULL, 11, N'Unavailable', N'42bcc747-7aff-4f43-a6c5-16d518c176a0', CAST(N'2024-11-20T21:08:19.117' AS DateTime), CAST(N'2024-11-20T21:08:19.117' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'062e5b9a-d3e4-4903-ae55-4f96d7278cdd', N'cai nay nhieu hinh ne ', N'nhieu lam lam luon', N'day la thong diep', 100, N'Available', N'051a00ba-e7ef-4edb-a9c0-334527d36aff', CAST(N'2024-11-29T16:25:49.323' AS DateTime), CAST(N'2024-11-29T16:25:49.323' AS DateTime), CAST(1999999 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'd0c5bd98-d1df-43af-9c5f-51b0a9c10fcf', N'Dưa leo', N'<p>ngon</p>', NULL, 100, N'Available', N'bae385df-433c-4438-b53a-4e56c84335ae', CAST(N'2024-11-22T10:55:57.273' AS DateTime), CAST(N'2024-11-22T10:55:57.273' AS DateTime), CAST(5 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'c9dff179-4e6a-429c-886f-58180c494b75', N'Cà phê Robusta gói 500g', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F312a2fc4-6d01-4ca5-a99b-c0d57a2012d8_cf4.jpeg?alt=media" width="584" style="cursor: nwse-resize"></p>', N'<p>Thông điệp mang tới một vị cà phê rất là robusta</p>', 100, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-28T23:05:26.897' AS DateTime), CAST(N'2024-11-28T23:05:26.897' AS DateTime), CAST(123000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'cd3a6be2-dbf1-4d5c-8c3a-5cddae96733b', N'tra ngon', N'ngon vch', NULL, 11, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:44:54.313' AS DateTime), CAST(N'2024-08-18T14:44:17.853' AS DateTime), CAST(110000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'3b294bb7-3841-466d-a852-5ee45a3ed154', N'string', N'string', NULL, 7, N'Available', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-16T23:26:08.877' AS DateTime), CAST(N'2024-08-16T23:26:08.880' AS DateTime), CAST(200000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'34cf56e5-ae8a-4cad-8436-667f5a3a6762', N'Trà hoa cúc 1000kg', N'<h3><br></h3><h3><strong style="background-color: rgba(255, 255, 255, 1); color: rgba(0, 0, 0, 1)">đánh giá của khách hàng</strong></h3><p><br></p><p><span style="color: rgba(119, 119, 119, 1); background-color: rgba(244, 244, 244, 1)"><img src="https://i1.wp.com/mrc.com.vn/wp-content/uploads/2022/09/image_2022-09-28_191922276.png?resize=1020%2C1235" height="300" width="657" style="cursor: nwse-resize"></span></p><h4><span style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">“Vị cà phê đậm đà hương vị Việt, uống một lần thôi mà nghiền quá trời nghiền, mong lần sau tới hương vị vẫn như lần đầu được thưởng thức” –&nbsp;</span><strong style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">Bạn khách Minh Anh chia sẻ</strong></h4><p><span style="color: rgba(119, 119, 119, 1); background-color: rgba(244, 244, 244, 1)"><img src="https://i0.wp.com/mrc.com.vn/wp-content/uploads/2022/07/ha-nguy-n-OFSQ6bHjFjs-unsplash-scaled.jpg?resize=1020%2C1529" height="300" width="1511" style=""></span></p><h4><span style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">“Được người bạn mua tặng gói cà phê ở đây, tôi uống thử và cảm nhận rất Good nên quyết định đặt mua thường xuyên để bán tại quán của gia đình” –&nbsp;</span><strong style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">Phạm Tuấn Anh</strong></h4><p><span style="color: rgba(119, 119, 119, 1); background-color: rgba(244, 244, 244, 1)"><img src="https://i2.wp.com/mrc.com.vn/wp-content/uploads/2020/05/Screenshot_20200529-113821_Instagram.jpg?resize=1020%2C760" height="300" width="310"></span></p><h4><span style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">”Xa quê hương Đaklak đã lâu, giờ về VN lại có hình ảnh quê hương giữa Sài Gòn, nhâm nhi ly cà phê của Mr Chí cho tôi cảm giác thật tuyệt vời” –&nbsp;</span><strong style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">Trần Thanh Thảo Ly</strong></h4><p><br></p>', NULL, 111, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T02:40:59.123' AS DateTime), CAST(N'2024-11-21T02:40:59.123' AS DateTime), CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'5593af7c-431d-4dbe-83f0-75475666c0b0', N'Cà phê hoà tan gói 500gg', N'<h1>Duybpz</h1>', NULL, 11, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:51:46.183' AS DateTime), CAST(N'2024-11-20T19:51:46.183' AS DateTime), CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b575b6fa-9183-4ed7-a373-762073e58e35', N'tra xanh', N'hoi bi ngon', NULL, 11, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-20T11:13:53.583' AS DateTime), CAST(N'2024-08-20T11:13:53.583' AS DateTime), CAST(150000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'044f474e-0864-4b53-a310-7ae5d16e834e', N'domixi', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã rẽ bất ngờ trong vụ kiện chống lại ông Trump</strong></a></p><p>Phiên tranh luận bị hủy bỏ tại Georgia cùng một thẩm phán Arizona rút lui đã làm dấy lên câu hỏi về tương lai của 4 vụ kiện cấp bang chống lại ông Donald Trump và các đồng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump chọn tỷ phú Lutnick làm bộ trưởng Thương mại</strong></a></li></ul><p><br></p>', NULL, 11111, N'Available', N'867e7a70-e209-4d05-8da3-82de4ac901fb', CAST(N'2024-11-21T15:16:03.917' AS DateTime), CAST(N'2024-11-21T15:16:03.917' AS DateTime), CAST(11111 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'847592c9-aa01-47db-9e2c-86397d29b2d0', N'huyquang ', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã rẽ bất ngờ trong vụ kiện chống lại ông Trump</strong></a></p><p>Phiên tranh luận bị hủy bỏ tại Georgia cùng một thẩm phán Arizona rút lui đã làm dấy lên câu hỏi về tương lai của 4 vụ kiện cấp bang chống lại ông Donald Trump và các đồng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump chọn tỷ phú Lutnick làm bộ trưởng Thương mại</strong></a></li></ul><p><br></p>', NULL, 111, N'Unavailable', N'867e7a70-e209-4d05-8da3-82de4ac901fb', CAST(N'2024-11-21T15:18:54.993' AS DateTime), CAST(N'2024-11-21T15:18:54.993' AS DateTime), CAST(1152 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'190da44b-c901-4d05-ae74-8900067fbde2', N'quang huy', N'hihi', NULL, 12, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:59:42.813' AS DateTime), CAST(N'2024-11-20T19:59:42.813' AS DateTime), CAST(123 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b5e93e04-a704-449e-ab2c-891890d02f2b', N'huy1', N'huy1', NULL, 1111, N'Unavailable', N'e93337c2-7eed-4658-9c31-30325baaa233', CAST(N'2024-11-20T20:47:24.253' AS DateTime), CAST(N'2024-11-20T20:47:24.253' AS DateTime), CAST(11111 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b4039f2a-5a93-49f2-9479-900d78ea429e', N'huyvo1', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="background-color: rgba(0, 0, 0, 0); color: rgba(51, 153, 204, 1)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p>', NULL, 111, N'Available', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T18:27:30.823' AS DateTime), CAST(N'2024-11-21T18:27:30.823' AS DateTime), CAST(12412 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'bcedd3ab-b845-46a8-9d10-9351df7bddc2', N'Cà phê hoà tan gói 0kg', N'<p><img></p>', NULL, 120, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T01:18:04.063' AS DateTime), CAST(N'2024-11-21T01:18:04.063' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'cc8d1ba9-103a-4469-b248-9604178ff639', N'Trà Sữa', N'Trà S?a', NULL, 7, N'Unavailable', N'1a15bf30-af44-43c8-a6c9-a4f86af9b7a6', CAST(N'2024-08-20T14:07:19.110' AS DateTime), CAST(N'2024-11-19T21:28:34.143' AS DateTime), CAST(15 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4e66d5c9-ba0d-4617-b1ae-9b9f1734b97a', N'Trà hoa cúc', N'<h1>S?n ph?m có ch?a cafein</h1>', NULL, 12, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-20T16:40:59.220' AS DateTime), CAST(N'2024-11-20T16:40:59.220' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'3b22ec0a-0633-4a42-bc3d-a04525b4eb52', N'huyvo', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã rẽ bất ngờ trong vụ kiện chống lại ông Trump</strong></a></p><p>Phiên tranh luận bị hủy bỏ tại Georgia cùng một thẩm phán Arizona rút lui đã làm dấy lên câu hỏi về tương lai của 4 vụ kiện cấp bang chống lại ông Donald Trump và các đồng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump chọn tỷ phú Lutnick làm bộ trưởng Thương mại</strong></a></li></ul><p><br></p>', NULL, 111, N'Unavailable', N'867e7a70-e209-4d05-8da3-82de4ac901fb', CAST(N'2024-11-21T15:20:01.850' AS DateTime), CAST(N'2024-11-21T15:20:01.850' AS DateTime), CAST(1152 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'21ce7e0f-809a-42c4-80e8-a2063a4b7295', N'huyqq', N'<p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 153, 204, 1); background-color: rgba(0, 0, 0, 0)"><img src="https://photo.znews.vn/w1000/Uploaded/rotntv/2024_11_19/President_elect_Donald_J._Trump_NYT_1.jpg" alt="Nga re bat ngo trong vu kien chong lai ong Trump hinh anh"></a></p><p><a href="https://znews.vn/nga-re-bat-ngo-trong-vu-kien-chong-lai-ong-trump-post1512288.html" rel="noopener noreferrer" target="_blank" style="color: rgba(0, 107, 160, 1)"><strong>Ngã r? b?t ng? trong v? ki?n ch?ng l?i ông Trump</strong></a></p><p>Phiên tranh lu?n b? h?y b? t?i Georgia cùng m?t th?m phán Arizona rút lui dã làm d?y lên câu h?i v? tuong lai c?a 4 v? ki?n c?p bang ch?ng l?i ông Donald Trump và các d?ng minh.</p><ul><li><a href="https://znews.vn/ong-trump-chon-ty-phu-lutnick-lam-bo-truong-thuong-mai-post1512268.html" rel="noopener noreferrer" target="_blank" style="color: rgba(51, 51, 51, 1)"><strong>Ông Trump ch?n t? phú Lutnick làm b? tru?ng Thuong m?i</strong></a></li></ul><p><br></p>', NULL, 15, N'Unavailable', N'42bcc747-7aff-4f43-a6c5-16d518c176a0', CAST(N'2024-11-20T21:03:06.060' AS DateTime), CAST(N'2024-11-20T21:03:06.067' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'03af42ee-4512-4bac-add5-a43b29e0278f', N'Cà phê phin hộp 10 gói', N'<h2>Cà phê Mrchi chất lượng số 2, ai cũng có thể số 1</h2><h2><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F3c9cad18-e209-4ea9-a9a8-430368d109bb_mrchi-coffee-1.jpg?alt=media" width="602" style=""></h2>', NULL, 120, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T15:18:05.007' AS DateTime), CAST(N'2024-11-21T15:18:05.007' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'9002b72e-ed41-4881-9e85-ab3cb8a1a4b7', N'Trà hoa cúc 100kg', N'<h3><br></h3><h3><strong style="background-color: rgba(255, 255, 255, 1); color: rgba(0, 0, 0, 1)">đánh giá của khách hàng</strong></h3><p><br></p><p><span style="color: rgba(119, 119, 119, 1); background-color: rgba(244, 244, 244, 1)"><img src="https://i1.wp.com/mrc.com.vn/wp-content/uploads/2022/09/image_2022-09-28_191922276.png?resize=1020%2C1235" height="300" width="1358" style="cursor: nwse-resize"></span></p><h4><span style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">“Vị cà phê đậm đà hương vị Việt, uống một lần thôi mà nghiền quá trời nghiền, mong lần sau tới hương vị vẫn như lần đầu được thưởng thức” –&nbsp;</span><strong style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">Bạn khách Minh Anh chia sẻ</strong></h4><p><span style="color: rgba(119, 119, 119, 1); background-color: rgba(244, 244, 244, 1)"><img src="https://i0.wp.com/mrc.com.vn/wp-content/uploads/2022/07/ha-nguy-n-OFSQ6bHjFjs-unsplash-scaled.jpg?resize=1020%2C1529" height="300" width="1511" style=""></span></p><h4><span style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">“Được người bạn mua tặng gói cà phê ở đây, tôi uống thử và cảm nhận rất Good nên quyết định đặt mua thường xuyên để bán tại quán của gia đình” –&nbsp;</span><strong style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">Phạm Tuấn Anh</strong></h4><p><span style="color: rgba(119, 119, 119, 1); background-color: rgba(244, 244, 244, 1)"><img src="https://i2.wp.com/mrc.com.vn/wp-content/uploads/2020/05/Screenshot_20200529-113821_Instagram.jpg?resize=1020%2C760" height="300" width="310"></span></p><h4><span style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">”Xa quê hương Đaklak đã lâu, giờ về VN lại có hình ảnh quê hương giữa Sài Gòn, nhâm nhi ly cà phê của Mr Chí cho tôi cảm giác thật tuyệt vời” –&nbsp;</span><strong style="background-color: rgba(235, 232, 216, 1); color: rgba(85, 85, 85, 1)">Trần Thanh Thảo Ly</strong></h4><p><br></p>', NULL, 111, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T02:39:37.937' AS DateTime), CAST(N'2024-11-21T02:39:37.937' AS DateTime), CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'5ce6d9e9-c0d4-412e-b857-aeab804afcda', N'huynggg', N'huy d?p trai', NULL, 11, N'Unavailable', N'42bcc747-7aff-4f43-a6c5-16d518c176a0', CAST(N'2024-11-20T21:09:24.077' AS DateTime), CAST(N'2024-11-20T21:09:24.077' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'753cd102-d202-4061-97d0-b52bd48d19e4', N'gà', N'<h1><strong>huy </strong></h1><p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F65ea2731-0fb4-4c4d-99bf-032bb2afcf0e_hinh-ly-cafe_3.jpg?alt=media"></p><p>gà</p>', NULL, 10, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-22T10:59:06.080' AS DateTime), CAST(N'2024-11-22T10:59:06.080' AS DateTime), CAST(12 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'0526d5c3-012f-4b6a-af6c-b64dab03caf4', N'Trà hoa cúc 700g', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F0f561daa-8daa-4a8b-a546-008f4ae93552_banh-pizza-thap-cam.jpeg?alt=media" width="281"><img width="284" style="cursor: nwse-resize"></p>', NULL, 11, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T15:05:00.767' AS DateTime), CAST(N'2024-11-21T15:05:00.767' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'74245727-371e-4700-99c0-c0b0d56c95f3', N'Cà phê hoà tan gói 100kg', N'<p><img></p><p><br></p><p>Sản phẩm có chứa Cường và Huy Lord</p>', NULL, 120, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T00:49:07.520' AS DateTime), CAST(N'2024-11-21T00:49:07.520' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'081de215-edd7-4af2-8bcb-c0cc6898a236', N'Trà hoa cúc 1kg', N'<p>Đây là mô tả sản phẩm</p><p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F83c02f8a-743c-4a94-9170-5f2eae9edc8c_Screenshot_1717050188.png?alt=media"></p>', N'<p>Đây là thông điệp</p>', 12, N'Available', N'bae385df-433c-4438-b53a-4e56c84335ae', CAST(N'2024-11-28T19:46:02.427' AS DateTime), CAST(N'2024-11-28T19:46:02.427' AS DateTime), CAST(10000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'a1f9b991-03d3-4172-bfad-c17009f411e6', N'Cà phê hoà tan gói 0kg2', N'<p><img></p>', NULL, 120, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T01:23:45.483' AS DateTime), CAST(N'2024-11-21T01:23:45.483' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'75fe7774-c8c1-4af3-8b7f-cd71817e1f48', N'Trà hoa cúc 500g', N'<h1>S?n ph?m có ch?a Th?ng Cu?ng Lord</h1>', NULL, 12, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-20T16:42:02.273' AS DateTime), CAST(N'2024-11-20T16:42:02.273' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4e253d0f-475f-44fd-9c40-ce1de1fd5e0d', N'huy', N'ngon', NULL, 10, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:32:42.873' AS DateTime), CAST(N'2024-08-18T01:22:23.527' AS DateTime), CAST(11011 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'36df0437-f654-4ae2-8f11-ceef3c7d8bc8', N'Trà hoa cúc 500ggg', N'<p>alkdlkaskdljasd</p>', NULL, 11, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T22:31:42.853' AS DateTime), CAST(N'2024-11-21T22:31:42.853' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'4999d04b-c30d-488d-b5dd-d5d625e0a838', N'cafe hat', N'ngon vch', NULL, 11, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-17T01:45:40.077' AS DateTime), CAST(N'2024-08-17T01:45:40.077' AS DateTime), NULL)
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'059a27a3-d1e9-457b-aff6-d94596720a2f', N'Cà phê hoà tan gói 500g', N'<h3>Cà phê ngon nhung d?</h3>', NULL, 11, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-20T19:45:44.190' AS DateTime), CAST(N'2024-11-20T19:45:44.190' AS DateTime), CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'e35e3cc2-51ad-4657-afe1-e0038d50845e', N'Trà hoa cúc 2kg', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F0f561daa-8daa-4a8b-a546-008f4ae93552_banh-pizza-thap-cam.jpeg?alt=media" width="281"><img width="284" style="cursor: nwse-resize"></p>', NULL, 11, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T15:06:21.683' AS DateTime), CAST(N'2024-11-21T15:06:21.683' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'0c36c4d4-0ccc-4143-b88f-e2025f7e0725', N'Trà hoa cúc 500gg', N'<p>alkdlkaskdljasd</p>', NULL, 11, N'Unavailable', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-21T22:31:14.440' AS DateTime), CAST(N'2024-11-21T22:31:14.440' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'883bd921-351c-4727-ab42-e412d965d741', N'Trà hoa cúc 5020g', N'<p>123123</p>', NULL, 12, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T22:32:27.043' AS DateTime), CAST(N'2024-11-21T22:32:27.043' AS DateTime), CAST(12 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'97263641-0c77-4b94-b45e-e4d13989acf4', N'123', N'<p>123</p>', NULL, 12, N'Available', N'650955c7-dd33-413f-ad69-6544555b4897', CAST(N'2024-11-22T10:24:03.757' AS DateTime), CAST(N'2024-11-22T10:24:03.757' AS DateTime), CAST(12 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'ac19a183-d5b0-4c01-8460-eb7cc4780822', N'Bánh cứt Cường', N'<p>Hihihj ngon</p>', NULL, 12, N'Available', N'38d28eec-b8f4-4fc1-9612-de1b8d51ef36', CAST(N'2024-11-23T22:26:40.283' AS DateTime), CAST(N'2024-11-23T22:26:40.283' AS DateTime), CAST(1 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'7d04ca3e-22db-4784-b0bc-f55dfab67e36', N'Trà hoa cúc 20kg', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o/images%2F0f561daa-8daa-4a8b-a546-008f4ae93552_banh-pizza-thap-cam.jpeg?alt=media" width="281"><img width="284" style="cursor: nwse-resize"></p>', NULL, 11, N'Unavailable', N'670ec06c-7e7d-4ed3-8ba2-e5aef5e2af27', CAST(N'2024-11-21T15:06:49.423' AS DateTime), CAST(N'2024-11-21T15:06:49.423' AS DateTime), CAST(120000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Product] ([id], [productName], [description], [message], [quantity], [status], [categoryId], [insDate], [upDate], [price]) VALUES (N'b78ba4b0-edce-4958-9cc1-ff0ba0803a17', N'tran nguyen', N'ngon ngon', NULL, 7, N'Unavailable', N'090d3bb9-c95e-4f15-a509-5e2702395a5e', CAST(N'2024-08-20T11:19:51.837' AS DateTime), CAST(N'2024-08-20T11:19:51.847' AS DateTime), CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Service] ([id], [serviceName], [status], [insDate], [upDate], [deleteAt]) VALUES (N'8a179dff-a59c-4452-96ca-2d2f1eaecf44', N'Workshop 2', N'Available', CAST(N'2024-11-29T00:45:30.710' AS DateTime), CAST(N'2024-11-29T00:45:30.710' AS DateTime), NULL)
GO
INSERT [dbo].[Service] ([id], [serviceName], [status], [insDate], [upDate], [deleteAt]) VALUES (N'1a64c24a-51ed-4b12-84e8-b040db5df47c', N'Workshop', N'Available', CAST(N'2024-11-29T00:38:37.577' AS DateTime), CAST(N'2024-11-29T00:38:37.577' AS DateTime), NULL)
GO
INSERT [dbo].[Service] ([id], [serviceName], [status], [insDate], [upDate], [deleteAt]) VALUES (N'6fb695e2-8464-4dd7-b0c2-c962c5ed3a7e', N'Triển Lãm', N'Available', CAST(N'2024-11-28T21:05:08.660' AS DateTime), CAST(N'2024-11-28T23:28:11.213' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'5b9c3699-d94a-44ae-9e79-00004bb96f5f', N'tranquoccuong0179', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'tranquoccuong0179@gmail.com', N'Cu?ng Tr?n', N'Available', NULL, N'Customer', CAST(N'2024-08-30T13:26:36.680' AS DateTime), CAST(N'2024-08-30T13:26:36.680' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'37296d41-6271-44fa-8a8e-0300197be8cd', N'tuan123', N'z4FyYXRyManz1tT5EHWEzf1i9mlfK82SnbITi29m6js=', N'quoccuongdevbe@gmail.com', N'võ quang huy', N'Unavailable', N'Male', N'Customer', CAST(N'2024-11-22T11:07:10.577' AS DateTime), CAST(N'2024-11-22T11:07:10.577' AS DateTime), N'0901911212', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'e5a971ba-fd77-4b8e-92fe-1c7737b5ac14', N'bangpham', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'bangphamtpst@gmail.com', N'Ph?m Trúc Bang', N'Unavailable', N'Female', N'Customer', CAST(N'2024-08-29T13:09:32.533' AS DateTime), CAST(N'2024-08-29T13:09:32.533' AS DateTime), N'0336607452', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'd86e36be-9050-49db-a1f9-1c836cd672b0', N'string', N'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=', N'string@gmail.com', N'string', N'Unavailable', N'Male', N'Customer', CAST(N'2024-11-21T19:40:50.973' AS DateTime), CAST(N'2024-11-21T19:40:50.973' AS DateTime), N'0987654321', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'b7e29dcf-64d3-4742-ab42-2683158aeb34', N'bangpham123', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'phamtrucbang2008ptb@gmail.com', N'Ph?m Trúc Bang', N'Unavailable', N'Female', N'Customer', CAST(N'2024-08-29T13:14:05.187' AS DateTime), CAST(N'2024-08-29T13:14:05.187' AS DateTime), N'0336607453', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'3e7bea9b-0c71-4f87-b445-2cb6584dd531', N'tuan', N'z4FyYXRyManz1tT5EHWEzf1i9mlfK82SnbITi29m6js=', N'nphuytuan2002@gmail.com', N'Nguy?n Phan Huy Tu?n', N'Available', N'Male', N'Customer', CAST(N'2024-11-22T09:44:44.903' AS DateTime), CAST(N'2024-11-22T09:44:44.903' AS DateTime), N'0911354297', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'6502de20-46fb-4eb8-a4bc-2f73db8f9634', N'adminMCR', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, N'Available', N'Female', N'Admin', CAST(N'2024-11-22T09:48:25.637' AS DateTime), CAST(N'2024-11-22T09:48:25.637' AS DateTime), N'0909106023', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'b108281e-6b43-48b2-a781-49e64d0bb7ab', N'manager1', N'OA+XcdLfhWbOK9W47XcrC7dP1kV/uAOrLSZ8OU2Jx1A=', N'manager1@gmail.com', N'string', NULL, N'Male', N'Manager', CAST(N'2024-08-16T21:48:35.557' AS DateTime), CAST(N'2024-08-16T21:48:35.560' AS DateTime), N'0901234568', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'9bb61de5-af7c-4afa-9a05-4d9578475ed0', N'quoccuong', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', N'cuongtqse160059@fpt.edu.vn', N'Tr?n Qu?c Cu?ng', N'Available', N'Male', N'Customer', CAST(N'2024-08-30T01:38:33.527' AS DateTime), CAST(N'2024-08-30T01:38:33.527' AS DateTime), N'0363919179', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'e4c9c30b-c2df-46d9-9a88-5600e278695b', N'vhuy16', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Admin', CAST(N'2024-08-14T21:37:48.583' AS DateTime), CAST(N'2024-08-14T21:37:48.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'827fba0f-ccb3-4ce1-b728-7c27e1a48777', N'manager', N'buSkac1OkQU4R/XT/LYdvMkejw7xC+d0jaTEobo4LRc=', N'manager@gmail.com', N'manager', N'Available', N'Male', N'Manager', CAST(N'2024-11-19T20:30:11.857' AS DateTime), CAST(N'2024-11-19T20:30:11.857' AS DateTime), N'0333311232', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'7d7c39b4-acd8-4dc2-83f9-7d89604008f7', N'cuongtq', N'9F69GG+kpIcM0QHspUF2Cmr9DIODeeEJLnebUpskIvQ=', N'tranquoccuong13072002@gmail.com', N'string', N'Available', N'Female', N'Customer', CAST(N'2024-11-19T17:49:45.103' AS DateTime), CAST(N'2024-11-19T17:49:45.103' AS DateTime), N'0987654321', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'59d09a3d-38a6-4133-a4c5-80d07b2bed27', N'vhuy10', N'i7DPbrmxfQ99IrRW8SElfcElTh8BZlNwR2OD6ndt9BQ=', N'voquanghuy56@gmail.com', N'string', N'Unavailable', N'Male', N'Customer', CAST(N'2024-10-11T12:17:39.580' AS DateTime), CAST(N'2024-10-11T12:17:39.580' AS DateTime), N'0901960523', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'6a6f6a03-56ee-48e5-b458-825fd9e7b6c4', N'vhuy1610', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Manager', CAST(N'2024-08-15T13:23:06.377' AS DateTime), CAST(N'2024-08-15T13:23:06.377' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'5aa6b11c-771f-458b-96f2-93c8c593a838', N'vhuy12', N'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=', N'voquanghuy506@gmail.com', N'string', N'Available', N'Male', N'Customer', CAST(N'2024-10-17T22:35:15.480' AS DateTime), CAST(N'2024-10-17T22:38:20.157' AS DateTime), N'0901960501', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'ce419771-09d5-4a6d-8021-a04427dd5d81', N'vhuy16102002', N'WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=', NULL, NULL, NULL, NULL, N'Customer', CAST(N'2024-08-15T13:25:03.243' AS DateTime), CAST(N'2024-08-15T13:25:03.243' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'ce43e40f-af86-4df2-99ca-a6ca6a206dfa', N'admin', N'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', NULL, NULL, N'Available', N'Female', N'Admin', CAST(N'2024-11-22T21:34:23.977' AS DateTime), CAST(N'2024-11-22T21:34:23.977' AS DateTime), N'0987654321', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'f6cdf58c-7388-4db4-be10-ad3e6cf79c9c', N'tangoccu', N'86I39YKhpaYNH+32p/XuNbuXHWQz1mWCKeq0r3fhB/Y=', N'ngocan2003krp@gmail.com', N'T? Ng?c Cu', N'Unavailable', N'Male', N'Customer', CAST(N'2024-09-05T11:19:11.713' AS DateTime), CAST(N'2024-09-05T11:19:11.713' AS DateTime), N'0356638270', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'4260c50a-ff1e-47f2-a312-bfa6df961cd7', N'tangoccu1', N'O6i3JEdRGBho4KYgWSnW4a/qMkyOLntDVR8GYBBnFvE=', N'antnqe170035@fpt.edu.vn', N'T? Ng?c Cu', N'Available', N'Male', N'Customer', CAST(N'2024-09-05T11:21:18.547' AS DateTime), CAST(N'2024-09-05T11:21:18.547' AS DateTime), N'0356638271', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'4487d6fa-9ce6-43f7-87b5-c3810702de12', N'tranquoccuong13072002', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'tranquoccuong13072002@gmail.com', N'Cu?ng Tr?n', N'Available', NULL, N'Customer', CAST(N'2024-08-20T13:56:20.607' AS DateTime), CAST(N'2024-08-20T13:56:20.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'a3d3fba5-f0c2-4716-8a9c-db1754999513', N'duybpz', N'PPPAC7TVWzdd/5xnKBxSi5gGz0o1NfOGkpQwt6AOVTI=', N'duybpz@gmail.com', N'V? Thanh Duy', N'Available', N'Male', N'Customer', CAST(N'2024-08-30T13:02:00.007' AS DateTime), CAST(N'2024-08-30T13:02:00.010' AS DateTime), N'0123456789', NULL)
GO
INSERT [dbo].[User] ([id], [userName], [password], [email], [fullName], [status], [gender], [role], [insDate], [upDate], [phoneNumber], [delDate]) VALUES (N'235b622f-9b4f-4f0a-b0b7-de08b8133cea', N'huyvqse160031', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', N'huyvqse160031@fpt.edu.vn', N'Vo Quang Huy (K16_HCM)', N'Available', NULL, N'Customer', CAST(N'2024-09-18T22:52:05.853' AS DateTime), CAST(N'2024-09-18T22:52:05.853' AS DateTime), NULL, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_index_0]    Script Date: 11/29/2024 7:56:09 PM ******/
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
USE [master]
GO
ALTER DATABASE [MRC] SET  READ_WRITE 
GO
