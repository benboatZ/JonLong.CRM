USE [jncrm]
GO
/****** Object:  StoredProcedure [dbo].[t_Sys_User_LoadAll]    Script Date: 09/27/2014 10:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[t_Sys_User_LoadAll]
(
@LoginName	VARCHAR(50)		= NULL
)
AS 
    BEGIN
	
        SET NOCOUNT ON ;
	
        SELECT  [UserId]
			  ,[LoginName]
			  ,[khbh]
			  ,[PwdPrompt]
			  ,[htkh]
			  ,[khEmail]
        FROM    dbo.t_Sys_User AS u WITH ( NOLOCK )
        WHERE u.State = 1  AND (@LoginName IS NULL OR  u.LoginName LIKE '%' + @LoginName +'%')
        and ISNULL(khbh,'')<>''; --alter by duan 20140911 只查询客户相关的帐号
        
        SELECT r.[RoleId] ,
           [RoleName] ,
           ur.UserId
		FROM [jncrm].[dbo].[t_UserRole] AS ur WITH (NOLOCK)
		INNER JOIN dbo.t_Sys_User AS u WITH ( NOLOCK )
			ON ur.UserId = u.UserId
		INNER JOIN dbo.t_Role AS r WITH (NOLOCK)
			ON ur.RoleId = r.RoleId
		WHERE u.State = 1  AND (@LoginName IS NULL OR  u.LoginName LIKE '%' + @LoginName +'%')
        and ISNULL(khbh,'')<>''; 
        
    END