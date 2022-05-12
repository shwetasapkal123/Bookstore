-------------Create database-----------------
create database BookStore;

----------------use created database---------------
use BookStore;

-----------------creating user table-----------
create table Users(
UserId int primary key identity(1,1),
FullName varchar(100),
Address varchar(255),
Email varchar(255),
Password varchar(255),
PhoneNumber bigint
)

select *from Users;

Alter table Users
drop column LastName;

Alter table Users
drop column FirstName;

Alter table Users
Add FullName varchar(255);

select *from Users;

----stored procedures for User Api------------------------
---Create procedured for User Registration----------------
Create procedure UserRegister(
@FullName varchar(255),
@Email varchar(255),
@Password varchar(255),
@Address varchar(255),
@PhoneNumber bigint)
As
Begin
insert into Users(FullName,Email,Password,Address,PhoneNumber) values(@FullName,@Email,@Password,@Address,@PhoneNumber);
end

drop table Users;

---Create procedured for User Login
create procedure UserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Users
where Email = @Email and Password = @Password
End;

---Create procedured for User Forgot Password---------------
create procedure UserForgotPassword
(
@Email varchar(Max)
)
as
begin
Update Users
set Password = 'Null'
where Email = @Email;
select * from Users where Email = @Email;
End;
