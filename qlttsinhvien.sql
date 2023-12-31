USE [master]
GO
/****** Object:  Database [qlttsinhvien]    Script Date: 10/16/2023 10:38:05 AM ******/
CREATE DATABASE [qlttsinhvien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'qlttsinhvien', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\qlttsinhvien.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'qlttsinhvien_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\qlttsinhvien_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [qlttsinhvien] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [qlttsinhvien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [qlttsinhvien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [qlttsinhvien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [qlttsinhvien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [qlttsinhvien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [qlttsinhvien] SET ARITHABORT OFF 
GO
ALTER DATABASE [qlttsinhvien] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [qlttsinhvien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [qlttsinhvien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [qlttsinhvien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [qlttsinhvien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [qlttsinhvien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [qlttsinhvien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [qlttsinhvien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [qlttsinhvien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [qlttsinhvien] SET  ENABLE_BROKER 
GO
ALTER DATABASE [qlttsinhvien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [qlttsinhvien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [qlttsinhvien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [qlttsinhvien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [qlttsinhvien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [qlttsinhvien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [qlttsinhvien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [qlttsinhvien] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [qlttsinhvien] SET  MULTI_USER 
GO
ALTER DATABASE [qlttsinhvien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [qlttsinhvien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [qlttsinhvien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [qlttsinhvien] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [qlttsinhvien] SET DELAYED_DURABILITY = DISABLED 
GO
USE [qlttsinhvien]
GO
/****** Object:  Table [dbo].[DIEM]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DIEM](
	[MaSV] [char](10) NOT NULL,
	[MaMH] [char](10) NOT NULL,
	[SoDiem] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC,
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MONHOC]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MONHOC](
	[MaMH] [char](10) NOT NULL,
	[TenMH] [nvarchar](50) NULL,
	[SoTC] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SINHVIEN]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SINHVIEN](
	[MaSV] [char](10) NOT NULL,
	[Hoten] [nvarchar](100) NULL,
	[SDT] [varchar](10) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[NamSinh] [char](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DIEM]  WITH CHECK ADD FOREIGN KEY([MaMH])
REFERENCES [dbo].[MONHOC] ([MaMH])
GO
ALTER TABLE [dbo].[DIEM]  WITH CHECK ADD FOREIGN KEY([MaSV])
REFERENCES [dbo].[SINHVIEN] ([MaSV])
GO
ALTER TABLE [dbo].[DIEM]  WITH CHECK ADD FOREIGN KEY([MaSV])
REFERENCES [dbo].[SINHVIEN] ([MaSV])
GO
/****** Object:  StoredProcedure [dbo].[SP_Retrieve_Diem]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_Retrieve_Diem]
as
begin
    select d.MaSV, d.MaMH,mh.TenMH,mh.SoTC, d.SoDiem from SINHVIEN sv,MONHOC mh, DIEM d
	where sv.MaSV=d.MaSV AND mh.MaMH=d.MaMH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Retrieve_DiemSV]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Retrieve_DiemSV]
as
begin
    select d.MaSV, d.MaMH,mh.TenMH,mh.SoTC, d.SoDiem from SINHVIEN sv,MONHOC mh, DIEM d
	where sv.MaSV=d.MaSV AND mh.MaMH=d.MaMH AND d.MaSV='191A010157'
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Retrieve_Student]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Retrieve_Student]
as
begin
    select * from SINHVIEN
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SuaDiem]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_SuaDiem]
@MSSV char(10),
@MaMH char(10),
@Diem int
as
begin
    update DIEM set
    SoDiem=@Diem
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SuaHocSinh]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_SuaHocSinh]
@MSSV varchar(10),
@Ten nvarchar(50),
@DiaChi nvarchar(50),
@Mobile nvarchar(50),
@NamSinh int
as
begin
    update SINHVIEN set
    Hoten = @Ten,
    SDT = @Mobile,
    DiaChi = @DiaChi,
	NamSinh= @NamSinh,
    MaSV = @MSSV
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ThemDiem]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[SP_ThemDiem]
@MSSV char(10),
@MaMH char(10),
@Diem int
as
begin
    insert into DIEM values (@MSSV, @MaMH, @Diem)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ThemHocSinh]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_ThemHocSinh]
@MSSV varchar(10),
@Ten nvarchar(50),
@DiaChi nvarchar(50),
@Mobile nvarchar(50),
@NamSinh int
as
begin
    insert into SINHVIEN values (@MSSV, @Ten, @DiaChi, @Mobile, @NamSinh)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_XoaDiem]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_XoaDiem]
@MSSV char(10),
@MaMH char(10)
as
begin
    delete DIEM where MaSV = @MSSV and MaMH= @MaMH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_XoaHocSinh]    Script Date: 10/16/2023 10:38:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_XoaHocSinh]
@MSSV varchar(10)
as
begin
    delete SINHVIEN where MaSV = @MSSV
end
GO
USE [master]
GO
ALTER DATABASE [qlttsinhvien] SET  READ_WRITE 
GO
