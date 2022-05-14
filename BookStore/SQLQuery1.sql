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

--------Creating one column only fullname so doing these part----------------
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
select * from Users where Email = @Email;
End;


---------------------Create book table--------------------
create table Books(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
Rating int,
TotalView int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
);

select *from Books;

----stored procedures for Book Api
---procedured to add book
create procedure AddBook
(
@BookName varchar(255),
@authorName varchar(255),
@rating int,
@totalView int,
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255)
)
as
BEGIN
Insert into Books(BookName, authorName, rating, totalview, originalPrice, 
discountPrice, BookDetails, bookImage)
values (@bookName, @authorName, @rating, @totalView ,@originalPrice, @discountPrice,
@BookDetails, @bookImage);
End;
