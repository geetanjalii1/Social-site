create table PostDetails(
PostID int primary key identity(1,1),
InsertionDate datetime default current_timestamp,
UserID int foreign key references UserDetail(UserID),
PostType varchar(10), -- text, image
Value text,
LikeCount int default 0,
IsDelete bit default 0
)