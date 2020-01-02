/* Install script, Monitoring, [paul@paulyarrow.com], 06/06/2003 */

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_Monitoring]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [rb_Monitoring] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NULL ,
	[PortalID] [int] NULL ,
	[PageID] [int] NULL ,
	[ActivityTime] [datetime] NULL ,
	[ActivityType] [nvarchar] (50) NULL ,
	[Referrer] [nvarchar] (255) NULL ,
	[UserAgent] [nvarchar] (100) NULL ,
	[UserHostAddress] [nvarchar] (15) NULL ,
	[BrowserType] [nvarchar] (100) NULL ,
	[BrowserName] [nvarchar] (100) NULL ,
	[BrowserVersion] [nvarchar] (100) NULL ,
	[BrowserPlatform] [nvarchar] (100) NULL ,
	[BrowserIsAOL] [bit] NULL,
	[UserField] [nvarchar] (500) NULL
) ON [PRIMARY]
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_AddMonitoringEntry]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_AddMonitoringEntry]
GO

CREATE PROCEDURE [rb_AddMonitoringEntry]
    @UserID int,
    @PortalID int,
    @PageID int,
    @ActivityType nvarchar(50),
    @Referrer nvarchar(255),
    @UserAgent nvarchar(100),
    @UserHostAddress nvarchar(15),
    @BrowserType nvarchar(100),
    @BrowserName nvarchar(100),
    @BrowserVersion nvarchar(100),
    @BrowserPlatform nvarchar(100),
    @BrowserIsAOL bit,
    @UserField nvarchar(500)
AS

INSERT INTO rb_Monitoring
(
    UserID,
    PortalID,
    PageID,
    ActivityTime,
    ActivityType,
    Referrer,
    UserAgent,
    UserHostAddress,
    BrowserType,
    BrowserName,
    BrowserVersion,
    BrowserPlatform,
    BrowserIsAOL,
    UserField
)
VALUES
(
    @UserID,
    @PortalID,
    @PageID,
    GETDATE(),
    @ActivityType,
    @Referrer,
    @UserAgent,
    @UserHostAddress,
    @BrowserType,
    @BrowserName,
    @BrowserVersion,
    @BrowserPlatform,
    @BrowserIsAOL,
    @UserField
)
GO


IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_GetMonitoringEntries]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetMonitoringEntries]
GO

CREATE PROCEDURE [rb_GetMonitoringEntries] 
(
    @PortalID			int,
    @StartDate			datetime,
    @EndDate			datetime,
    @ReportType			nvarchar(50),
    @CurrentTabID		bigint,
    @IPAddress			nvarchar(16),
    @IncludeMonitorPage		bit,
    @IncludeAdminUser		bit,
    @IncludePageRequests	bit,
    @IncludeLogon		bit,
    @IncludeLogoff		bit,
    @IncludeIPAddress		bit
)
AS

	SET NOCOUNT ON

	DECLARE @SQLSTRING nvarchar(2000)

	IF (UPPER(@ReportType) = 'DETAILED SITE LOG')
	BEGIN

		SET @SQLSTRING = 'SELECT rbm.ActivityTime, rbm.ActivityType, rbt.TabName, rbu.[Name], rbm.BrowserName, rbm.BrowserVersion, rbm.BrowserPlatform, rbm.UserHostAddress, rbm.UserField ' +
					'FROM rb_Monitoring rbm ' + 
					'LEFT OUTER JOIN rb_Users rbu ON rbm.UserID = rbu.UserID ' +
					'INNER JOIN rb_Portals rbp ON rbm.PortalID = rbp.PortalID ' +
					'LEFT OUTER JOIN rb_Tabs rbt ON rbm.PageID = rbt.TabID ' +
					'WHERE ActivityTime >= ''' + CAST(@StartDate AS nvarchar) + ''' AND ActivityTime <= ''' + CAST(@EndDate AS nvarchar) + '''  ' +
					'AND rbm.PortalID = ' + CAST(@PortalID AS nvarchar)

		IF (@IncludeMonitorPage = 0) 
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.PageID != ' + CAST(@CurrentTabID AS nvarchar) + ' '
		IF (@IncludeAdminUser = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.UserID != 1 '
		IF (@IncludePageRequests = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''PageRequest'' '
		IF (@IncludeLogon = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logon'' '
		IF (@IncludeLogoff = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logoff'' '
		IF (@IncludeIPAddress = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND UserHostAddress != ''' + @IPAddress + ''' '

		SET @SQLSTRING = @SQLSTRING + ' ORDER BY ActivityTime DESC'

	END
	ELSE
	IF (UPPER(@ReportType) = 'PAGE POPULARITY')
	BEGIN

		SET @SQLSTRING = 'SELECT rbt.TabName, ''Requests'' = COUNT(*), ''LastRequest'' = max(ActivityTime) ' +
					'FROM rb_Monitoring rbm ' +
					'INNER JOIN rb_Tabs rbt ON rbm.PageID = rbt.TabID ' +
					'WHERE ActivityTime >= ''' + CAST(@StartDate AS nvarchar) + ''' AND ActivityTime <= ''' + CAST(@EndDate AS nvarchar) + '''  ' +
					'AND rbm.PortalID = ' + CAST(@PortalID AS nvarchar) + ' AND rbm.ActivityType=''PageRequest'''

		IF (@IncludeMonitorPage = 0) 
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.PageID != ' + CAST(@CurrentTabID AS nvarchar) + ' '
		IF (@IncludeAdminUser = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.UserID != 1 '
		IF (@IncludePageRequests = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''PageRequest'' '
		IF (@IncludeLogon = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logon'' '
		IF (@IncludeLogoff = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logoff'' '
		IF (@IncludeIPAddress = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND UserHostAddress != ''' + @IPAddress + ''' '

		SET @SQLSTRING = @SQLSTRING + ' GROUP BY rbt.TabName ORDER BY Requests DESC'

	END
	ELSE
	IF (UPPER(@ReportType) = 'MOST ACTIVE USERS')
	BEGIN

		SET @SQLSTRING = 'SELECT rbu.[Name], ''Actions'' = COUNT(*), ''LastAction'' = max(ActivityTime) ' +
					'FROM rb_Monitoring rbm ' +
					'INNER JOIN rb_Users rbu ON rbm.UserID = rbu.UserID ' +
					'WHERE ActivityTime >= ''' + CAST(@StartDate AS nvarchar) + ''' AND ActivityTime <= ''' + CAST(@EndDate AS nvarchar) + '''  ' +
					'AND rbm.PortalID = ' + CAST(@PortalID AS nvarchar) + ' '

		IF (@IncludeMonitorPage = 0) 
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.PageID != ' + CAST(@CurrentTabID AS nvarchar) + ' '
		IF (@IncludeAdminUser = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.UserID != 1 '
		IF (@IncludePageRequests = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''PageRequest'' '
		IF (@IncludeLogon = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logon'' '
		IF (@IncludeLogoff = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logoff'' '
		IF (@IncludeIPAddress = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND UserHostAddress != ''' + @IPAddress + ''' '

		SET @SQLSTRING = @SQLSTRING + ' GROUP BY rbu.[Name] ORDER BY Actions DESC'

	END
	ELSE
	IF (UPPER(@ReportType) = 'PAGE VIEWS BY DAY')
	BEGIN
		SET @SQLSTRING = 'SELECT ''Date'' = convert(varchar,ActivityTime,102), ''Views'' = COUNT(*), ''Visitors'' = COUNT(DISTINCT UserHostAddress), ''Users'' = COUNT(DISTINCT UserID) ' +
					'FROM rb_Monitoring rbm ' +
					'WHERE ActivityTime >= ''' + CAST(@StartDate AS nvarchar) + ''' AND ActivityTime <= ''' + CAST(@EndDate AS nvarchar) + '''  ' +
					'AND rbm.PortalID = ' + CAST(@PortalID AS nvarchar) + ' AND rbm.ActivityType=''PageRequest'''

		IF (@IncludeMonitorPage = 0) 
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.PageID != ' + CAST(@CurrentTabID AS nvarchar) + ' '
		IF (@IncludeAdminUser = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.UserID != 1 '
		IF (@IncludePageRequests = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''PageRequest'' '
		IF (@IncludeLogon = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logon'' '
		IF (@IncludeLogoff = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logoff'' '
		IF (@IncludeIPAddress = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND UserHostAddress != ''' + @IPAddress + ''' '

		SET @SQLSTRING = @SQLSTRING + 'GROUP BY convert(varchar,ActivityTime,102) ORDER BY [Date] ASC'

	END
	ELSE
	IF (UPPER(@ReportType) = 'PAGE VIEWS BY BROWSER TYPE')
	BEGIN

		SET @SQLSTRING = 'SELECT BrowserType, ''Views'' = COUNT(*) ' +
					'FROM rb_Monitoring rbm ' +
					'WHERE ActivityTime >= ''' + CAST(@StartDate AS nvarchar) + ''' AND ActivityTime <= ''' + CAST(@EndDate AS nvarchar) + '''  ' + 
					'AND rbm.PortalID = ' + CAST(@PortalID AS nvarchar) + ' AND rbm.ActivityType=''PageRequest'''

		IF (@IncludeMonitorPage = 0) 
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.PageID != ' + CAST(@CurrentTabID AS nvarchar) + ' '
		IF (@IncludeAdminUser = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND rbm.UserID != 1 '
		IF (@IncludePageRequests = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''PageRequest'' '
		IF (@IncludeLogon = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logon'' '
		IF (@IncludeLogoff = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND ActivityType != ''Logoff'' '
		IF (@IncludeIPAddress = 0)
			SET @SQLSTRING = @SQLSTRING + 'AND UserHostAddress != ''' + @IPAddress + ''' '

		SET @SQLSTRING = @SQLSTRING + 'GROUP BY BrowserType ORDER BY [Views] DESC'

	END

	exec (@SQLSTRING)
GO
