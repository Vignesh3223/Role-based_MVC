use MVCDatabase

create table Training
(
DomainID int primary key,
Domain varchar(20),
Trainer varchar(20)
)

create table Trainee
(
TraineeID int primary key identity(1,1),
Tname varchar(20),
Tage int,
Tcity varchar(25),
DomainID int references Training(DomainID),
TImage varbinary(max) not null
)

insert into Training values(100,'SE Trainee','Pallavi,Thangam'),(101,'DOTNET','Pallavi Katari'),(102,'PHP','Thangam Arulseeli')

select * from Training 

select * from Trainee

alter table Trainee drop constraint [FK__Trainee__DomainI__06CD04F7]

alter table Trainee drop column DomainID

alter table Trainee add DomainID int default 100 constraint FK__Trainee__DomainID foreign key(DomainID) references Training(DomainID)
on delete set default

select * from Users

delete from Users where userId = 3
