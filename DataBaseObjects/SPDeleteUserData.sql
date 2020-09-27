CREATE PROCEDURE dbo.DeleteUserData
(
@userId int null
)
as
begin		
	update dbo.tbl_Users set IsActive = 0 where UserID = @userId	
end