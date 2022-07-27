Create Table FriendRequestTable(
FriendRequestID int identity(1,1) primary key,
RequestTime datetime default current_timestamp,
AcceptTime datetime,
FromUserID int foreign key references UserDetail(UserId),
ToUserID int foreign key references UserDetail(UserId),
IsAccept bit default 0,
IsReject bit default 0,
IsActive bit default 1
);

#FriendRequestID RequestTime AcceptTime FromUserID ToUserID IsAccept IsReject IsActive