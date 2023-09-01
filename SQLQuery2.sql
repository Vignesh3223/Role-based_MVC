use MVCDatabase

select * from Training
select * from Trainee

delete from Training where DomainID = 2

insert into Training values (1,'SE Trainee', 'Pallavi,Thangam'),(2,'DOTNET','Pallavi Katari'),(3,'PHP','Thangam Arulseeli')

alter table Trainee drop constraint [FK__Trainee__DomainI__52593CB8]

alter table Trainee drop column DomainID

alter table Trainee add DomainID int default 1 constraint FK__Trainee__DomainI foreign key(DomainID) references Training(DomainID)
on delete cascade on update set default

select * from Trainee


insert into Training values (4,'REact','Pallavi')

update Training set DomainID=5 where DomainID=4

create table Roles(
RoleID int primary key,
RoleName varchar(20)
)

insert into Roles values(1,'Admin'),(2,'Trainer'),(3,'Trainee')

create table Users
(
UserId int primary key identity(1,1),
Username varchar(25),
Password varchar(20),
LastLoginDate datetime,
RoleID int references Roles(RoleID)
)

insert into Users values('Admin','Admin@123','2022-08-31',1)

select * from Roles
select * from Users
    
drop table Users

create Procedure [dbo].[Validate_User]
@Username nvarchar(25),
@Password nvarchar(20)
as begin
set nocount on
declare @UserId int,@LastLoginDate datetime,@RoleID int
select @UserId = UserId,@LastLoginDate = LastLoginDate,@RoleID = RoleID from Users where Username = @Username and [Password] = @Password
if @UserId is not null
begin
update Users set LastLoginDate = GETDATE() where UserId = @UserId
select @UserId[UserId],(select RoleName from Roles where RoleID = @RoleID)[Roles]
end
else
begin
select -1[UserId],''[Roles]
end
end


delete from Users where UserId=7

insert into Users values('Vignesh','Vicky@123','2022-08-31',3),('Yamini','Yamini@1611','2022-08-31',2)