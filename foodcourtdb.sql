USE [master]
GO
/****** Object:  Database [foodcourt]    Script Date: 10/25/2015 12:28:58 PM ******/
CREATE DATABASE [foodcourt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'foodcourt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\foodcourt.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'foodcourt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\foodcourt_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [foodcourt] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [foodcourt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [foodcourt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [foodcourt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [foodcourt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [foodcourt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [foodcourt] SET ARITHABORT OFF 
GO
ALTER DATABASE [foodcourt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [foodcourt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [foodcourt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [foodcourt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [foodcourt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [foodcourt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [foodcourt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [foodcourt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [foodcourt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [foodcourt] SET  DISABLE_BROKER 
GO
ALTER DATABASE [foodcourt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [foodcourt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [foodcourt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [foodcourt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [foodcourt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [foodcourt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [foodcourt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [foodcourt] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [foodcourt] SET  MULTI_USER 
GO
ALTER DATABASE [foodcourt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [foodcourt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [foodcourt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [foodcourt] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [foodcourt] SET DELAYED_DURABILITY = DISABLED 
GO
USE [foodcourt]
GO
/****** Object:  Table [dbo].[Dish]    Script Date: 10/25/2015 12:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dish](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Dish] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 10/25/2015 12:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Restaurant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RestaurantDish]    Script Date: 10/25/2015 12:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantDish](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantId] [int] NULL,
	[DishId] [int] NULL,
 CONSTRAINT [PK_RestaurantDish] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[RestaurantDish]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantDish_Dish] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dish] ([Id])
GO
ALTER TABLE [dbo].[RestaurantDish] CHECK CONSTRAINT [FK_RestaurantDish_Dish]
GO
ALTER TABLE [dbo].[RestaurantDish]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantDish_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurant] ([Id])
GO
ALTER TABLE [dbo].[RestaurantDish] CHECK CONSTRAINT [FK_RestaurantDish_Restaurant]
GO
USE [master]
GO
ALTER DATABASE [foodcourt] SET  READ_WRITE 
GO
