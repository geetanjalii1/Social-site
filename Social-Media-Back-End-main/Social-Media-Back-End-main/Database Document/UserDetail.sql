  Create Table UserDetail(
  UserID int identity(1,1) primary key,
  FullName varchar(255) not null,
  UserName varchar(255),
  PassWord varchar(255),
  InsertionDate datetime default current_timestamp,
  IsActive bit default 1
  )