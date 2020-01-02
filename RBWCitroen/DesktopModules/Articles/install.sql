/* Install script, Announcements module, mario@hartmann.net, 07/09/03 */

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_Articles]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [rb_Articles] (
	[ItemID] [int] IDENTITY (1, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[CreatedByUser] [nvarchar] (100)  NULL ,
	[CreatedDate] [datetime] NULL ,
	[Title] [nvarchar] (100)  NULL ,
	[Subtitle] [nvarchar] (200)  NULL ,
	[Abstract] [nvarchar] (512)  NULL ,
	[StartDate] [datetime] NULL ,
	[ExpireDate] [datetime] NULL ,
	[IsInNewsletter] [bit] NULL ,
	[MoreLink] [nvarchar] (150)  NULL ,
	[Description] [ntext]  NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [rb_Articles] WITH NOCHECK ADD 
	CONSTRAINT [PK_rb_Articles] PRIMARY KEY  CLUSTERED 
	(
		[ItemID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 


ALTER TABLE [rb_Articles] ADD 
	CONSTRAINT [FK_rb_Articles_rb_Modules] FOREIGN KEY 
	(
		[ModuleID]
	) REFERENCES [rb_Modules] (
		[ModuleID]
	) ON DELETE CASCADE 

END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_AddArticle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_AddArticle]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_DeleteArticle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_DeleteArticle]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_GetArticles]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetArticles]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_GetSingleArticle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetSingleArticle]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_GetSingleArticleWithImages]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetSingleArticleWithImages]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[rb_UpdateArticle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_UpdateArticle]
GO



CREATE PROCEDURE rb_AddArticle
(

    @ModuleID       int,

    @UserName       nvarchar(100),

    @Title          nvarchar(100),

    @Subtitle       nvarchar(200),

    @Abstract	    nvarchar(512),

    @Description    ntext,

    @StartDate      datetime,

    @ExpireDate     datetime,

    @IsInNewsletter bit,

    @MoreLink       nvarchar(150),

    @ItemID         int OUTPUT

)

AS



INSERT INTO rb_Articles

(

    ModuleID,

    CreatedByUser,

    CreatedDate,

    Title,

	Subtitle,

    Abstract,

	Description,

	StartDate,

	ExpireDate,

	IsInNewsletter,

	MoreLink

)

VALUES

(

    @ModuleID,

    @UserName,

    GETDATE(),

    @Title,

    @Subtitle,

    @Abstract,

    @Description,

    @StartDate,

    @ExpireDate,

    @IsInNewsletter,

    @MoreLink

)



SELECT

    @ItemID = @@IDENTITY
-- END proc rb_Addarticles
GO


CREATE PROCEDURE rb_DeleteArticle
(
    @ItemID int
)
AS

DELETE FROM
    rb_Articles

WHERE
    ItemID = @ItemID
GO

CREATE PROCEDURE rb_GetArticles
(
    @ModuleID int
)
AS

SELECT		ItemID, 
			ModuleID, 
			CreatedByUser, 
			CreatedDate, 
			Title, 
			Subtitle, 
			Abstract, 
			Description, 
			StartDate, 
			ExpireDate, 
			IsInNewsletter, 
			MoreLink

FROM        rb_Articles

WHERE
    (ModuleID = @ModuleID) AND (GETDATE() <= ExpireDate) AND (GETDATE() >= StartDate)

ORDER BY
    StartDate DESC
GO


CREATE PROCEDURE rb_GetSingleArticle
(
    @ItemID int
)
AS

SELECT		ItemID,
			ModuleID,
			CreatedByUser,
			CreatedDate,
			Title, 
			Subtitle, 
			Abstract, 
			Description, 
			StartDate, 
			ExpireDate, 
			IsInNewsletter, 
			MoreLink
FROM	rb_Articles
WHERE   (ItemID = @ItemID)
GO


CREATE PROCEDURE rb_GetSingleArticleWithImages
(
    @ItemID int,
    @Variation varchar(50)
)
AS

SELECT		rb_Articles.ItemID, 
			rb_Articles.ModuleID, 
			rb_Articles.CreatedByUser, 
			rb_Articles.CreatedDate, 
			rb_Articles.Title, 
			rb_Articles.Subtitle, 
			rb_Articles.Abstract, 
			rb_Articles.Description, 
            rb_Articles.StartDate, 
            rb_Articles.ExpireDate, 
            rb_Articles.IsInNewsletter, 
            rb_Articles.MoreLink
            
FROM        rb_Articles
WHERE     (ItemID = @ItemID)
GO


CREATE PROCEDURE rb_UpdateArticle

(

    @ItemID         int,

    @ModuleID       int,

    @UserName       nvarchar(100),

    @Title          nvarchar(100),

    @Subtitle       nvarchar(200),

    @Abstract       nvarchar(512),

    @Description    ntext,

    @StartDate      datetime,

    @ExpireDate     datetime,

    @IsInNewsletter bit,

    @MoreLink       nvarchar(150)

)

AS



UPDATE rb_Articles



SET 

ModuleID = @ModuleID,

CreatedByUser = @UserName,

CreatedDate = GETDATE(),

Title =@Title ,

Subtitle =  @Subtitle,

Abstract =@Abstract,

Description =@Description,

StartDate = @StartDate,

ExpireDate =@ExpireDate,

IsInNewsletter = @IsInNewsletter,

MoreLink =@MoreLink

WHERE 

ItemID = @ItemID
GO

