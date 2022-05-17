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

--procedure to updatebook
create procedure UpdateBook
(
@bookId int,
@bookName varchar(255),
@authorName varchar(255),
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255)
)
as
BEGIN
Update Books set bookName = @bookName, 
authorName = @authorName,
originalPrice= @originalPrice,
discountPrice = @discountPrice,
BookDetails = @BookDetails,
bookImage =@bookImage
where bookId = @bookId;
End;

---Procedure to deletebook
create procedure DeleteBook
(
@bookId int
)
as
BEGIN
Delete Books 
where bookId = @bookId;
End;

---create procedure to getbookbybookid
create procedure GetBookByBookId
(
@bookId int
)
as
BEGIN
select * from Books
where bookId = @bookId;
End;

-- create procedure to get all book -------------------------
create procedure GetAllBook
as
BEGIN
	select * from Books;
End;


---Create cart table------------------------------------
create Table Cart
(
CartId INT IDENTITY(1,1) PRIMARY KEY,
Quantity INT
);

alter table Cart add UserId int  references Users (UserId)
alter table Cart add BookId int  references Books (BookId)

select * from Cart;

---create procedure to addcart-------------------
create Procedure AddCart
(
@Quantity int,
@UserId int,
@BookId int
)
as
BEGIN
if(Exists (select * from Books where bookId = @BookId))
begin
Insert Into Cart(Quantity,UserId, BookId)
Values (@Quantity,@UserId, @BookId);
end
else
begin
select 1
end			 
End;

---create procedure to UpdateCart
create procedure UpdateCart
(
@Quantity int,
@BookId int,
@CartId int
)
as
BEGIN
update Cart 
set BookId = @BookId,
Quantity = @Quantity 
where CartId = @CartId;
End;

---Create procedure to deletecart
create procedure DeleteCart
(
@CartId int
)
as
BEGIN
Delete Cart 
where CartId = @CartId 
End;

--create procedure to getcartbyuserid-------------------
alter Procedure GetCartbyUserId
(
@userId INT
)
AS
BEGIN
SELECT 
c.cartId,
b.BookName,
b.AuthorName,
b.discountPrice,
b.DiscountPrice,
b.OriginalPrice,
b.BookImage
FROM [Cart] AS c
LEFT JOIN [Books] AS b ON c.BookId = b.BookId
WHERE c.UserId = @userId
END

--------------------create address type table----------------------------
create Table AddressTypeT
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	TypeName varchar(255)
);

select * from AddressTypeT

---------------insert record for addresstype table-----------------
insert into AddressTypeT values('Home'),('Office'),('Other');

-----------------create address table-------------------------
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
FullAddress varchar(255),
City varchar(100),
State varchar(100),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeT(TypeId),
UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
select * from AddressTable

------------------------create procedure to AddAddress---------------------
------------------------ Procedure To Add Address--------------------------
create procedure AddAddress
(
@FullAddress varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
as
BEGIN
If Exists (select * from AddressTypeT where TypeId = @TypeId)
begin
Insert into AddressTable 
values(@FullAddress, @City, @State, @TypeId, @UserId);
end
Else
begin
select 2
end
End;
select * from Users
select * from Books
select * from Cart

sp_help users

SET IDENTITY_INSERT Addresses ON 
delete from Addresses where AddressId = 0

------------------------create procedure for updateAddress----------------------
create procedure UpdateAddress
(
	@AddressId int,
	@FullAddress varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int
)
as
BEGIN
If Exists (select * from AddressTypeT where TypeId = @TypeId)
begin
Update AddressTable set
FullAddress = @FullAddress, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
Else
begin
select 2
end
End;

----------------------create procedure to delete address--------------------
create Procedure DeleteAddress
(
@AddressId int
)
as
BEGIN
Delete AddressTable where AddressId = @AddressId 
End;

-- --------------------Procedure To Get All Address--------------------------
create Procedure GetAllAddress
(
@UserId int
)
as
BEGIN
Select FullAddress, City, State,a1.UserId, a2.TypeId
from AddressTable a1
Inner join AddressTypeT a2 on a2.TypeId = a1.TypeId 
where UserId = @UserId;
END;
