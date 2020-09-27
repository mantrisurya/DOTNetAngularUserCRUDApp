ALTER PROCEDURE dbo.GetUserData
(
@id int null
)
as
begin
	if(@id > 0)
	begin		
		select UserID, Name, Email, MobileNumber, Status from dbo.tbl_Users with(nolock)
			where UserID = @id and IsActive = 1
	end
	else
	begin
		select UserID, Name, Email, MobileNumber, Status from dbo.tbl_Users with(nolock)
		where IsActive = 1
	end
end