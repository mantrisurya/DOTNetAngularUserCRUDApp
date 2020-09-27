CREATE PROCEDURE dbo.InsertUserData
(
@userId int null,
@name varchar(100),
@mobileNumber varchar(50),
@status varchar(20),
@createdBy int null
)
as
begin
	if(@userID > 0)
	begin		
	update dbo.tbl_Users set IsActive = 0 where UserID = @userId	
	end
	insert into dbo.tbl_User (Name, Email, MobileNumber, Status,IsActive,CreatedDate,CreatedBy)
	values (@name,@mobileNumber,@status,1,GETDATE(),@createdBy)
end