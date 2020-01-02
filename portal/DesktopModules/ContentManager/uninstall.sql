
if  exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentManager]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
    DROP TABLE [rb_ContentManager]
END

if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_GetModuleTypes]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_GetModuleTypes]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_GetPortals]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_GetPortals]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_GetModuleInstances]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_GetModuleInstances]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_GetModuleInstancesExc]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_GetModuleInstancesExc]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_GetModuleData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_GetModuleData]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_MoveItem]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_MoveItem]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_CopyItem]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_CopyItem]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_CopyAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_CopyAll]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[rb_ContentMgr_DeleteItem]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [rb_ContentMgr_DeleteItem]
GO
