--use master
--go
--if exists(select * from sys.databases where name='AiXiu')
--drop database AiXiu
--go
--上面这几行代码的作用是检查数据库AiXiu是否存在，若存在，则删除
create database AiXiu
go
use AiXiu
go
create table TBLogins(
Id int primary key identity(10001,1),
UserName varchar(32),
MobileNumber varchar(11),
Password varchar(50)
)
go
create table TBUsers(
Id int primary key references TBLogins(Id),
NickName nvarchar(16),
Avatar varchar(100),
Sex int,
Birthday datetime2,
Hobby nvarchar(100),
CreationTime datetime2,
ADDress varchar(100)
)
go
create table TBVideos(
VideoId varchar(100) primary key,
UserId int references TBUsers(Id),
Headline nvarchar(20),
Location varchar(20),
CoverURL varchar(20),
Status nvarchar(50),
UploadTime varchar(100)
)