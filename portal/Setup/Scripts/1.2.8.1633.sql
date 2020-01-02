---------------------
--1.2.8.1633.sql
---------------------


-- Add new module: WebPartModule
DECLARE @GeneralModDefID uniqueidentifier
DECLARE @FriendlyName nvarchar(128)
DECLARE @DesktopSrc nvarchar(256)
DECLARE @MobileSrc nvarchar(256)
DECLARE @AssemblyName varchar(50)
DECLARE @ClassName nvarchar(128)
DECLARE @Admin bit
DECLARE @Searchable bit

SET @GeneralModDefID = '{2502DB18-B580-4F90-8CB4-C15E6E531009}'
SET @FriendlyName = 'WebPartModule'
SET @DesktopSrc = 'DesktopModules/WebPartModule/WebPartModule.ascx'
SET @MobileSrc = ''
SET @AssemblyName = 'Rainbow.DLL'
SET @ClassName = 'Rainbow.DesktopModules.WebPartModule'
SET @Admin = 0
SET @Searchable = 0

-- Installs module
EXEC [rb_AddGeneralModuleDefinitions] @GeneralModDefID, @FriendlyName, @DesktopSrc, @MobileSrc, @AssemblyName, @ClassName, @Admin, @Searchable

-- Install it for default portal
EXEC [rb_UpdateModuleDefinitions] @GeneralModDefID, 0, 1
GO


/* add version info */
INSERT INTO [rb_Versions] ([Release],[Version],[ReleaseDate]) VALUES('1633','1.2.8.1633', CONVERT(datetime, '05/01/2003', 101))
GO
