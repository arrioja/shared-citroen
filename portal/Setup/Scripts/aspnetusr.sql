use [Rainbow]
exec sp_grantlogin 'MANUTABLET\ASPNET'
exec sp_addrolemember 'db_owner', 'MANUTABLET\ASPNET'
exec sp_grantdbaccess 'MANUTABLET\ASPNET'
